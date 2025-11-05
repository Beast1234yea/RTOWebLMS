# Quick Start: Apply Database Migration to Supabase

## Files You Need

1. **Schema Migration:** `C:\Users\nickb\Projects\RTOWebLMS\Migrations\init_supabase.sql` (16 KB)
2. **Data Import:** `C:\Users\nickb\Projects\DataMigrationTool\data_export.sql` (470 KB)

## 3-Step Migration Process

### Step 1: Apply Schema (2 minutes)

1. Open [Supabase SQL Editor](https://supabase.com/dashboard/project/_/sql)
2. Copy entire contents of `Migrations/init_supabase.sql`
3. Paste and click "Run"
4. Wait for "Success" message
5. Verify 18 tables created

### Step 2: Import Data (2 minutes)

1. In Supabase SQL Editor
2. Copy entire contents of `data_export.sql`
3. Paste and click "Run"
4. Wait for "Success" message
5. Verify 110 rows inserted

### Step 3: Verify (1 minute)

Run this query in Supabase SQL Editor:

```sql
SELECT
    'Users' as table_name, COUNT(*) as count FROM "Users"
UNION ALL SELECT 'Courses', COUNT(*) FROM "Courses"
UNION ALL SELECT 'Modules', COUNT(*) FROM "Modules"
UNION ALL SELECT 'Lessons', COUNT(*) FROM "Lessons"
UNION ALL SELECT 'Enrollments', COUNT(*) FROM "Enrollments"
UNION ALL SELECT 'LessonProgress', COUNT(*) FROM "LessonProgress"
UNION ALL SELECT 'Quizzes', COUNT(*) FROM "Quizzes"
UNION ALL SELECT 'QuizQuestions', COUNT(*) FROM "QuizQuestions"
UNION ALL SELECT 'AuditLogs', COUNT(*) FROM "AuditLogs";
```

Expected results:
- Users: 3
- Courses: 1
- Modules: 3
- Lessons: 19
- Enrollments: 1
- LessonProgress: 2
- Quizzes: 1
- QuizQuestions: 54
- AuditLogs: 25

## Connection String

Update your application to use Supabase:

**Option A: appsettings.Production.json** (temporary, for testing)
```json
{
  "DatabaseProvider": "PostgreSQL",
  "ConnectionStrings": {
    "DefaultConnection": "Host=aws-0-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.lnoyarfwhcdvvdykhfqo;Password=SkylaHugo2025"
  }
}
```

**Option B: Railway Environment Variables** (recommended)
```
DatabaseProvider=PostgreSQL
ConnectionStrings__DefaultConnection=Host=aws-0-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.lnoyarfwhcdvvdykhfqo;Password=SkylaHugo2025
```

## Testing

Run the application:
```bash
cd C:\Users\nickb\Projects\RTOWebLMS
set ASPNETCORE_ENVIRONMENT=Production
dotnet run
```

Navigate to:
- http://localhost:8080
- Login with: student@rto.edu.au / (password from database)

## Troubleshooting

**Error: "Tenant or user not found"**
- Verify Supabase project is active
- Check connection string is correct
- Ensure database user has permissions

**Error: "Table already exists"**
- Schema was already applied
- Skip to Step 2 (Import Data)

**Error: "Duplicate key violation"**
- Data was already imported
- Migration complete!

## What Was Migrated?

- ✓ 3 Users (1 instructor, 2 students)
- ✓ 1 Course (CPCCLDG3001 - Dogging)
- ✓ 3 Modules
- ✓ 19 Lessons
- ✓ 1 Quiz with 54 questions
- ✓ 1 Enrollment (Georgia Montgomerie)
- ✓ 2 Lesson progress records
- ✓ 1 Lesson media item
- ✓ 25 Audit log entries

**Total: 110 records successfully exported**

## Need More Details?

See `MIGRATION_SUMMARY.md` for complete documentation.
