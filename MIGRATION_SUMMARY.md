# RTO LMS Database Migration Summary

**Date:** November 5, 2025
**Source:** RTODesktopLMS (SQLite)
**Destination:** Supabase PostgreSQL
**Status:** Migration Files Generated - Ready for Manual Application

---

## Overview

This document summarizes the database migration from RTODesktopLMS (SQLite) to RTOWebLMS (Supabase PostgreSQL). The migration process has been completed in two phases:

1. **Schema Migration:** Generated PostgreSQL-compatible database schema
2. **Data Export:** Exported all data from SQLite to PostgreSQL INSERT statements

---

## Migration Files Created

### 1. Schema Migration Script
- **File:** `Migrations/init_supabase.sql`
- **Size:** 16 KB
- **Purpose:** Creates all tables, indexes, and foreign key constraints in PostgreSQL
- **Tables Created:** 18 tables

### 2. Data Export Script
- **File:** `C:\Users\nickb\Projects\DataMigrationTool\data_export.sql`
- **Size:** 470 KB
- **Purpose:** Contains INSERT statements for all data from RTODesktopLMS
- **Total Records:** 110 records

---

## Database Analysis

### Source Database (RTODesktopLMS)
**Location:** `C:\Users\nickb\AppData\Local\RTODesktopLMS\rto_lms.db`
**Size:** 847 KB

### Data Summary by Table

| Table | Record Count | Status |
|-------|--------------|--------|
| Users | 3 | ✓ Exported |
| Courses | 1 | ✓ Exported |
| Modules | 3 | ✓ Exported |
| Lessons | 19 | ✓ Exported |
| Enrollments | 1 | ✓ Exported |
| LessonProgress | 2 | ✓ Exported |
| Certificates | 0 | N/A (Empty) |
| Assessments | 0 | N/A (Empty) |
| Competencies | 0 | N/A (Empty) |
| Quizzes | 1 | ✓ Exported |
| QuizQuestions | 54 | ✓ Exported |
| QuizAnswers | 0 | N/A (Empty) |
| QuizAttempts | 0 | N/A (Empty) |
| UnitySimulations | 0 | N/A (Empty) |
| SimulationResults | 0 | N/A (Empty) |
| Documents | 0 | N/A (Empty) |
| LessonMedia | 1 | ✓ Exported |
| AuditLogs | 25 | ✓ Exported |
| **TOTAL** | **110** | **All Data Exported** |

---

## Database Schema

### Tables Created in PostgreSQL

1. **Users** - Student and instructor user accounts
   - Includes AVETMISS-compliant fields
   - Unique email constraint
   - Supports roles: Student, Instructor, Admin

2. **Courses** - Training units and courses
   - Links to instructor (User)
   - Supports nationally recognised training
   - License tracking capability

3. **Modules** - Course content organization
   - Ordered sections within courses
   - Publication status tracking

4. **Lessons** - Individual learning content
   - Types: Text, Video, Quiz, Simulation, Document
   - Optional quiz and simulation linking
   - Progress tracking support

5. **Enrollments** - Student course enrollments
   - Status tracking (Enrolled, InProgress, Completed, Withdrawn)
   - Progress percentage calculation
   - Payment status tracking
   - AVETMISS date tracking (start, end, completion)

6. **LessonProgress** - Individual lesson completion
   - Time spent tracking
   - Completion status

7. **Certificates** - Qualification certificates
   - Unique certificate numbers
   - Verification codes
   - Expiry tracking
   - PDF URL storage

8. **Assessments** - Student assessments
   - Status tracking (NotStarted, InProgress, Submitted, Graded)
   - Scoring and feedback
   - Grader tracking

9. **Competencies** - Unit competency outcomes
   - Results: Competent, NotYetCompetent, NotAssessed
   - Evidence documentation
   - AVETMISS-compliant

10. **Quizzes** - Knowledge assessments
    - Passing score requirements
    - Time limits
    - Maximum attempts

11. **QuizQuestions** - Quiz question bank
    - Question types: MultipleChoice, TrueFalse, ShortAnswer
    - Point values
    - Order tracking

12. **QuizAnswers** - Multiple choice answer options
    - Correct answer flagging

13. **QuizAttempts** - Student quiz attempts
    - Score tracking
    - Pass/fail status
    - Time spent
    - Answer storage (JSON)

14. **UnitySimulations** - Unity-based practical simulations
    - Build path storage
    - High-risk flagging
    - Version tracking

15. **SimulationResults** - Simulation completion data
    - Score tracking
    - Result data storage (JSON)
    - Completion status

