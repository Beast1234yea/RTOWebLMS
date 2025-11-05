# Check BOTH Supabase databases to see which one has data

Write-Host "`n=== CHECKING BOTH SUPABASE DATABASES ===`n" -ForegroundColor Cyan

# Database 1 (current in appsettings)
$db1_host = "aws-0-ap-southeast-2.pooler.supabase.com"
$db1_user = "postgres.blhzpoicleeojjxokztu"
$db1_pass = "SkylaHugo2025"

# Database 2 (newly shared)
$db2_host = "aws-1-ap-southeast-2.pooler.supabase.com"
$db2_user = "postgres.dequkghvbcqqjoiwbltv"
Write-Host "DATABASE 2 PASSWORD NEEDED!" -ForegroundColor Red
$db2_pass = Read-Host "Enter password for Database 2"

Write-Host "`n--- DATABASE 1 (blhzpoicleeojjxokztu) ---" -ForegroundColor Yellow

if (Get-Command psql -ErrorAction SilentlyContinue) {
    $env:PGPASSWORD = $db1_pass

    Write-Host "Courses:" -ForegroundColor Green
    psql -h $db1_host -p 5432 -U $db1_user -d postgres -c 'SELECT "UnitCode", "Title" FROM "Courses";' 2>&1

    Write-Host "`nCourse counts:" -ForegroundColor Green
    psql -h $db1_host -p 5432 -U $db1_user -d postgres -t -c 'SELECT COUNT(*) FROM "Courses";' 2>&1

    Write-Host "`n--- DATABASE 2 (dequkghvbcqqjoiwbltv) ---" -ForegroundColor Yellow

    $env:PGPASSWORD = $db2_pass

    Write-Host "Courses:" -ForegroundColor Green
    psql -h $db2_host -p 5432 -U $db2_user -d postgres -c 'SELECT "UnitCode", "Title" FROM "Courses";' 2>&1

    Write-Host "`nCourse counts:" -ForegroundColor Green
    psql -h $db2_host -p 5432 -U $db2_user -d postgres -t -c 'SELECT COUNT(*) FROM "Courses";' 2>&1

} else {
    Write-Host "psql not found. Please check manually in Supabase dashboard." -ForegroundColor Red
}

Write-Host "`n=== DONE ===`n" -ForegroundColor Cyan
