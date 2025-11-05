# Quick script to verify the forklift course is in Production database
$env:ASPNETCORE_ENVIRONMENT = "Production"

Write-Host "`n🔍 Verifying Forklift Course in Production Database...`n" -ForegroundColor Cyan

# Run a simple query to check course status
$result = dotnet run --verify-course 2>&1

Write-Host $result
