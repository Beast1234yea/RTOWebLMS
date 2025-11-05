using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RTOWebLMS.Data
{
    /// <summary>
    /// Design-time factory for EF Core migrations
    /// This ensures migrations are created for PostgreSQL
    /// </summary>
    public class LmsDbContextFactory : IDesignTimeDbContextFactory<LmsDbContext>
    {
        public LmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();

            // Use a dummy PostgreSQL connection string for migration generation
            // The actual connection string will be provided at runtime via appsettings
            optionsBuilder.UseNpgsql("Host=localhost;Database=dummy;Username=postgres;Password=postgres");

            return new LmsDbContext(optionsBuilder.Options);
        }
    }
}
