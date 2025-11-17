using RTOWebLMS.Models;
using Microsoft.AspNetCore.Identity;

namespace RTOWebLMS.Data;

/// <summary>
/// Database initialization - creates default tenant and admin user for local development
/// </summary>
public static class DbInitializer
{
    public static async Task InitializeAsync(LmsDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if default tenant already exists
        if (context.Tenants.Any(t => t.Id == "default-tenant"))
        {
            return; // Database already seeded
        }

        // Create default tenant
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

        // Create roles
        string[] roleNames = { "Admin", "Instructor", "Student" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"✅ Role created: '{roleName}'");
            }
        }

        // Create default admin user
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
    }
}