16. **Documents** - Lesson attachments
    - File metadata
    - Path storage

17. **LessonMedia** - Media attachments (images, videos)
    - Media type classification
    - Ordering support

18. **AuditLogs** - System audit trail
    - Action tracking
    - User association
    - IP address logging
    - Severity levels

---

## Key Relationships

- Users → Courses (Instructor)
- Courses → Modules → Lessons
- Users + Courses → Enrollments
- Enrollments + Lessons → LessonProgress
- Users + Courses → Certificates
- Users + Courses → Assessments → Competencies
- Quizzes → QuizQuestions → QuizAnswers
- Users + Quizzes → QuizAttempts
- UnitySimulations + Users → SimulationResults
- Lessons → Documents, LessonMedia, Quizzes, Simulations

---

## Migration Instructions

### Step 1: Verify Supabase Connection

The provided connection string may need to be verified:
```
Host=aws-0-ap-southeast-2.pooler.supabase.com
Port=5432
Database=postgres
Username=postgres.lnoyarfwhcdvvdykhfqo
Password=SkylaHugo2025
```

**Note:** During testing, we encountered authentication errors ("Tenant or user not found"). Please verify:
1. The Supabase project is active
2. The connection string is correct
3. The database user has proper permissions

### Step 2: Apply Schema Migration

1. Open Supabase SQL Editor
2. Copy contents of `Migrations/init_supabase.sql`
3. Execute the script
4. Verify all 18 tables are created

Expected output:
- `__EFMigrationsHistory` table created
- All 18 data tables created
- All indexes created
- All foreign key constraints created

### Step 3: Import Data

1. Open Supabase SQL Editor
2. Copy contents of `C:\Users\nickb\Projects\DataMigrationTool\data_export.sql`
3. Execute the script
4. Verify 110 records are imported

Expected output:
- 3 Users imported
- 1 Course imported (CPCCLDG3001 - License to Perform Dogging)
- 3 Modules imported
- 19 Lessons imported
- 1 Quiz imported with 54 questions
- 1 Enrollment imported
- 2 Lesson Progress records imported
- 1 Lesson Media record imported
- 25 Audit Log entries imported

### Step 4: Verify Migration

Run these verification queries in Supabase:

```sql
-- Count records in each table
SELECT 'Users' as table_name, COUNT(*) as count FROM "Users"
UNION ALL SELECT 'Courses', COUNT(*) FROM "Courses"
UNION ALL SELECT 'Modules', COUNT(*) FROM "Modules"
UNION ALL SELECT 'Lessons', COUNT(*) FROM "Lessons"
UNION ALL SELECT 'Enrollments', COUNT(*) FROM "Enrollments"
UNION ALL SELECT 'LessonProgress', COUNT(*) FROM "LessonProgress"
UNION ALL SELECT 'Quizzes', COUNT(*) FROM "Quizzes"
UNION ALL SELECT 'QuizQuestions', COUNT(*) FROM "QuizQuestions"
UNION ALL SELECT 'LessonMedia', COUNT(*) FROM "LessonMedia"
UNION ALL SELECT 'AuditLogs', COUNT(*) FROM "AuditLogs";

-- Verify foreign key relationships
SELECT c.*, u.Name as InstructorName
FROM "Courses" c
JOIN "Users" u ON c."InstructorId" = u."Id";

SELECT m.*, c.Title as CourseTitle
FROM "Modules" m
JOIN "Courses" c ON m."CourseId" = c."Id";

SELECT l.*, m.Title as ModuleTitle
FROM "Lessons" l
JOIN "Modules" m ON l."ModuleId" = m."Id";
```

Expected verification results:
- Total records should match 110
- All foreign keys should resolve correctly
- No NULL values in required fields

### Step 5: Update Application Configuration

Update `appsettings.Production.json` or environment variables on Railway:

```json
{
  "DatabaseProvider": "PostgreSQL",
  "ConnectionStrings": {
    "DefaultConnection": "Host=aws-0-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.lnoyarfwhcdvvdykhfqo;Password=SkylaHugo2025"
  }
}
```

Or set environment variables:
- `DatabaseProvider=PostgreSQL`
- `ConnectionStrings__DefaultConnection=<your-supabase-connection-string>`

---

## Data Migration Tool

A C# console application was created to facilitate the migration:

**Location:** `C:\Users\nickb\Projects\DataMigrationTool`

### Features:
- Connects to both SQLite and PostgreSQL databases
- Analyzes source database structure
- Exports data to PostgreSQL-compatible SQL
- Maintains referential integrity
- Handles batch processing
- Provides detailed progress reporting

