using RTOWebLMS.Models;

namespace RTOWebLMS.Data;

/// <summary>
/// Database initialization - creates default tenant for local development
/// </summary>
public static class DbInitializer
{
    public static void Initialize(LmsDbContext context)
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
        context.SaveChanges();

        Console.WriteLine("✅ Default tenant created: 'Default RTO'");
    }
}
