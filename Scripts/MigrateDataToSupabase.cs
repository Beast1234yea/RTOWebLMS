using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;

namespace RTOWebLMS.Scripts;

public class MigrateDataToSupabase
{
    public static async Task Run()
    {
        Console.WriteLine("=== Data Migration: SQLite → Supabase ===\n");

        // SQLite connection
        var sqliteConnectionString = "Data Source=C:\\Users\\nickb\\AppData\\Local\\RTODesktopLMS\\rto_lms.db";
        var sqliteOptions = new DbContextOptionsBuilder<LmsDbContext>()
            .UseSqlite(sqliteConnectionString)
            .Options;

        // Supabase PostgreSQL connection
        var supabaseConnectionString = "Host=aws-1-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.blhzpoicleeojjxokztu;Password=SkylaHugo2025;SSL Mode=Require;Trust Server Certificate=true";
        var supabaseOptions = new DbContextOptionsBuilder<LmsDbContext>()
            .UseNpgsql(supabaseConnectionString)
            .Options;

        try
        {
            using var sourceDb = new LmsDbContext(sqliteOptions);
            using var targetDb = new LmsDbContext(supabaseOptions);

            // Test connections
            Console.WriteLine("Testing connections...");
            await sourceDb.Database.CanConnectAsync();
            Console.WriteLine("✓ Connected to SQLite source database");

            await targetDb.Database.CanConnectAsync();
            Console.WriteLine("✓ Connected to Supabase target database\n");

            // Get counts from source
            Console.WriteLine("Source Database Contents:");
            Console.WriteLine("-------------------------");
            var userCount = await sourceDb.Users.CountAsync();
            var courseCount = await sourceDb.Courses.CountAsync();
            var moduleCount = await sourceDb.Modules.CountAsync();
            var lessonCount = await sourceDb.Lessons.CountAsync();
            var lessonMediaCount = await sourceDb.LessonMedia.CountAsync();
            var quizCount = await sourceDb.Quizzes.CountAsync();
            var quizQuestionCount = await sourceDb.QuizQuestions.CountAsync();
            var quizAnswerCount = await sourceDb.QuizAnswers.CountAsync();
            var enrollmentCount = await sourceDb.Enrollments.CountAsync();
            var lessonProgressCount = await sourceDb.LessonProgress.CountAsync();
            var quizAttemptCount = await sourceDb.QuizAttempts.CountAsync();
            var assessmentCount = await sourceDb.Assessments.CountAsync();
            var certificateCount = await sourceDb.Certificates.CountAsync();
            var competencyCount = await sourceDb.Competencies.CountAsync();
            var documentCount = await sourceDb.Documents.CountAsync();
            var unitySimCount = await sourceDb.UnitySimulations.CountAsync();
            var simResultCount = await sourceDb.SimulationResults.CountAsync();
            var auditLogCount = await sourceDb.AuditLogs.CountAsync();

            Console.WriteLine($"Users: {userCount}");
            Console.WriteLine($"Courses: {courseCount}");
            Console.WriteLine($"Modules: {moduleCount}");
            Console.WriteLine($"Lessons: {lessonCount}");
            Console.WriteLine($"Lesson Media: {lessonMediaCount}");
            Console.WriteLine($"Quizzes: {quizCount}");
            Console.WriteLine($"Quiz Questions: {quizQuestionCount}");
            Console.WriteLine($"Quiz Answers: {quizAnswerCount}");
            Console.WriteLine($"Enrollments: {enrollmentCount}");
            Console.WriteLine($"Lesson Progress: {lessonProgressCount}");
            Console.WriteLine($"Quiz Attempts: {quizAttemptCount}");
            Console.WriteLine($"Assessments: {assessmentCount}");
            Console.WriteLine($"Certificates: {certificateCount}");
            Console.WriteLine($"Competencies: {competencyCount}");
            Console.WriteLine($"Documents: {documentCount}");
            Console.WriteLine($"Unity Simulations: {unitySimCount}");
            Console.WriteLine($"Simulation Results: {simResultCount}");
            Console.WriteLine($"Audit Logs: {auditLogCount}");

            var totalRecords = userCount + courseCount + moduleCount + lessonCount + lessonMediaCount +
                             quizCount + quizQuestionCount + quizAnswerCount + enrollmentCount +
                             lessonProgressCount + quizAttemptCount + assessmentCount + certificateCount +
                             competencyCount + documentCount + unitySimCount + simResultCount + auditLogCount;

            Console.WriteLine($"\nTotal Records: {totalRecords}");

            if (totalRecords == 0)
            {
                Console.WriteLine("\n⚠ No data found in source database. Migration not needed.");
                return;
            }

            Console.WriteLine("\n⚠ WARNING: This will copy all data to Supabase.");
            Console.WriteLine("Press ENTER to continue or Ctrl+C to cancel...");
            Console.ReadLine();

            Console.WriteLine("\nStarting migration...\n");

            // Migrate in dependency order

            // 1. Users (no dependencies)
            if (userCount > 0)
            {
                Console.Write($"Migrating {userCount} users... ");
                var users = await sourceDb.Users.AsNoTracking().ToListAsync();
                await targetDb.Users.AddRangeAsync(users);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 2. Competencies (no dependencies)
            if (competencyCount > 0)
            {
                Console.Write($"Migrating {competencyCount} competencies... ");
                var competencies = await sourceDb.Competencies.AsNoTracking().ToListAsync();
                await targetDb.Competencies.AddRangeAsync(competencies);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 3. Courses (depends on Competencies)
            if (courseCount > 0)
            {
                Console.Write($"Migrating {courseCount} courses... ");
                var courses = await sourceDb.Courses.AsNoTracking().ToListAsync();
                await targetDb.Courses.AddRangeAsync(courses);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 4. Modules (depends on Courses)
            if (moduleCount > 0)
            {
                Console.Write($"Migrating {moduleCount} modules... ");
                var modules = await sourceDb.Modules.AsNoTracking().ToListAsync();
                await targetDb.Modules.AddRangeAsync(modules);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 5. Lessons (depends on Modules)
            if (lessonCount > 0)
            {
                Console.Write($"Migrating {lessonCount} lessons... ");
                var lessons = await sourceDb.Lessons.AsNoTracking().ToListAsync();
                await targetDb.Lessons.AddRangeAsync(lessons);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 6. Lesson Media (depends on Lessons)
            if (lessonMediaCount > 0)
            {
                Console.Write($"Migrating {lessonMediaCount} lesson media... ");
                var lessonMedia = await sourceDb.LessonMedia.AsNoTracking().ToListAsync();
                await targetDb.LessonMedia.AddRangeAsync(lessonMedia);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 7. Quizzes (depends on Modules)
            if (quizCount > 0)
            {
                Console.Write($"Migrating {quizCount} quizzes... ");
                var quizzes = await sourceDb.Quizzes.AsNoTracking().ToListAsync();
                await targetDb.Quizzes.AddRangeAsync(quizzes);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 8. Quiz Questions (depends on Quizzes)
            if (quizQuestionCount > 0)
            {
                Console.Write($"Migrating {quizQuestionCount} quiz questions... ");
                var quizQuestions = await sourceDb.QuizQuestions.AsNoTracking().ToListAsync();
                await targetDb.QuizQuestions.AddRangeAsync(quizQuestions);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 9. Quiz Answers (depends on Quiz Questions)
            if (quizAnswerCount > 0)
            {
                Console.Write($"Migrating {quizAnswerCount} quiz answers... ");
                var quizAnswers = await sourceDb.QuizAnswers.AsNoTracking().ToListAsync();
                await targetDb.QuizAnswers.AddRangeAsync(quizAnswers);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 10. Unity Simulations (depends on Modules)
            if (unitySimCount > 0)
            {
                Console.Write($"Migrating {unitySimCount} Unity simulations... ");
                var unitySims = await sourceDb.UnitySimulations.AsNoTracking().ToListAsync();
                await targetDb.UnitySimulations.AddRangeAsync(unitySims);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 11. Documents (depends on Courses/Modules)
            if (documentCount > 0)
            {
                Console.Write($"Migrating {documentCount} documents... ");
                var documents = await sourceDb.Documents.AsNoTracking().ToListAsync();
                await targetDb.Documents.AddRangeAsync(documents);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 12. Enrollments (depends on Users and Courses)
            if (enrollmentCount > 0)
            {
                Console.Write($"Migrating {enrollmentCount} enrollments... ");
                var enrollments = await sourceDb.Enrollments.AsNoTracking().ToListAsync();
                await targetDb.Enrollments.AddRangeAsync(enrollments);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 13. Lesson Progress (depends on Users and Lessons)
            if (lessonProgressCount > 0)
            {
                Console.Write($"Migrating {lessonProgressCount} lesson progress records... ");
                var lessonProgress = await sourceDb.LessonProgress.AsNoTracking().ToListAsync();
                await targetDb.LessonProgress.AddRangeAsync(lessonProgress);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 14. Quiz Attempts (depends on Users and Quizzes)
            if (quizAttemptCount > 0)
            {
                Console.Write($"Migrating {quizAttemptCount} quiz attempts... ");
                var quizAttempts = await sourceDb.QuizAttempts.AsNoTracking().ToListAsync();
                await targetDb.QuizAttempts.AddRangeAsync(quizAttempts);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 15. Simulation Results (depends on Users and Unity Simulations)
            if (simResultCount > 0)
            {
                Console.Write($"Migrating {simResultCount} simulation results... ");
                var simResults = await sourceDb.SimulationResults.AsNoTracking().ToListAsync();
                await targetDb.SimulationResults.AddRangeAsync(simResults);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 16. Assessments (depends on Users and Courses)
            if (assessmentCount > 0)
            {
                Console.Write($"Migrating {assessmentCount} assessments... ");
                var assessments = await sourceDb.Assessments.AsNoTracking().ToListAsync();
                await targetDb.Assessments.AddRangeAsync(assessments);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 17. Certificates (depends on Users and Courses)
            if (certificateCount > 0)
            {
                Console.Write($"Migrating {certificateCount} certificates... ");
                var certificates = await sourceDb.Certificates.AsNoTracking().ToListAsync();
                await targetDb.Certificates.AddRangeAsync(certificates);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            // 18. Audit Logs (depends on Users)
            if (auditLogCount > 0)
            {
                Console.Write($"Migrating {auditLogCount} audit logs... ");
                var auditLogs = await sourceDb.AuditLogs.AsNoTracking().ToListAsync();
                await targetDb.AuditLogs.AddRangeAsync(auditLogs);
                await targetDb.SaveChangesAsync();
                Console.WriteLine("✓");
            }

            Console.WriteLine("\n✓ Migration completed successfully!");
            Console.WriteLine("\nVerifying target database...");

            // Verify counts match
            var targetUserCount = await targetDb.Users.CountAsync();
            var targetCourseCount = await targetDb.Courses.CountAsync();
            var targetModuleCount = await targetDb.Modules.CountAsync();
            var targetLessonCount = await targetDb.Lessons.CountAsync();

            Console.WriteLine($"\nTarget Database:");
            Console.WriteLine($"Users: {targetUserCount} (source: {userCount})");
            Console.WriteLine($"Courses: {targetCourseCount} (source: {courseCount})");
            Console.WriteLine($"Modules: {targetModuleCount} (source: {moduleCount})");
            Console.WriteLine($"Lessons: {targetLessonCount} (source: {lessonCount})");

            if (targetUserCount == userCount && targetCourseCount == courseCount &&
                targetModuleCount == moduleCount && targetLessonCount == lessonCount)
            {
                Console.WriteLine("\n✓ All data verified successfully!");
            }
            else
            {
                Console.WriteLine("\n⚠ Warning: Record counts don't match. Please review the migration.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Error during migration: {ex.Message}");
            Console.WriteLine($"\nFull exception:\n{ex}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
