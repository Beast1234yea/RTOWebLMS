using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;

namespace RTOWebLMS.Utilities;

public static class CheckSupabase
{
    public static async Task Run(LmsDbContext context)
    {
        Console.WriteLine("\n=== SUPABASE DATABASE CONTENTS ===\n");

        // Check courses
        var courses = await context.Courses.ToListAsync();
        Console.WriteLine($"📚 COURSES ({courses.Count}):");
        foreach (var course in courses)
        {
            Console.WriteLine($"  - {course.UnitCode}: {course.Title} ({course.Status})");
        }

        // Check modules per course
        Console.WriteLine($"\n📖 MODULES:");
        foreach (var course in courses)
        {
            var moduleCount = await context.Modules
                .Where(m => m.CourseId == course.Id)
                .CountAsync();
            Console.WriteLine($"  - {course.UnitCode}: {moduleCount} modules");
        }

        // Check lessons per course
        Console.WriteLine($"\n📝 LESSONS:");
        foreach (var course in courses)
        {
            var lessonCount = await context.Lessons
                .Where(l => l.Module.CourseId == course.Id)
                .CountAsync();
            Console.WriteLine($"  - {course.UnitCode}: {lessonCount} lessons");
        }

        // Check media per course
        Console.WriteLine($"\n🖼️ MEDIA FILES:");
        foreach (var course in courses)
        {
            var mediaCount = await context.LessonMedia
                .Where(lm => lm.Lesson.Module.CourseId == course.Id)
                .CountAsync();
            Console.WriteLine($"  - {course.UnitCode}: {mediaCount} media files");
        }

        // Show sample media paths
        Console.WriteLine($"\n📂 SAMPLE MEDIA PATHS:");
        var sampleMedia = await context.LessonMedia
            .OrderBy(lm => lm.Id)
            .Take(10)
            .ToListAsync();

        foreach (var media in sampleMedia)
        {
            Console.WriteLine($"  - {media.MediaType}: {media.FilePath}");
        }

        // Forklift course details
        var forkliftCourse = await context.Courses
            .FirstOrDefaultAsync(c => c.UnitCode == "TLILIC003");

        if (forkliftCourse != null)
        {
            Console.WriteLine($"\n🚜 FORKLIFT COURSE (TLILIC003):");
            Console.WriteLine($"  Title: {forkliftCourse.Title}");
            Console.WriteLine($"  Status: {forkliftCourse.Status}");
            Console.WriteLine($"  Duration: {forkliftCourse.NominalHours} hours");

            var modules = await context.Modules
                .Where(m => m.CourseId == forkliftCourse.Id)
                .OrderBy(m => m.OrderIndex)
                .ToListAsync();

            Console.WriteLine($"\n  Modules ({modules.Count}):");
            foreach (var module in modules)
            {
                Console.WriteLine($"    {module.OrderIndex}. {module.Title}");
            }
        }

        Console.WriteLine($"\n✅ Database check complete!\n");
    }
}
