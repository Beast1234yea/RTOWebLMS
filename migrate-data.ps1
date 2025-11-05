# Data Migration Script: SQLite → Supabase
Write-Host "RTO Web LMS - Data Migration Tool" -ForegroundColor Cyan
Write-Host "==================================`n" -ForegroundColor Cyan

# Compile and run the migration
Write-Host "Compiling migration tool..." -ForegroundColor Yellow
$code = @'
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Scripts;

await MigrateDataToSupabase.Run();
'@

# Save to temp file
$tempFile = Join-Path $PSScriptRoot "temp_migration_runner.cs"
$code | Out-File -FilePath $tempFile -Encoding UTF8

try {
    # Run using dotnet run with the script
    Write-Host "Starting migration...`n" -ForegroundColor Green
    dotnet build --nologo

    if ($LASTEXITCODE -eq 0) {
        # Create a simple console app to run the migration
        $migrationCode = @"
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Scripts;

Console.WriteLine("Starting data migration from SQLite to Supabase...");
await MigrateDataToSupabase.Run();
"@

        # Write migration runner
        $migrationRunner = Join-Path $PSScriptRoot "MigrationRunner.cs"
        $migrationCode | Out-File -FilePath $migrationRunner -Encoding UTF8

        # Use C# scripting
        dotnet-script $migrationRunner 2>&1

        if ($LASTEXITCODE -ne 0) {
            Write-Host "`ndotnet-script not found, using alternative method..." -ForegroundColor Yellow

            # Alternative: Create temp console project
            $tempDir = Join-Path $PSScriptRoot "temp_migration"
            if (Test-Path $tempDir) {
                Remove-Item -Path $tempDir -Recurse -Force
            }
            New-Item -ItemType Directory -Path $tempDir -Force | Out-Null

            Push-Location $tempDir
            try {
                dotnet new console -n MigrationTool -f net9.0
                cd MigrationTool

                # Add required packages
                dotnet add package Microsoft.EntityFrameworkCore.Sqlite
                dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

                # Add project reference to main web app
                dotnet add reference ..\..\RTOWebLMS.csproj

                # Replace Program.cs
                $migrationCode | Out-File -FilePath "Program.cs" -Encoding UTF8 -Force

                # Build and run
                dotnet run --no-build
            }
            finally {
                Pop-Location
                # Cleanup
                if (Test-Path $tempDir) {
                    Remove-Item -Path $tempDir -Recurse -Force -ErrorAction SilentlyContinue
                }
            }
        }
    }
}
finally {
    # Cleanup temp files
    if (Test-Path $tempFile) {
        Remove-Item -Path $tempFile -Force -ErrorAction SilentlyContinue
    }
    if (Test-Path $migrationRunner) {
        Remove-Item -Path $migrationRunner -Force -ErrorAction SilentlyContinue
    }
}

Write-Host "`nMigration complete!" -ForegroundColor Green
