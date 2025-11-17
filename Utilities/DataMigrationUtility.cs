using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Models;

namespace RTOWebLMS.Utilities;

/// <summary>
/// Utility to migrate data from old database (without Identity) to new database (with Identity)
/// </summary>
public class DataMigrationUtility
{
    public static async Task MigrateDataAsync(string oldDbPath, LmsDbContext newDbContext)
    {
        Console.WriteLine("🔄 Starting data migration...");
        Console.WriteLine($"📂 Old database: {oldDbPath}");

        // Create options for old database connection
        var optionsBuilder = new DbContextOptionsBuilder<LmsDbContext>();
        optionsBuilder.UseSqlite($"Data Source={oldDbPath}");

        using var oldDbContext = new LmsDbContext(optionsBuilder.Options);

        try
        {
            // Check if old database has data
            var oldCourseCount = await oldDbContext.Courses.CountAsync();
            Console.WriteLine($"📊 Found {oldCourseCount} courses in old database");

            if (oldCourseCount == 0)
            {
                Console.WriteLine("ℹ️  No courses to migrate");
                return;
            }

            // Get the default tenant from new database
            var defaultTenant = await newDbContext.Tenants.FirstOrDefaultAsync(t => t.Id == "default-tenant");
            if (defaultTenant == null)
            {
                Console.WriteLine("❌ Default tenant not found in new database. Run DbInitializer first.");
                return;
            }

            Console.WriteLine($"✅ Target tenant: {defaultTenant.Name}");

            // Migrate Courses
            var oldCourses = await oldDbContext.Courses
                .Include(c => c.Modules)
                    .ThenInclude(m => m.Lessons)
                .ToListAsync();

            Console.WriteLine($"🔄 Migrating {oldCourses.Count} courses...");

            foreach (var oldCourse in oldCourses)
            {
                // Check if course already exists
                var existingCourse = await newDbContext.Courses
                    .FirstOrDefaultAsync(c => c.Slug == oldCourse.Slug);

                if (existingCourse != null)
                {
                    Console.WriteLine($"   ⏭️  Skipping existing course: {oldCourse.Title}");
                    continue;
                }

                // Create new course with tenant
                var newCourse = new Course
                {
                    Id = oldCourse.Id,
                    TenantId = defaultTenant.Id,
                    Title = oldCourse.Title,
                    Slug = oldCourse.Slug,
                    Description = oldCourse.Description,
                    UnitCode = oldCourse.UnitCode,
                    Level = oldCourse.Level,
                    Duration = oldCourse.Duration,
                    IsPublished = oldCourse.IsPublished,
                    ThumbnailUrl = oldCourse.ThumbnailUrl,
                    InstructorId = oldCourse.InstructorId,
                    CreatedAt = oldCourse.CreatedAt,
                    UpdatedAt = oldCourse.UpdatedAt
                };

                newDbContext.Courses.Add(newCourse);
                Console.WriteLine($"   ✅ Migrated course: {newCourse.Title}");

                // Migrate modules for this course
                foreach (var oldModule in oldCourse.Modules.OrderBy(m => m.OrderIndex))
                {
                    var newModule = new Module
                    {
                        Id = oldModule.Id,
                        TenantId = defaultTenant.Id,
                        CourseId = newCourse.Id,
                        Title = oldModule.Title,
                        Description = oldModule.Description,
                        OrderIndex = oldModule.OrderIndex,
                        CreatedAt = oldModule.CreatedAt,
                        UpdatedAt = oldModule.UpdatedAt
                    };

                    newDbContext.Modules.Add(newModule);
                    Console.WriteLine($"      ➕ Migrated module: {newModule.Title}");

                    // Migrate lessons for this module
                    foreach (var oldLesson in oldModule.Lessons.OrderBy(l => l.OrderIndex))
                    {
                        var newLesson = new Lesson
                        {
                            Id = oldLesson.Id,
                            TenantId = defaultTenant.Id,
                            ModuleId = newModule.Id,
                            Title = oldLesson.Title,
                            Content = oldLesson.Content,
                            LessonType = oldLesson.LessonType,
                            OrderIndex = oldLesson.OrderIndex,
                            Duration = oldLesson.Duration,
                            IsPublished = oldLesson.IsPublished,
                            VideoUrl = oldLesson.VideoUrl,
                            QuizId = oldLesson.QuizId,
                            SimulationId = oldLesson.SimulationId,
                            CreatedAt = oldLesson.CreatedAt,
                            UpdatedAt = oldLesson.UpdatedAt
                        };

                        newDbContext.Lessons.Add(newLesson);
                        Console.WriteLine($"         📝 Migrated lesson: {newLesson.Title}");
                    }
                }
            }

            // Save all changes
            await newDbContext.SaveChangesAsync();
            Console.WriteLine($"✅ Migration completed! Migrated {oldCourses.Count} courses");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Migration failed: {ex.Message}");
            Console.WriteLine($"   Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}
