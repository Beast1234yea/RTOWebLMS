using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;

namespace RTOWebLMS.Utilities
{
    public class VerifyProductionData
    {
        private readonly LmsDbContext _db;

        public VerifyProductionData(LmsDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Run()
        {
            Console.WriteLine("\n🔍 Verifying Production Database Data...\n");

            try
            {
                // Check database connection
                var canConnect = await _db.Database.CanConnectAsync();
                Console.WriteLine($"📡 Database Connection: {(canConnect ? "✅ SUCCESS" : "❌ FAILED")}");

                if (!canConnect)
                {
                    Console.WriteLine("❌ Cannot connect to database!");
                    return false;
                }

                // Get connection string info (without password)
                var connectionString = _db.Database.GetConnectionString();
                if (connectionString != null)
                {
                    var hostStart = connectionString.IndexOf("Host=") + 5;
                    var hostEnd = connectionString.IndexOf(";", hostStart);
                    var host = connectionString.Substring(hostStart, hostEnd - hostStart);
                    Console.WriteLine($"🗄️  Database Host: {host}");
                }

                // Count all courses
                var totalCourses = await _db.Courses.CountAsync();
                Console.WriteLine($"\n📚 Total Courses: {totalCourses}");

                // Find forklift course
                var forkliftCourse = await _db.Courses
                    .FirstOrDefaultAsync(c => c.UnitCode == "TLILIC003");

                if (forkliftCourse == null)
                {
                    Console.WriteLine("❌ TLILIC003 course NOT FOUND in database!");
                    return false;
                }

                Console.WriteLine($"\n✅ TLILIC003 Course Found:");
                Console.WriteLine($"   ID: {forkliftCourse.Id}");
                Console.WriteLine($"   Title: {forkliftCourse.Title}");
                Console.WriteLine($"   Status: {forkliftCourse.Status}");
                Console.WriteLine($"   Slug: {forkliftCourse.Slug}");

                // Count modules
                var moduleCount = await _db.Modules
                    .Where(m => m.CourseId == forkliftCourse.Id)
                    .CountAsync();
                Console.WriteLine($"   Modules: {moduleCount}");

                // Count lessons
                var lessonCount = await _db.Lessons
                    .Where(l => l.Module!.CourseId == forkliftCourse.Id)
                    .CountAsync();
                Console.WriteLine($"   Lessons: {lessonCount}");

                // Count images
                var imageCount = await _db.LessonMedia
                    .Where(lm => lm.Lesson!.Module!.CourseId == forkliftCourse.Id)
                    .CountAsync();
                Console.WriteLine($"   Images: {imageCount}");

                // List all courses with their status
                Console.WriteLine($"\n📋 All Courses in Database:");
                var allCourses = await _db.Courses.ToListAsync();
                foreach (var course in allCourses)
                {
                    Console.WriteLine($"   - {course.UnitCode}: {course.Title} (Status: {course.Status})");
                }

                Console.WriteLine($"\n✅ Database verification complete!");
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
