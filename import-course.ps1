#!/usr/bin/env pwsh
# Import full TLILIC003 forklift course content

Write-Host "🚛 Importing TLILIC003 Forklift Course Content" -ForegroundColor Cyan
Write-Host "=============================================" -ForegroundColor Cyan

Set-Location "C:\Users\nickb\Projects\RTOWebLMS"

# Create a simple console app to run the import
$importCode = @"
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables();

var configuration = builder.Build();

var services = new ServiceCollection();

var databaseProvider = configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";

services.AddDbContext<LmsDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        options.UseNpgsql(connectionString);
    }
    else
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataPath, "RTODesktopLMS", "rto_lms.db");
        var connectionString = $"Data Source={dbPath}";
        options.UseSqlite(connectionString);
    }
});

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LmsDbContext>();
    
    Console.WriteLine("🗄️  Ensuring database exists...");
    await dbContext.Database.EnsureCreatedAsync();
    
    Console.WriteLine("📚 Running forklift course import...");
    var importer = new ImportForkliftCourseSimple(dbContext);
    var success = await importer.Run();
    
    if (success)
    {
        Console.WriteLine("✅ Import completed successfully!");
        Console.WriteLine("🌐 Restart your application and check /debug/courses");
    }
    else
    {
        Console.WriteLine("❌ Import failed!");
    }
}
"@

# Write to temporary C# file
$importCode | Out-File -FilePath "TempImport.cs" -Encoding UTF8

Write-Host "📦 Creating temporary import project..." -ForegroundColor Green

# Create a temporary project file
$projectXml = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="9.0.0" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.0" />
    <PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.0" />
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Data/*.cs" />
    <Compile Include="Models/*.cs" />
    <Compile Include="Enums/*.cs" />
    <Compile Include="Utilities/*.cs" />
    <Compile Include="TempImport.cs" />
  </ItemGroup>
  <ItemGroup>
    <Content Include="appsettings.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Include="appsettings.Development.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
</Project>
"@

$projectXml | Out-File -FilePath "TempImport.csproj" -Encoding UTF8

Write-Host "🚀 Running import..." -ForegroundColor Green
dotnet run --project TempImport.csproj

# Cleanup
Write-Host "🧹 Cleaning up temporary files..." -ForegroundColor Yellow
Remove-Item "TempImport.cs" -ErrorAction SilentlyContinue
Remove-Item "TempImport.csproj" -ErrorAction SilentlyContinue

Write-Host "✅ Done! Check /debug/courses to verify the import." -ForegroundColor Green