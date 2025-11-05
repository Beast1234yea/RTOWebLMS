# Quick script to check Supabase database for forklift course
Write-Host "`n🔍 Checking Supabase Database...`n" -ForegroundColor Cyan

# Connection details (from appsettings.json)
$host = "aws-0-ap-southeast-2.pooler.supabase.com"
$port = "5432"
$database = "postgres"
$username = "postgres.blhzpoicleeojjxokztu"
$password = "SkylaHugo2025"

# Build connection string
$connString = "Host=$host;Port=$port;Database=$database;Username=$username;Password=$password;SSL Mode=Require;Trust Server Certificate=true"

Write-Host "Attempting to connect to Supabase..." -ForegroundColor Yellow

# Try using psql if available, otherwise use .NET
if (Get-Command psql -ErrorAction SilentlyContinue) {
    Write-Host "Using psql to query database..." -ForegroundColor Green

    $env:PGPASSWORD = $password

    # Check for forklift course
    $courseQuery = "SELECT ""CourseId"", ""Title"", ""UnitCode"", ""Status"" FROM ""Courses"" WHERE ""UnitCode"" = 'TLILIC003';"
    $courseResult = psql -h $host -p $port -U $username -d $database -c $courseQuery 2>&1

    # Check module count
    $moduleQuery = "SELECT COUNT(*) FROM ""Modules"" WHERE ""CourseId"" IN (SELECT ""CourseId"" FROM ""Courses"" WHERE ""UnitCode"" = 'TLILIC003');"
    $moduleCount = psql -h $host -p $port -U $username -d $database -t -c $moduleQuery 2>&1

    # Check lesson count
    $lessonQuery = "SELECT COUNT(*) FROM ""Lessons"" WHERE ""ModuleId"" IN (SELECT ""ModuleId"" FROM ""Modules"" WHERE ""CourseId"" IN (SELECT ""CourseId"" FROM ""Courses"" WHERE ""UnitCode"" = 'TLILIC003'));"
    $lessonCount = psql -h $host -p $port -U $username -d $database -t -c $lessonQuery 2>&1

    Write-Host "`n📊 SUPABASE DATABASE STATUS:" -ForegroundColor Cyan
    Write-Host "================================" -ForegroundColor Cyan
    Write-Host "`nForklift Course (TLILIC003):" -ForegroundColor White
    Write-Host $courseResult
    Write-Host "`nModule Count: $moduleCount" -ForegroundColor Green
    Write-Host "Lesson Count: $lessonCount" -ForegroundColor Green

} else {
    Write-Host "psql not found. Using .NET to check database..." -ForegroundColor Yellow

    # Use dotnet to check
    dotnet run -- --check-course 2>&1
}

Write-Host "`n✅ Database check complete!`n" -ForegroundColor Green
