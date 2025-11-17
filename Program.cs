using RTOWebLMS.Components;
using RTOWebLMS.Data;
using RTOWebLMS.Services;
using RTOWebLMS.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register application services
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuditLogService>();

// Register multi-tenancy services
builder.Services.AddScoped<ITenantService, TenantService>();

// Configure database - support both SQLite (development) and PostgreSQL (production)
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";

builder.Services.AddDbContext<LmsDbContext>(options =>
{
    if (databaseProvider.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase))
    {
        // Use PostgreSQL (Supabase) for production
        // Try environment variable first, then appsettings
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (string.IsNullOrEmpty(connectionString) || connectionString.Contains("PLACEHOLDER"))
        {
            throw new InvalidOperationException(
                "PostgreSQL connection string is not configured. " +
                "Set 'ConnectionStrings__DefaultConnection' environment variable.");
        }

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Resolve tenant before any auth/processing
app.UseTenantResolution();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

try
{
    Console.WriteLine("[DEBUG] About to call app.Run()...");
    app.Run();
    Console.WriteLine("[DEBUG] app.Run() returned normally");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR] Exception in app.Run(): {ex.GetType().Name}");
    Console.WriteLine($"[ERROR] Message: {ex.Message}");
    Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
    throw;
}
