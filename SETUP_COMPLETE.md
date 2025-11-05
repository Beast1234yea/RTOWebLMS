# RTOWebLMS Setup Complete

## What's Been Done

Your Blazor web application is now fully configured for cloud deployment with Supabase!

### Files Created/Modified

1. **[appsettings.json](appsettings.json)** - Development configuration (uses SQLite)
2. **[appsettings.Production.json](appsettings.Production.json)** - Production configuration (ready for PostgreSQL/Supabase)
3. **[Program.cs](Program.cs)** - Updated to support both SQLite and PostgreSQL
4. **[SUPABASE_SETUP.md](SUPABASE_SETUP.md)** - Comprehensive Supabase setup guide

### What's Working

- **Web application running at**: http://localhost:5059
- **Database**: Currently using SQLite (same database as desktop app)
- **Pages**:
  - `/` - Home page
  - `/my-learning` - Student dashboard with courses and progress
  - `/lesson/{id}` - Lesson viewer with media slideshow

### Architecture

```
┌─────────────────────────────────────────────────┐
│          RTOWebLMS (Blazor Server)              │
│                                                 │
│  ┌──────────────┐        ┌──────────────┐     │
│  │ appsettings  │        │   Program.cs │     │
│  │    .json     │───────▶│              │     │
│  │              │        │  Reads:      │     │
│  │ Provider:    │        │  - Provider  │     │
│  │   "Sqlite"   │        │  - ConnStr   │     │
│  └──────────────┘        └──────┬───────┘     │
│                                 │             │
│                          ┌──────▼──────┐      │
│                          │ DbContext   │      │
│                          │             │      │
│                          │ UseSqlite() │      │
│                          └──────┬──────┘      │
└──────────────────────────────────┼────────────┘
                                  │
                                  ▼
                          ┌──────────────┐
                          │   SQLite     │
                          │   Local DB   │
                          └──────────────┘
```

### When You Switch to Supabase

```
┌─────────────────────────────────────────────────┐
│       RTOWebLMS (Production/Cloud)              │
│                                                 │
│  ┌──────────────┐        ┌──────────────┐     │
│  │appsettings   │        │   Program.cs │     │
│  │.Production   │───────▶│              │     │
│  │   .json      │        │  Reads:      │     │
│  │              │        │  - Provider  │     │
│  │ Provider:    │        │  - ConnStr   │     │
│  │ "PostgreSQL" │        └──────┬───────┘     │
│  └──────────────┘               │             │
│                          ┌──────▼──────┐      │
│                          │ DbContext   │      │
│                          │             │      │
│                          │ UseNpgsql() │      │
│                          └──────┬──────┘      │
└──────────────────────────────────┼────────────┘
                                  │
                                  ▼ (HTTPS/SSL)
                          ┌──────────────┐
                          │   Supabase   │
                          │  PostgreSQL  │
                          │   (Cloud)    │
                          └──────────────┘
```

## Cost Breakdown

### Free Tier ($0/month) - Great for Testing
- 500MB database
- 1GB file storage
- 50K monthly active users
- Perfect for: Testing, pilot programs, small classes

### Pro Tier ($25/month) - Recommended for Production
- 8GB database
- 100GB file storage
- 100K monthly active users
- Daily backups
- Perfect for: Full RTO deployment

### Additional Costs (If Needed)
- Extra storage: ~$0.021/GB/month
- Extra database space: ~$0.125/GB/month
- Bandwidth: Free tier includes plenty for typical use

## Current Status

### ✅ Completed
- Blazor web application created
- PostgreSQL provider (Npgsql v9.0.4) installed
- Program.cs configured for database provider switching
- Configuration files ready for both SQLite and PostgreSQL
- MyLearning page with course dashboard
- LessonDetail page with media slideshow
- Colorful UI matching desktop application
- Setup documentation created

### ⏳ Next Steps (When Ready)

1. **Create Supabase Account**
   - Go to https://supabase.com
   - Sign up and create project named "RTO-LMS"
   - See [SUPABASE_SETUP.md](SUPABASE_SETUP.md) for detailed steps

2. **Get Connection String**
   - Copy from Supabase dashboard
   - Update `appsettings.Production.json`

3. **Run Database Migration**
   ```bash
   cd C:\Users\nickb\Projects\RTOWebLMS
   dotnet ef migrations add InitialCreate
   dotnet ef database update --configuration Production
   ```

4. **Configure Storage**
   - Create buckets in Supabase for lesson media
   - Upload Unity WebGL builds

5. **Deploy to Cloud**
   - Azure App Service (recommended)
   - Or Railway/Render/AWS

## Testing Locally

Your web app is currently running at **http://localhost:5059**

### Switch Between Databases

**Use SQLite (Local Development):**
```json
// In appsettings.json
"DatabaseProvider": "Sqlite"
```

**Use PostgreSQL (Test Cloud Connection):**
```json
// In appsettings.json
"DatabaseProvider": "PostgreSQL"
```
(Make sure to add real Supabase connection string first!)

## What Makes This Setup Great

1. **Flexible**: Develop locally with SQLite, deploy to cloud with PostgreSQL
2. **No Code Changes**: Just change configuration to switch databases
3. **Cost Effective**: Free tier perfect for testing
4. **Scalable**: Easy to upgrade as you grow
5. **Remote Access**: Students can access from anywhere
6. **Shared Database**: All students see same content and progress

## Files You'll Need to Update

When you get your Supabase connection string, update **only** this file:

**appsettings.Production.json**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=YOUR_ACTUAL_SUPABASE_HOST.supabase.co;Database=postgres;Username=postgres;Password=YOUR_ACTUAL_PASSWORD;SSL Mode=Require;Trust Server Certificate=true"
  },
  "DatabaseProvider": "PostgreSQL"
}
```

**DO NOT** commit this file with real password to Git!

## Questions?

Refer to [SUPABASE_SETUP.md](SUPABASE_SETUP.md) for:
- Step-by-step Supabase account creation
- How to get connection string
- How to run migrations
- How to configure storage
- Deployment options

## Ready to Go!

Everything is set up and ready. When you're ready to enable remote access:

1. Create Supabase account (5 minutes)
2. Update connection string (2 minutes)
3. Run migration (1 minute)
4. Deploy to cloud hosting (30-60 minutes)

Total time to go live: **~1 hour**

Your students will then be able to access the LMS from home via their web browsers!
