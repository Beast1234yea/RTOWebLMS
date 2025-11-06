using RTOWebLMS.Components;
using RTOWebLMS.Data;
using RTOWebLMS.Services;
using RTOWebLMS.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register application services
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuditLogService>();

// Configure database - support both SQLite (development) and PostgreSQL (production)
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";

builder.Services.AddDbContext<LmsDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        // Use PostgreSQL (Supabase) for production
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseNpgsql(connectionString);
    }
    else
    {
        // Use SQLite for local development
        var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            // Fallback to default SQLite path
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(appDataPath, "RTODesktopLMS", "rto_lms.db");
            connectionString = $"Data Source={dbPath}";
        }
        options.UseSqlite(connectionString);
    }
});

// Configure URLs for Railway deployment (uses PORT env variable)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

try
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseAntiforgery();

    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    // Seed test data on startup (SQLite only)
    try
    {
        Console.WriteLine("🔧 Starting database initialization...");
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<LmsDbContext>();
            var provider = app.Configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";
            Console.WriteLine($"📊 Database Provider: {provider}");
            
            if (provider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
            {
                // Ensure database is created
                Console.WriteLine("📊 Creating SQLite database if it doesn't exist...");
                await dbContext.Database.EnsureCreatedAsync();
                Console.WriteLine("✅ Database ready");
                
                // Seed test data
                Console.WriteLine("🌱 Seeding test data...");
                await SeedTestData.SeedAsync(dbContext);
                Console.WriteLine("✅ Test data seeded successfully");
            }
        }
        Console.WriteLine("✅ Initialization complete!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Database initialization failed: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        throw;
    }

    Console.WriteLine("🚀 Starting application...");
    app.Run();
    Console.WriteLine("✅ Application exited normally");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ FATAL ERROR: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
    throw;
}
