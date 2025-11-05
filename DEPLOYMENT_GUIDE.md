# RTO Web LMS - Deployment Guide

## Quick Start - Deploy to Azure App Service

This guide will help you deploy your Blazor web application so students can access it from anywhere.

---

## Prerequisites

- Azure account (free trial available: https://azure.microsoft.com/free/)
- Your Supabase database is ready (✅ Already complete!)
- Application configured (✅ Already complete!)

---

## Step 1: Build for Production

First, let's test that the application builds correctly:

```bash
cd "C:\Users\nickb\Projects\RTOWebLMS"
dotnet build --configuration Release
```

This should complete without errors.

---

## Step 2: Deploy to Azure (3 Options)

### Option A: Deploy via Azure Portal (Easiest)

#### 1. Create Azure Account
- Go to https://portal.azure.com
- Sign up for free tier (includes $200 credit for 30 days)
- No credit card required for trial

#### 2. Create App Service
1. Click "Create a resource"
2. Search for "Web App"
3. Click "Create"
4. Fill in the details:
   - **Subscription**: Your subscription
   - **Resource Group**: Create new → "RTOWebLMS-RG"
   - **Name**: `rto-web-lms` (this becomes your URL: rto-web-lms.azurewebsites.net)
   - **Publish**: Code
   - **Runtime stack**: .NET 9 (STS)
   - **Operating System**: Windows or Linux
   - **Region**: Australia East (closest to you)
   - **Pricing Plan**:
     - Free (F1) - For testing only
     - Basic (B1) - $13/month - Recommended for production
     - Standard (S1) - $55/month - For higher traffic

5. Click "Review + Create"
6. Click "Create"
7. Wait 2-3 minutes for deployment

#### 3. Deploy Your Application

**Option 3A: Via Visual Studio** (Easiest)
1. Open the project in Visual Studio
2. Right-click on `RTOWebLMS` project
3. Click "Publish"
4. Select "Azure"
5. Select "Azure App Service (Windows)" or "Azure App Service (Linux)"
6. Sign in to your Azure account
7. Select your subscription
8. Select the App Service you created (`rto-web-lms`)
9. Click "Finish"
10. Click "Publish"

**Option 3B: Via Azure CLI**
```bash
# Install Azure CLI if you haven't
# Download from: https://aka.ms/installazurecliwindows

# Login to Azure
az login

# Publish your app
cd "C:\Users\nickb\Projects\RTOWebLMS"
dotnet publish -c Release -o ./publish

# Create a zip file
cd publish
Compress-Archive -Path * -DestinationPath ../app.zip -Force
cd ..

# Deploy to Azure
az webapp deployment source config-zip `
  --resource-group RTOWebLMS-RG `
  --name rto-web-lms `
  --src app.zip
```

**Option 3C: Via GitHub Actions** (Best for continuous deployment)
1. Push your code to GitHub
2. In Azure Portal, go to your App Service
3. Click "Deployment Center"
4. Select "GitHub"
5. Authorize and select your repository
6. Azure will automatically create a GitHub Actions workflow
7. Every push to main will trigger automatic deployment

---

### Option B: Deploy to Railway (Simpler Alternative)

Railway is easier and cheaper for smaller deployments.

#### 1. Create Railway Account
- Go to https://railway.app
- Sign up with GitHub (free)

#### 2. Deploy Your App
1. Click "New Project"
2. Select "Deploy from GitHub repo"
3. Connect your GitHub account
4. Select your repository
5. Railway will automatically:
   - Detect it's a .NET app
   - Build it
   - Deploy it
   - Give you a URL

#### 3. Add Environment Variables
In Railway dashboard:
1. Go to your project
2. Click "Variables"
3. Add:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ```

Cost: ~$5/month

---

### Option C: Deploy to Render

#### 1. Create Render Account
- Go to https://render.com
- Sign up (free)

#### 2. Create Web Service
1. Click "New +" → "Web Service"
2. Connect your GitHub repository
3. Configure:
   - **Name**: rto-web-lms
   - **Environment**: Docker (or .NET if available)
   - **Build Command**: `dotnet publish -c Release -o out`
   - **Start Command**: `dotnet out/RTOWebLMS.dll`
   - **Plan**: Free (for testing) or Starter ($7/month)

#### 3. Add Environment Variable
```
ASPNETCORE_ENVIRONMENT=Production
```

---

## Step 3: Configure Production Environment

After deployment, configure your app:

### Set Environment Variables (Azure)
1. Go to Azure Portal
2. Navigate to your App Service
3. Go to "Configuration" → "Application settings"
4. Click "New application setting"
5. Add:
   ```
   Name: ASPNETCORE_ENVIRONMENT
   Value: Production
   ```
6. Click "Save"
7. Wait for app to restart

Your app will now use `appsettings.Production.json` which points to Supabase!

---

## Step 4: Test Your Deployment

1. Visit your app URL:
   - **Azure**: https://rto-web-lms.azurewebsites.net
   - **Railway**: https://your-app.up.railway.app
   - **Render**: https://rto-web-lms.onrender.com

2. The app should load (even if no data is shown yet)

3. Check the connection to Supabase:
   - Try to create a test user/course
   - Verify data appears in Supabase dashboard

---

## Step 5: Set Up Custom Domain (Optional)

### For Azure:
1. Go to your App Service
2. Click "Custom domains"
3. Click "Add custom domain"
4. Follow the wizard to:
   - Verify domain ownership
   - Add DNS records
   - Enable HTTPS (free SSL certificate)

### For Railway/Render:
- Go to settings → Add custom domain
- Add DNS CNAME record pointing to their servers

---

## Step 6: Enable HTTPS (Automatic)

All platforms provide free SSL certificates:
- **Azure**: Automatically enabled for *.azurewebsites.net
- **Railway**: Automatically enabled
- **Render**: Automatically enabled

Your students will access via secure HTTPS!

---

## Troubleshooting

### App Won't Start
**Check logs:**

**Azure:**
```bash
az webapp log tail --name rto-web-lms --resource-group RTOWebLMS-RG
```

Or in Azure Portal:
- Go to App Service
- Click "Log stream"

**Common Issues:**
1. **Wrong .NET version**: Ensure Azure is set to .NET 9
2. **Connection string error**: Verify Supabase credentials in `appsettings.Production.json`
3. **Build failed**: Check that `dotnet build` works locally first

### Database Connection Failed
1. Check Supabase is online (visit dashboard)
2. Verify connection string in `appsettings.Production.json`
3. Test port 5432 is accessible from Azure
4. Check Supabase firewall settings (should allow all IPs)

### App Loads But No Data
This is normal! You haven't migrated data from SQLite to Supabase yet.
- The app is working correctly
- You can manually create test data
- Or migrate your course data (see [DATA_MIGRATION_STATUS.md](DATA_MIGRATION_STATUS.md))

---

## Cost Breakdown

### Azure App Service
| Tier | Cost/Month | Best For |
|------|------------|----------|
| Free (F1) | $0 | Testing only (sleeps after 20 min) |
| Basic (B1) | ~$13 | Production (1 CPU, 1.75 GB RAM) |
| Basic (B2) | ~$26 | More students (2 CPU, 3.5 GB RAM) |
| Standard (S1) | ~$55 | High traffic + auto-scaling |

### Railway
- $5/month for 512MB RAM
- $20/month for 8GB RAM

### Render
- Free (for testing, sleeps after 15 min)
- $7/month for production

### Supabase (Database)
- Free tier: $0/month (perfect for start)
- Pro tier: $25/month (recommended for production)

### Total Monthly Cost (Recommended Setup)
- Azure Basic B1: $13
- Supabase Pro: $25
- **Total: $38/month** for full production RTO LMS

---

## Security Best Practices

### 1. Don't Commit Secrets
Add to `.gitignore`:
```
appsettings.Production.json
*.publish
```

### 2. Use Environment Variables
Instead of hardcoding connection strings, use environment variables:

In Azure:
1. Go to Configuration → Connection strings
2. Add:
   - **Name**: `DefaultConnection`
   - **Value**: Your Supabase connection string
   - **Type**: PostgreSQL

Update `Program.cs` to read from environment:
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("DefaultConnection");
```

### 3. Enable Authentication
Add student authentication before going live:
- ASP.NET Core Identity
- Or use Supabase Auth
- Prevent unauthorized access

---

## Next Steps After Deployment

1. ✅ **Test the deployed app** - Visit your URL
2. ⏳ **Migrate course data** - Copy from SQLite to Supabase
3. ⏳ **Configure Supabase Storage** - For lesson media files
4. ⏳ **Add authentication** - Secure student access
5. ⏳ **Custom domain** - Use your RTO's domain name
6. ⏳ **Invite students** - Share the URL!

---

## Support Resources

- **Azure Documentation**: https://docs.microsoft.com/azure/app-service/
- **Railway Documentation**: https://docs.railway.app/
- **Render Documentation**: https://render.com/docs
- **Supabase Documentation**: https://supabase.com/docs

---

## Quick Reference Commands

### Build for Production
```bash
cd "C:\Users\nickb\Projects\RTOWebLMS"
dotnet build --configuration Release
```

### Publish Locally
```bash
dotnet publish -c Release -o ./publish
```

### Test Production Configuration Locally
```bash
$env:ASPNETCORE_ENVIRONMENT="Production"
dotnet run
```

### Deploy to Azure
```bash
az webapp deployment source config-zip `
  --resource-group RTOWebLMS-RG `
  --name rto-web-lms `
  --src app.zip
```

---

## You're Almost There!

Your infrastructure is ready:
- ✅ Supabase database configured
- ✅ Application ready for deployment
- ✅ Configuration files set up

Just follow the steps above and your students will be able to access the LMS from anywhere!

**Recommended Path**: Start with Azure Free tier to test, then upgrade to Basic (B1) when you're ready to go live.
