# Supabase Setup Guide for RTO Web LMS

## Overview

Your web application is now configured to support both **SQLite** (for local development) and **PostgreSQL** (for production with Supabase). This gives you the flexibility to develop locally and deploy to the cloud seamlessly.

## Pricing Information

### Supabase Free Tier (Perfect to Start)
- **Cost**: $0/month
- **Database**: 500MB PostgreSQL
- **Storage**: 1GB for files (lesson media, Unity WebGL builds)
- **Users**: Up to 50K monthly active users
- **Bandwidth**: 2GB
- **Best for**: Testing, development, small pilot programs

### Supabase Pro Tier (Recommended for Production)
- **Cost**: $25/month
- **Database**: 8GB PostgreSQL (expandable)
- **Storage**: 100GB for files
- **Users**: 100K monthly active users
- **Bandwidth**: 50GB
- **Additional**: Daily backups, point-in-time recovery
- **Best for**: Full RTO deployment with multiple courses and students

## Step 1: Create Supabase Account

1. Go to [https://supabase.com](https://supabase.com)
2. Click "Start your project" or "Sign Up"
3. Sign up with GitHub, Google, or email
4. Verify your email if required

## Step 2: Create New Project

1. Once logged in, click "New Project"
2. Fill in the project details:
   - **Organization**: Create new or select existing
   - **Name**: `RTO-LMS` (or your preferred name)
   - **Database Password**: Choose a strong password and **SAVE IT SECURELY**
   - **Region**: Choose closest to Australia:
     - `ap-southeast-1` (Singapore)
     - `ap-southeast-2` (Sydney) - if available
     - `ap-northeast-1` (Tokyo)
   - **Pricing Plan**: Start with "Free" tier
3. Click "Create new project"
4. Wait 2-3 minutes for provisioning to complete

## Step 3: Get Your Connection String

1. In your Supabase project dashboard, click on the **Settings** icon (gear) in the left sidebar
2. Click on **Database** under Settings
3. Scroll down to **Connection string** section
4. Select the **URI** tab (not the Session mode)
5. Click "Copy" to copy the connection string
6. It will look like:
   ```
   postgresql://postgres:[YOUR-PASSWORD]@db.xxxxxxxxxxxxxx.supabase.co:5432/postgres
   ```
7. Replace `[YOUR-PASSWORD]` with the database password you created in Step 2

### Convert to Npgsql Format

The connection string needs to be in Npgsql format. Convert it like this:

**From (Supabase format):**
```
postgresql://postgres:YourPassword123@db.abcdefghijklmnop.supabase.co:5432/postgres
```

**To (Npgsql format):**
```
Host=db.abcdefghijklmnop.supabase.co;Database=postgres;Username=postgres;Password=YourPassword123;SSL Mode=Require;Trust Server Certificate=true
```

## Step 4: Configure Your Application

### For Local Development (Keep using SQLite)

Your `appsettings.json` is already configured to use SQLite by default. No changes needed for local development.

```json
"DatabaseProvider": "Sqlite"
```

### For Production Deployment

When you're ready to deploy, update `appsettings.Production.json`:

1. Open `appsettings.Production.json`
2. Replace the placeholder connection string with your real Supabase connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=db.YOUR_PROJECT_ID.supabase.co;Database=postgres;Username=postgres;Password=YOUR_ACTUAL_PASSWORD;SSL Mode=Require;Trust Server Certificate=true"
  },
  "DatabaseProvider": "PostgreSQL"
}
```

## Step 5: Create and Run Database Migration

Once your Supabase project is ready and connection string is configured:

### Option A: Test Connection First

```bash
cd C:\Users\nickb\Projects\RTOWebLMS
dotnet ef dbcontext info --configuration Production
```

### Option B: Create Initial Migration

```bash
cd C:\Users\nickb\Projects\RTOWebLMS
dotnet ef migrations add InitialCreate --context LmsDbContext
```

### Option C: Apply Migration to Supabase

```bash
# Make sure to set environment to Production or update connection string temporarily
dotnet ef database update --configuration Production
```

### Option D: Migrate Existing SQLite Data (If Needed)

If you have existing data in SQLite that you want to migrate to PostgreSQL:

1. Export data from SQLite
2. Use a migration script or tool like `pgloader`
3. Or manually seed the PostgreSQL database

## Step 6: Configure Supabase Storage (For Lesson Media)

For lesson images and Unity WebGL builds:

1. In Supabase dashboard, click **Storage** in left sidebar
2. Click "Create a new bucket"
3. Create buckets:
   - **Name**: `lesson-media`
   - **Public**: Yes (for lesson images)
   - **File size limit**: 50MB

   - **Name**: `unity-builds`
   - **Public**: Yes (for Unity WebGL games)
   - **File size limit**: 500MB

4. Upload your lesson media files
5. Get the public URL format:
   ```
   https://[PROJECT_ID].supabase.co/storage/v1/object/public/lesson-media/[FILE_NAME]
   ```

## Step 7: Deploy Your Application

### Option 1: Azure App Service (Recommended)
- Best integration with .NET
- Easy deployment from Visual Studio or GitHub
- Automatic SSL certificates
- Cost: ~$13-50/month depending on plan

### Option 2: AWS Elastic Beanstalk
- Good .NET support
- More complex but highly scalable
- Cost: Pay as you go

### Option 3: Railway / Render
- Simpler deployment
- Good for smaller apps
- Cost: $5-20/month

## How It Works

### Development Flow
1. Run locally with SQLite: `dotnet run`
2. Application reads `DatabaseProvider: "Sqlite"` from appsettings.json
3. Uses local SQLite database for quick development

### Production Flow
1. Deploy to cloud hosting (Azure, AWS, etc.)
2. Application runs in Production environment
3. Reads `DatabaseProvider: "PostgreSQL"` from appsettings.Production.json
4. Connects to Supabase PostgreSQL database
5. All students access the same cloud database from anywhere

## Current Application Status

- ✅ Blazor Web Application created
- ✅ PostgreSQL provider (Npgsql) installed
- ✅ Configuration files ready for both SQLite and PostgreSQL
- ✅ Program.cs configured to switch between databases
- ✅ MyLearning and LessonDetail pages with colorful UI
- ⏳ Waiting for Supabase project creation
- ⏳ Database migration to be run
- ⏳ Storage configuration
- ⏳ Deployment setup

## Next Steps

1. **Create your Supabase account and project** (Steps 1-2 above)
2. **Get your connection string** (Step 3 above)
3. **Update appsettings.Production.json** with real connection string (Step 4)
4. **Run database migrations** to create tables in Supabase (Step 5)
5. **Configure Supabase Storage** for lesson media (Step 6)
6. **Deploy to cloud hosting** (Step 7)

## Testing Locally with PostgreSQL (Optional)

If you want to test with PostgreSQL locally before deploying:

1. Update `appsettings.json`:
   ```json
   "DatabaseProvider": "PostgreSQL"
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. It will connect to Supabase instead of SQLite

4. Switch back to SQLite when done:
   ```json
   "DatabaseProvider": "Sqlite"
   ```

## Support and Documentation

- **Supabase Docs**: https://supabase.com/docs
- **Npgsql Docs**: https://www.npgsql.org/doc/
- **EF Core Docs**: https://learn.microsoft.com/en-us/ef/core/
- **Blazor Docs**: https://learn.microsoft.com/en-us/aspnet/core/blazor/

## Security Notes

- ✅ Never commit appsettings.Production.json with real passwords to Git
- ✅ Use environment variables for production connection strings
- ✅ Enable Row Level Security (RLS) in Supabase for data protection
- ✅ Implement authentication before deployment
- ✅ Use HTTPS only in production
