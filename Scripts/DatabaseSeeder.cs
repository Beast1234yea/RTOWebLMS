using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RTOWebLMS.Data;
using RTOWebLMS.Models;
using RTOWebLMS.Services;

namespace RTOWebLMS.Scripts;

/// <summary>
/// Database seeding helper - creates default tenant and test users
/// Run once after applying migrations to set up the database
/// </summary>
public class DatabaseSeeder
{
    public static void SeedDatabase(LmsDbContext context)
    {
        Console.WriteLine("🌱 Starting database seeding...");

        // Create default tenant if it doesn't exist
        var defaultTenant = context.Tenants.FirstOrDefault(t => t.Id == "default-tenant");
        if (defaultTenant == null)
        {
            defaultTenant = new Tenant
            {
                Id = "default-tenant",
                TenantId = "default-tenant",
                Name = "Default RTO",
                Subdomain = "localhost",
                RTOCode = "00000",
                Plan = SubscriptionPlan.Professional,
                MaxStudents = 200,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Tenants.Add(defaultTenant);
            context.SaveChanges();

            Console.WriteLine("✅ Created default tenant: 'Default RTO'");
        }
        else
        {
            Console.WriteLine("ℹ️  Default tenant already exists");
        }

        // Display summary
        Console.WriteLine("\n📊 Database Summary:");
        Console.WriteLine($"   Tenants: {context.Tenants.Count()}");
        Console.WriteLine($"   Users: {context.Users.Count()}");
        Console.WriteLine($"   Courses: {context.Courses.Count()}");
        Console.WriteLine();
        Console.WriteLine("✅ Database seeding complete!");
        Console.WriteLine();
        Console.WriteLine("🚀 Next steps:");
        Console.WriteLine("   1. Run the application: dotnet run");
        Console.WriteLine("   2. Navigate to: https://localhost:5001/register");
        Console.WriteLine("   3. Create your admin account");
        Console.WriteLine();
    }

    // Helper method to run seeder from command line
    public static void Main(string[] args)
    {
        Console.WriteLine("=================================================");
        Console.WriteLine("  RTO LMS - Database Seeder");
        Console.WriteLine("=================================================\n");

        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Get database provider
        var databaseProvider = configuration.GetValue<string>("DatabaseProvider") ?? "Sqlite";

        // Build DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();

        if (databaseProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = configuration.GetConnectionString("SqliteConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var dbPath = Path.Combine(appDataPath, "RTODesktopLMS", "rto_lms.db");
                connectionString = $"Data Source={dbPath}";
            }
            optionsBuilder.UseSqlite(connectionString);
            Console.WriteLine($"📁 Using SQLite database: {connectionString}");
        }
        else
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
            Console.WriteLine($"🐘 Using PostgreSQL database");
        }

        Console.WriteLine();

        // Create context and seed
        using (var context = new LmsDbContext(optionsBuilder.Options))
        {
            try
            {
                SeedDatabase(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error seeding database: {ex.Message}");
                Console.WriteLine($"   Stack trace: {ex.StackTrace}");
                Environment.Exit(1);
            }
        }
    }
}
