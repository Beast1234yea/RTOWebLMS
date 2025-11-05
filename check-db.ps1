# PowerShell script to query Supabase database

Write-Host "`n🔍 Querying Supabase Database...`n" -ForegroundColor Cyan

$connectionString = "Host=aws-0-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.blhzpoicleeojjxokztu;Password=SkylaHugo2025;SSL Mode=Require;Trust Server Certificate=true"

# Check if psql is available
if (Get-Command psql -ErrorAction SilentlyContinue) {
    Write-Host "Using psql to query database...`n" -ForegroundColor Green

    $env:PGPASSWORD = "SkylaHugo2025"

    Write-Host "=== ALL COURSES ===" -ForegroundColor Yellow
    psql -h aws-0-ap-southeast-2.pooler.supabase.com -p 5432 -U postgres.blhzpoicleeojjxokztu -d postgres -c 'SELECT "CourseId", "Title", "UnitCode", "Status" FROM "Courses";'

    Write-Host "`n=== MODULE COUNTS ===" -ForegroundColor Yellow
    psql -h aws-0-ap-southeast-2.pooler.supabase.com -p 5432 -U postgres.blhzpoicleeojjxokztu -d postgres -c 'SELECT c."UnitCode", COUNT(m."ModuleId") as modules FROM "Courses" c LEFT JOIN "Modules" m ON c."CourseId" = m."CourseId" GROUP BY c."CourseId", c."UnitCode";'

    Write-Host "`n=== LESSON COUNTS ===" -ForegroundColor Yellow
    psql -h aws-0-ap-southeast-2.pooler.supabase.com -p 5432 -U postgres.blhzpoicleeojjxokztu -d postgres -c 'SELECT c."UnitCode", COUNT(l."LessonId") as lessons FROM "Courses" c LEFT JOIN "Modules" m ON c."CourseId" = m."CourseId" LEFT JOIN "Lessons" l ON m."ModuleId" = l."ModuleId" GROUP BY c."CourseId", c."UnitCode";'

    Write-Host "`n=== MEDIA COUNTS ===" -ForegroundColor Yellow
    psql -h aws-0-ap-southeast-2.pooler.supabase.com -p 5432 -U postgres.blhzpoicleeojjxokztu -d postgres -c 'SELECT c."UnitCode", COUNT(lm."MediaId") as media FROM "Courses" c LEFT JOIN "Modules" m ON c."CourseId" = m."CourseId" LEFT JOIN "Lessons" l ON m."ModuleId" = l."ModuleId" LEFT JOIN "LessonMedia" lm ON l."LessonId" = lm."LessonId" GROUP BY c."CourseId", c."UnitCode";'

    Write-Host "`n=== SAMPLE MEDIA PATHS ===" -ForegroundColor Yellow
    psql -h aws-0-ap-southeast-2.pooler.supabase.com -p 5432 -U postgres.blhzpoicleeojjxokztu -d postgres -c 'SELECT "MediaId", "FilePath", "MediaType" FROM "LessonMedia" LIMIT 10;'

} else {
    Write-Host "psql not found. Using .NET...`n" -ForegroundColor Yellow

    # Use .NET to query
    Add-Type -AssemblyName System.Data

    $conn = New-Object Npgsql.NpgsqlConnection($connectionString)

    try {
        $conn.Open()
        Write-Host "✅ Connected to Supabase!`n" -ForegroundColor Green

        # Query courses
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = 'SELECT "CourseId", "Title", "UnitCode", "Status" FROM "Courses"'
        $reader = $cmd.ExecuteReader()

        Write-Host "=== COURSES IN DATABASE ===" -ForegroundColor Yellow
        while ($reader.Read()) {
            $unitCode = $reader["UnitCode"]
            $title = $reader["Title"]
            $status = $reader["Status"]
            Write-Host "$unitCode - $title ($status)" -ForegroundColor White
        }
        $reader.Close()

        $conn.Close()

    } catch {
        Write-Host "❌ Error: $_" -ForegroundColor Red
        Write-Host "`nTrying alternative method..." -ForegroundColor Yellow

        # Fallback to dotnet run
        $env:ASPNETCORE_ENVIRONMENT = "Production"
        dotnet run -- --list-courses
    }
}

Write-Host "`n✅ Database query complete!`n" -ForegroundColor Green
