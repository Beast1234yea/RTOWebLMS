# Database Migration Files

## Migration Files to Execute

### 1. Schema Migration (Execute First)
**File:** `C:\Users\nickb\Projects\RTOWebLMS\Migrations\init_supabase.sql`
- **Size:** 16 KB
- **Purpose:** Creates all database tables, indexes, and constraints
- **Execute in:** Supabase SQL Editor
- **Execution time:** ~2 seconds

### 2. Data Import (Execute Second)
**File:** `C:\Users\nickb\Projects\DataMigrationTool\data_export.sql`
- **Size:** 470 KB
- **Purpose:** Imports all 110 records from RTODesktopLMS
- **Execute in:** Supabase SQL Editor
- **Execution time:** ~5 seconds

## Documentation Files

### Quick Start Guide
**File:** `C:\Users\nickb\Projects\RTOWebLMS\MIGRATION_QUICKSTART.md`
- **Purpose:** Step-by-step instructions for applying the migration
- **Audience:** Developers performing the migration

### Complete Migration Summary
**File:** `C:\Users\nickb\Projects\RTOWebLMS\MIGRATION_SUMMARY.md`
- **Purpose:** Complete documentation of migration process, data analysis, and verification steps
- **Audience:** Technical stakeholders, future reference

## Source Code Files

### Migration Tool (Reusable)
**Location:** `C:\Users\nickb\Projects\DataMigrationTool\`

Files:
- `Program.cs` (3.4 KB) - Main application entry point
- `DataExporter.cs` (18 KB) - Data export logic for all tables
- `ExportProgram.cs` (1.4 KB) - Alternative entry point
- `DataMigrationTool.csproj` - Project file with dependencies

**Purpose:** C# console application for exporting SQLite data to PostgreSQL SQL format

**Usage:**
```bash
cd C:\Users\nickb\Projects\DataMigrationTool
dotnet run
```

**Features:**
- Connects to RTODesktopLMS SQLite database
- Analyzes data counts for all tables
- Exports data as PostgreSQL-compatible INSERT statements
- Maintains referential integrity with correct insert order
- Handles NULL values correctly
- Escapes special characters in strings

### EF Core Migration Files
**Location:** `C:\Users\nickb\Projects\RTOWebLMS\Migrations\`

Files:
- `20251105043951_InitialCreate.cs` (42 KB) - Migration class
- `20251105043951_InitialCreate.Designer.cs` (46 KB) - Migration metadata
- `LmsDbContextModelSnapshot.cs` (46 KB) - Current model snapshot
- `init_supabase.sql` (16 KB) - Generated SQL script

**Purpose:** EF Core migration for creating PostgreSQL database schema

### Design-Time Factory
**File:** `C:\Users\nickb\Projects\RTOWebLMS\Data\LmsDbContextFactory.cs`
- **Purpose:** Ensures EF Core migrations generate PostgreSQL-compatible code
- **Note:** Uses dummy connection string for design-time only

## Configuration Files

### Production Configuration (Temporary)
**File:** `C:\Users\nickb\Projects\RTOWebLMS\appsettings.Production.json`
- **Purpose:** Contains Supabase connection string for migration testing
- **IMPORTANT:** Should be deleted after migration (already in .gitignore)
- **Do not commit to git**

### Original SQLite Database (Preserved)
**File:** `C:\Users\nickb\AppData\Local\RTODesktopLMS\rto_lms.db`
- **Size:** 847 KB
- **Purpose:** Original data source (preserved unchanged)
- **Records:** 110 total records
- **Status:** Unchanged, can be used for rollback

## File Checklist

Before migration, verify these files exist:

- [x] `RTOWebLMS/Migrations/init_supabase.sql` (16 KB)
- [x] `DataMigrationTool/data_export.sql` (470 KB)
- [x] `RTOWebLMS/MIGRATION_QUICKSTART.md`
- [x] `RTOWebLMS/MIGRATION_SUMMARY.md`
- [x] `RTOWebLMS/MIGRATION_FILES.md` (this file)

## After Migration

### Files to Keep
- Migration documentation (MIGRATION_*.md)
- `data_export.sql` (as backup)
- `init_supabase.sql` (for reference)
- DataMigrationTool project (for future use)
- Original SQLite database (for rollback)

### Files to Delete
- `appsettings.Production.json` (after moving to environment variables)

### Files to Update
- `appsettings.json` or Railway environment variables with production connection string

## Backup Recommendations

1. **Before Migration:**
   - Keep original SQLite database
   - Save copies of both SQL files
   - Document current Supabase state (if any)

2. **After Migration:**
   - Export Supabase database dump
   - Keep migration SQL files
   - Document migration date and time

## Version Information

- **EF Core Version:** 9.0.10
- **Npgsql Version:** 9.0.4
- **PostgreSQL Version:** 14+ (Supabase)
- **SQLite Version:** 3.x
- **.NET Version:** 9.0

## Summary

| Item | Count |
|------|-------|
| SQL Files to Execute | 2 |
| Documentation Files | 3 |
| Source Code Projects | 1 |
| Total Records to Migrate | 110 |
| Total Tables | 18 |
| Estimated Migration Time | 5 minutes |

---

**Created:** 2025-11-05
**Purpose:** Database migration from RTODesktopLMS (SQLite) to RTOWebLMS (Supabase PostgreSQL)
