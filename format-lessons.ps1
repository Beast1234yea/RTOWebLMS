#!/usr/bin/env pwsh
# Fix lesson content formatting to add proper paragraphs and structure

Write-Host "📝 RTOWebLMS - Fix Lesson Content Formatting" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan

Set-Location "C:\Users\nickb\Projects\RTOWebLMS"

# Create a simple console app to run the formatting fix
$formatCode = @"
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        var connectionString = `$"Data Source={dbPath}";
        options.UseSqlite(connectionString);
    }
});

var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LmsDbContext>();
    
    Console.WriteLine("🔧 Formatting lesson content with proper HTML structure...");
    var success = await FormatLessonContent.FixAllLessonFormatting(dbContext);
    
    if (success)
    {
        Console.WriteLine("✅ Content formatting completed!");
        Console.WriteLine("📖 Lessons now have proper paragraphs, headings, and lists");
        Console.WriteLine("🌐 Refresh your browser to see the formatted content");
    }
    else
    {
        Console.WriteLine("❌ Formatting failed!");
    }
}
"@

# Write to temporary C# file
$formatCode | Out-File -FilePath "TempFormat.cs" -Encoding UTF8

Write-Host "📦 Creating temporary formatting project..." -ForegroundColor Green

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
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Data/*.cs" />
    <Compile Include="Models/*.cs" />
    <Compile Include="Enums/*.cs" />
    <Compile Include="Utilities/*.cs" />
    <Compile Include="TempFormat.cs" />
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

$projectXml | Out-File -FilePath "TempFormat.csproj" -Encoding UTF8

Write-Host "🚀 Running content formatting..." -ForegroundColor Green
dotnet run --project TempFormat.csproj

# Cleanup
Write-Host "🧹 Cleaning up temporary files..." -ForegroundColor Yellow
Remove-Item "TempFormat.cs" -ErrorAction SilentlyContinue
Remove-Item "TempFormat.csproj" -ErrorAction SilentlyContinue

Write-Host "✅ Done! Refresh your browser to see properly formatted lessons with paragraphs!" -ForegroundColor Green