# RTO LMS - Database Seeder Script
# Run this after migrations to create default tenant

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "  RTO LMS - Database Setup" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
Write-Host ""

# Ensure we're using SQLite for local development
$env:DatabaseProvider = "Sqlite"
Write-Host "Setting DatabaseProvider to SQLite for local development..." -ForegroundColor Yellow
Write-Host ""

# Run the seeder
Write-Host "Running database seeder..." -ForegroundColor Yellow
dotnet run --project . Scripts/DatabaseSeeder.cs

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ Database setup complete!" -ForegroundColor Green
    Write-Host ""
    Write-Host "You can now:" -ForegroundColor Cyan
    Write-Host "  1. Start the application: " -NoNewline
    Write-Host "dotnet run" -ForegroundColor Yellow
    Write-Host "  2. Register at: " -NoNewline
    Write-Host "https://localhost:5001/register" -ForegroundColor Yellow
    Write-Host ""
} else {
    Write-Host ""
    Write-Host "❌ Database seeding failed. See error above." -ForegroundColor Red
    Write-Host ""
}
