using RTOWebLMS.Models;
using RTOWebLMS.Utilities;
using Microsoft.AspNetCore.Identity;

namespace RTOWebLMS.Data;

/// <summary>
/// Database initialization - creates default tenant and admin user for local development
/// </summary>
public static class DbInitializer
{
    public static async Task InitializeAsync(LmsDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Setup paths
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var dbPath = Path.Combine(appDataPath, "RTODesktopLMS", "rto_lms.db");
        var backupPath = Path.Combine(appDataPath, "RTODesktopLMS", "backup");
        string? backupFile = null;

        // Check if database exists and if it's the old schema (without Identity)
        if (File.Exists(dbPath))
        {
            try
            {
                // Try to connect and check if it has Identity tables
                var hasIdentityTables = context.Database.CanConnect() &&
                    context.Database.ExecuteSqlRaw("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='AspNetUsers'") > 0;

                if (!hasIdentityTables)
                {
                    Console.WriteLine("🔍 Detected old database schema (without Identity)");

                    // Create backup before deleting
                    Directory.CreateDirectory(backupPath);
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    backupFile = Path.Combine(backupPath, $"rto_lms_backup_{timestamp}.db");

                    Console.WriteLine($"📦 Creating backup: {backupFile}");
                    File.Copy(dbPath, backupFile, overwrite: true);
                    Console.WriteLine("✅ Backup created successfully");

                    // Delete old database so we can create new one with Identity
                    Console.WriteLine("🗑️  Deleting old database...");
                    File.Delete(dbPath);
                    Console.WriteLine("✅ Old database deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Warning: Could not check database schema: {ex.Message}");
            }
        }

        // Ensure database is created (will create new one if we deleted the old one)
        context.Database.EnsureCreated();

        // Check if default tenant exists, create if not
        if (!context.Tenants.Any(t => t.Id == "default-tenant"))
        {
            var defaultTenant = new Tenant
            {
                Id = "default-tenant",
                TenantId = "default-tenant",
                Name = "Default RTO",
                Subdomain = "localhost",
                RTOCode = "00000",
                ABN = "00 000 000 000",
                Plan = SubscriptionPlan.Professional,
                MaxStudents = 200,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Tenants.Add(defaultTenant);
            await context.SaveChangesAsync();
            Console.WriteLine("✅ Default tenant created: 'Default RTO'");
        }
        else
        {
            Console.WriteLine("ℹ️  Default tenant already exists");
        }

        // Always ensure roles exist (even if tenant already existed)
        string[] roleNames = { "Admin", "Instructor", "Student" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"✅ Role created: '{roleName}'");
            }
        }

        // Always check and create admin user if needed (even if tenant already existed)
        var adminEmail = "admin@localhost.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                TenantId = "default-tenant",
                FirstName = "Admin",
                LastName = "User",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                Console.WriteLine("✅ Default admin user created:");
                Console.WriteLine($"   Email: {adminEmail}");
                Console.WriteLine($"   Password: Admin123!");
            }
            else
            {
                Console.WriteLine("❌ Failed to create admin user:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"   - {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("ℹ️  Admin user already exists");
        }

        // Migrate data from backup if we created one
        if (backupFile != null && File.Exists(backupFile))
        {
            Console.WriteLine("\n🔄 Migrating data from old database...");

            // Check if we already have courses (migration already done)
            var existingCourseCount = context.Courses.Count();
            if (existingCourseCount == 0)
            {
                // Run migration
                await DataMigrationUtility.MigrateDataAsync(backupFile, context);
                Console.WriteLine("✅ Migration completed successfully!");
                Console.WriteLine($"💾 Backup saved at: {backupFile}");
            }
            else
            {
                Console.WriteLine($"ℹ️  Migration skipped - database already has {existingCourseCount} courses");
            }
        }
    }
}
