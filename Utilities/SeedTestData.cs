using RTOWebLMS.Data;
using RTOWebLMS.Models;
using RTOWebLMS.Enums;
using Microsoft.EntityFrameworkCore;

namespace RTOWebLMS.Utilities
{
    public static class SeedTestData
    {
        public static async Task SeedAsync(LmsDbContext context)
        {
            // Check if data already exists
            if (await context.Courses.AnyAsync())
            {
                return; // Data already seeded
            }

            // Create a test user
            var userId = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = userId,
                Name = "Test Instructor",
                Email = "instructor@test.com",
                FirstName = "Test",
                LastName = "Instructor",
                Role = UserRole.INSTRUCTOR,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Create a test course
            var courseId = Guid.NewGuid().ToString();
            var course = new Course
            {
                Id = courseId,
                Title = "Forklift Operation & Safety",
                Description = "Comprehensive training course on forklift operation, safety procedures, and best practices for handling heavy materials.",
                Slug = "forklift-operation",
                IsHighRisk = true,
                Status = CourseStatus.PUBLISHED,
                NominalHours = 20,
                PassingScore = 70,
                InstructorId = userId,
                IsNationallyRecognised = true,
                LicenseType = "Forklift",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Courses.Add(course);
            await context.SaveChangesAsync();

            // Create modules
            var module1Id = Guid.NewGuid().ToString();
            var module1 = new Module
            {
                Id = module1Id,
                CourseId = courseId,
                Title = "Module 1: Forklift Basics",
                Description = "Introduction to forklift equipment and components",
                OrderIndex = 1,
                IsPublished = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Modules.Add(module1);
            await context.SaveChangesAsync();

            // Create lessons
            var lesson1Id = Guid.NewGuid().ToString();
            var lesson1 = new Lesson
            {
                Id = lesson1Id,
                ModuleId = module1Id,
                Title = "Lesson 1: Forklift Components and Functions",
                Content = @"1. INTRODUCTION TO FORKLIFTS

Forklifts are essential material handling equipment in warehouses, construction sites, and industrial facilities.

2. MAIN COMPONENTS

The key components of a forklift include:

• Forks - The load-bearing elements that slide under pallets and materials
• Mast - The vertical lifting mechanism that raises and lowers the forks
• Controls - The steering wheel and hydraulic levers for operation
• Wheels - Front wheels for steering, rear wheels for support
• Body - The main chassis containing the engine and counterweight

3. SAFETY INFORMATION

Always ensure proper training before operating a forklift. Follow all site-specific safety procedures.

4. OPERATION

When operating a forklift, remember to:

• Keep the load low and centered
• Drive slowly in tight spaces
• Use mirrors and spotters when visibility is limited
• Never exceed rated load capacity
• Perform daily maintenance checks",
                Type = LessonType.TEXT,
                Duration = 30,
                OrderIndex = 1,
                IsPublished = true,
                IsRequired = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Lessons.Add(lesson1);
            await context.SaveChangesAsync();

            // Create a sample enrollment
            var studentId = Guid.NewGuid().ToString();
            var student = new User
            {
                Id = studentId,
                Name = "Test Student",
                Email = "student@test.com",
                FirstName = "Test",
                LastName = "Student",
                Role = UserRole.STUDENT,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Users.Add(student);
            await context.SaveChangesAsync();

            var enrollmentId = Guid.NewGuid().ToString();
            var enrollment = new Enrollment
            {
                Id = enrollmentId,
                UserId = studentId,
                CourseId = courseId,
                Status = EnrollmentStatus.ACTIVE,
                EnrolledAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            context.Enrollments.Add(enrollment);
            await context.SaveChangesAsync();

            Console.WriteLine("✅ Test data seeded successfully!");
        }
    }
}
