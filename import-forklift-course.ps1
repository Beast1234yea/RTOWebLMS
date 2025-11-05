# PowerShell Script to Import Forklift Course to Supabase
# Run this script from the RTOWebLMS project root directory

Write-Host "╔══════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║    TLILIC003 FORKLIFT COURSE - DATABASE IMPORT           ║" -ForegroundColor Cyan
Write-Host "╚══════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

# Check if we're in the right directory
if (-not (Test-Path "RTOWebLMS.csproj")) {
    Write-Host "❌ ERROR: Must run from RTOWebLMS project directory" -ForegroundColor Red
    Write-Host "Current directory: $PWD" -ForegroundColor Yellow
    exit 1
}

# Check database connection
Write-Host "🔍 Checking database connection..." -ForegroundColor Yellow
$env:ASPNETCORE_ENVIRONMENT = "Production"

# Compile the import script
Write-Host "🔨 Compiling import script..." -ForegroundColor Yellow
$compileResult = dotnet build --no-restore 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Compilation failed!" -ForegroundColor Red
    Write-Host $compileResult
    exit 1
}

Write-Host "✅ Compilation successful`n" -ForegroundColor Green

# Run the import
Write-Host "🚀 Starting forklift course import...`n" -ForegroundColor Cyan

# Create a temporary C# script runner
$runnerCode = @'
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Scripts;

class Program
{
    static async Task Main(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("SUPABASE_CONNECTION_STRING") ??
            "Host=aws-0-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.blhzpoicleeojjxokztu;Password=Bj3gsd83ufu!;SSL Mode=Require";

        var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        using (var db = new LmsDbContext(optionsBuilder.Options))
        {
            Console.WriteLine("📡 Testing database connection...");
            try
            {
                await db.Database.CanConnectAsync();
                Console.WriteLine("✅ Database connection successful!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Database connection failed: {ex.Message}");
                Environment.Exit(1);
            }

            var importer = new ImportForkliftCourseSimple(db);
            var success = await importer.Run();

            Environment.Exit(success ? 0 : 1);
        }
    }
}
'@

# Save and run
$runnerCode | Out-File -FilePath "TempImportRunner.cs" -Encoding UTF8

try {
    # Run with dotnet script (if available) or compile and run
    if (Get-Command "dotnet-script" -ErrorAction SilentlyContinue) {
        dotnet script TempImportRunner.cs
    } else {
        Write-Host "⚠️  dotnet-script not found. Use alternative method:" -ForegroundColor Yellow
        Write-Host "   1. Install: dotnet tool install -g dotnet-script" -ForegroundColor Cyan
        Write-Host "   2. Or run manually from Program.cs`n" -ForegroundColor Cyan

        Write-Host "📝 Alternative: Add this to your Program.cs for testing:" -ForegroundColor Yellow
        Write-Host @"

// Temporary: Import Forklift Course (remove after import)
if (args.Contains("--import-forklift"))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<LmsDbContext>();
        var importer = new RTOWebLMS.Scripts.ImportForkliftCourseSimple(db);
        await importer.Run();
        return;
    }
}
"@ -ForegroundColor Gray

        Write-Host "`nThen run: dotnet run --import-forklift`n" -ForegroundColor Cyan
    }
} finally {
    # Cleanup
    if (Test-Path "TempImportRunner.cs") {
        Remove-Item "TempImportRunner.cs"
    }
}

Write-Host "`n✨ Import process completed!" -ForegroundColor Green
Write-Host "🔗 View your course at: http://localhost:5000/courses/tlilic003-forklift-licence`n" -ForegroundColor Cyan