### Usage:
```bash
cd C:\Users\nickb\Projects\DataMigrationTool
dotnet run
```

The tool can be modified and re-run if needed for future data exports.

---

## Current Data Overview

### Users (3)
1. **Default Instructor** (instructor@rto.edu.au) - Instructor role
2. **Test Student** (nickbeashel@hotmail.com) - Student role, STU-2025-0001
3. **Georgia Montgomerie** (student@rto.edu.au) - Student role, STU-2025-0002

### Course (1)
**CPCCLDG3001 - License to Perform Dogging**
- Nationally recognised training
- 3 modules, 19 lessons
- 1 comprehensive written assessment (54 questions)
- Pass requirement: 95%
- Instructor: Default Instructor

### Enrollments (1)
- Student: Georgia Montgomerie (student@rto.edu.au)
- Course: CPCCLDG3001
- Status: In Progress
- Progress: 2 of 19 lessons completed (10.5%)

---

## Important Notes

### Connection String Security
The connection string in `appsettings.Production.json` contains sensitive credentials. This file should:
- **NOT** be committed to git (already in `.gitignore`)
- Be deleted after migration is complete
- Be replaced with environment variables in production (Railway)

### Environment Variables (Railway)
Set these in Railway dashboard:
```
DATABASE_PROVIDER=PostgreSQL
CONNECTION_STRING=<your-supabase-connection-string>
```

### File Cleanup
After successful migration, you can:
1. Delete `appsettings.Production.json` from RTOWebLMS
2. Keep `data_export.sql` as a backup
3. Keep the DataMigrationTool project for future use

---

## Migration Status

### ✅ Completed
- [x] EF Core migration created for PostgreSQL
- [x] Schema SQL script generated (16 KB)
- [x] Data export tool created
- [x] Source database analyzed (110 records)
- [x] Data exported to SQL (470 KB)
- [x] Migration documentation completed

### ⏳ Pending (Manual Steps)
- [ ] Verify Supabase connection string
- [ ] Apply schema migration in Supabase
- [ ] Import data in Supabase
- [ ] Verify data integrity
- [ ] Test application with PostgreSQL
- [ ] Deploy to Railway
- [ ] Delete temporary appsettings.Production.json

---

## Troubleshooting

### Issue: "Tenant or user not found"
This error occurred during our automated connection attempts. Possible solutions:
1. Verify the Supabase project URL and database name
2. Check that the database user exists and has correct permissions
3. Ensure the password is correct
4. Verify the Supabase project is active and not paused

### Issue: Foreign Key Constraint Violations
If you encounter foreign key errors during import:
1. Ensure schema migration completed successfully
2. Verify data is imported in dependency order (Users before Courses, etc.)
3. The export script already handles correct ordering

### Issue: Duplicate Key Errors
If records already exist:
1. Truncate all tables before re-importing
2. Or drop and recreate the schema
3. The migration tool uses GUIDs, so duplicates shouldn't occur on first import

---

## Rollback Plan

If migration fails:
1. The original SQLite database remains unchanged at: `C:\Users\nickb\AppData\Local\RTODesktopLMS\rto_lms.db`
2. RTODesktopLMS application can continue to be used
3. Migration can be re-attempted after addressing issues

---

## Next Steps

1. **Verify Supabase Connection**
   - Log into Supabase dashboard
   - Verify database details
   - Test connection string

2. **Execute Schema Migration**
   - Run `Migrations/init_supabase.sql` in Supabase SQL Editor
   - Verify all tables created

3. **Import Data**
   - Run `data_export.sql` in Supabase SQL Editor
   - Verify 110 records imported

4. **Test Application**
   - Update connection string in RTOWebLMS
   - Run application locally against Supabase
   - Verify all features work

5. **Deploy to Railway**
   - Set environment variables
   - Deploy application
   - Monitor for errors

6. **Cleanup**
   - Remove `appsettings.Production.json`
   - Archive migration files
   - Document any issues encountered

---

## Support

If you encounter issues during migration:
1. Check the troubleshooting section above
2. Verify all connection strings and credentials
3. Review Supabase logs for detailed error messages
4. Ensure EF Core migrations are up to date

---

**Migration Prepared By:** Claude Code
**Date:** 2025-11-05
**Files Generated:**
- `C:\Users\nickb\Projects\RTOWebLMS\Migrations\init_supabase.sql`
- `C:\Users\nickb\Projects\DataMigrationTool\data_export.sql`
