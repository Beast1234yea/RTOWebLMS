using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RTOWebLMS.Data
{
    /// <summary>
    /// Design-time factory for EF Core migrations
    /// Uses SQLite by default for local development
    /// Use --postgres argument for PostgreSQL migrations
    /// </summary>
    public class LmsDbContextFactory : IDesignTimeDbContextFactory<LmsDbContext>
    {
        public LmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();

            // Check if PostgreSQL is requested via environment variable or argument
            var usePostgres = Environment.GetEnvironmentVariable("USE_POSTGRES") == "true" ||
                             (args.Length > 0 && args[0] == "--postgres");

            if (usePostgres)
            {
                // Use PostgreSQL (for production migrations)
                var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException(
                        "PostgreSQL connection string not found. " +
                        "Set ConnectionStrings__DefaultConnection environment variable.");
                }
                optionsBuilder.UseNpgsql(connectionString);
                Console.WriteLine("Using PostgreSQL for migrations");
            }
            else
            {
                // Use SQLite by default (for local development)
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var dbPath = Path.Combine(appDataPath, "RTODesktopLMS", "rto_lms.db");

                // Ensure directory exists
                var directory = Path.GetDirectoryName(dbPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var connectionString = $"Data Source={dbPath}";
                optionsBuilder.UseSqlite(connectionString);
                Console.WriteLine($"Using SQLite for migrations: {dbPath}");
            }

            return new LmsDbContext(optionsBuilder.Options);
        }
    }
}
