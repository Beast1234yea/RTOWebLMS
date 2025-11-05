using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Utilities
{
    public class PublishForkliftCourse
    {
        private readonly LmsDbContext _db;

        public PublishForkliftCourse(LmsDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Run()
        {
            Console.WriteLine("\n🚀 Publishing TLILIC003 Forklift Course...\n");

            try
            {
                // Find the forklift course
                var course = await _db.Courses
                    .FirstOrDefaultAsync(c => c.UnitCode == "TLILIC003");

                if (course == null)
                {
                    Console.WriteLine("❌ ERROR: Forklift course (TLILIC003) not found in database.");
                    Console.WriteLine("   Please run the import first: dotnet run --import-forklift");
                    return false;
                }

                // Check current status
                Console.WriteLine($"📋 Current Status: {course.Status}");

                if (course.Status == CourseStatus.PUBLISHED)
                {
                    Console.WriteLine("✅ Course is already PUBLISHED!");
                    return true;
                }

                // Update status to PUBLISHED
                course.Status = CourseStatus.PUBLISHED;
                course.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();

                Console.WriteLine($"\n✅ SUCCESS!");
                Console.WriteLine($"   Course Status: DRAFT → PUBLISHED");
                Console.WriteLine($"   Course Title: {course.Title}");
                Console.WriteLine($"   Unit Code: {course.UnitCode}");
                Console.WriteLine($"   Slug: {course.Slug}");
                Console.WriteLine($"\n🌐 Course is now visible at:");
                Console.WriteLine($"   http://localhost:8080/courses");
                Console.WriteLine($"   https://rtoweblms-production.up.railway.app/courses");
                Console.WriteLine($"\n✅ Students can now enroll in the course!");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine($"   Stack Trace: {ex.StackTrace}");
                return false;
            }
        }
    }
}
