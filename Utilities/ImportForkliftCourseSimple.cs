using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Models;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Utilities
{
    /// <summary>
    /// SIMPLIFIED Forklift Course Importer
    /// Automatically maps 143 images to lessons based on page ranges
    /// Run with: dotnet run --import-forklift
    /// </summary>
    public class ImportForkliftCourseSimple
    {
        private readonly LmsDbContext _db;
        private  string _courseId = Guid.NewGuid().ToString();
        private string _instructorId = string.Empty;

        public ImportForkliftCourseSimple(LmsDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Run()
        {
            try
            {
                Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║    TLILIC003 FORKLIFT COURSE - AUTOMATED IMPORT          ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");

                // Get all forklift images
                var images = GetAllForkliftImages();
                Console.WriteLine($"📁 Found {images.Count} forklift images (page 1-69)\n");

                // Step 1: Setup instructor
                Console.WriteLine("👤 Step 1: Setting up instructor...");
                await SetupInstructor();

                // Step 2: Create course
                Console.WriteLine("📚 Step 2: Creating TLILIC003 course...");
                var course = await CreateCourse();

                // Step 3: Create modules with lessons and auto-mapped images
                Console.WriteLine("📖 Step 3: Creating 12 modules with lessons...\n");
                await CreateModulesWithLessons(course, images);

                // Step 4: Verify
                Console.WriteLine("\n✅ Step 4: Verifying import...");
                await VerifyImport(course.Id);

                Console.WriteLine("\n╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine("║              ✅ IMPORT COMPLETED SUCCESSFULLY!             ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝\n");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERROR: {ex.Message}");
                Console.WriteLine($"📍 {ex.StackTrace}");
                return false;
            }
        }

        private List<string> GetAllForkliftImages()
        {
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "forklift");
            if (!Directory.Exists(imageFolder))
            {
                throw new Exception($"Image folder not found: {imageFolder}");
            }

            return Directory.GetFiles(imageFolder)
                .Where(f => f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                           f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                           f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                .Select(f => Path.GetFileName(f))
                .OrderBy(f => f)
                .ToList();
        }

        private async Task SetupInstructor()
        {
            var instructor = await _db.Users
                .Where(u => u.Role == UserRole.ADMIN || u.Role == UserRole.INSTRUCTOR)
                .FirstOrDefaultAsync();

            if (instructor == null)
            {
                instructor = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Forklift Instructor",
                    Email = "instructor@rto.edu.au",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("ChangeMe123!"),
                    FirstName = "Forklift",
                    LastName = "Instructor",
                    Role = UserRole.INSTRUCTOR
                };
                _db.Users.Add(instructor);
                await _db.SaveChangesAsync();
                Console.WriteLine($"   ✅ Created instructor: {instructor.Email}");
            }
            else
            {
                Console.WriteLine($"   ✅ Using instructor: {instructor.Email}");
            }

            _instructorId = instructor.Id;
        }

        private async Task<Course> CreateCourse()
        {
            // Check if exists
            var existing = await _db.Courses.FirstOrDefaultAsync(c => c.UnitCode == "TLILIC003");
            if (existing != null)
            {
                Console.WriteLine("   ⚠️  Course exists - deleting and recreating...");
                _db.Courses.Remove(existing);
                await _db.SaveChangesAsync();
            }

            var course = new Course
            {
                Id = _courseId,
                Title = "TLILIC003 - Licence to Operate a Forklift Truck",
                Slug = "tlilic003-forklift-licence",
                Description = @"<h2>National Forklift Operator Licence Training</h2>
<p>This nationally recognised course provides comprehensive training for obtaining a High Risk Work Licence to operate a forklift truck.</p>

<h3>Course Overview</h3>
<ul>
    <li><strong>Unit Code:</strong> TLILIC003</li>
    <li><strong>Duration:</strong> 20 nominal hours</li>
    <li><strong>Assessment:</strong> Three-part competency assessment</li>
    <li><strong>Outcome:</strong> High Risk Work Licence (Forklift - Class LF)</li>
</ul>

<h3>Assessment Requirements</h3>
<ul>
    <li><strong>Part 1 - Knowledge:</strong> 61 questions, 95% pass mark (57/61 correct)</li>
    <li><strong>Part 2 - Calculations:</strong> 9 questions, 100% pass mark (all correct)</li>
    <li><strong>Part 3 - Practical:</strong> 5 tasks, all must be completed satisfactorily</li>
</ul>

<h3>What You'll Learn</h3>
<ul>
    <li>✅ WHS legislation and operator duties</li>
    <li>✅ Forklift components and stability principles</li>
    <li>✅ Pre-operational inspections and safety checks</li>
    <li>✅ Load calculations and capacity assessments</li>
    <li>✅ Safe operating procedures and hazard management</li>
    <li>✅ Emergency response procedures</li>
</ul>

<div class='alert alert-info'>
    <strong>Interactive Features:</strong> This course includes 3D forklift simulations, interactive pre-start inspections, and virtual hazard identification exercises.
</div>",
                Thumbnail = "/images/forklift/page_001_image_01.jpeg",

                // RTO Compliance
                UnitCode = "TLILIC003",
                UnitTitle = "Licence to Operate a Forklift Truck",
                TrainingPackage = "TLI Transport and Logistics Training Package",
                IsNationallyRecognised = true,
                NominalHours = 20,
                RequiresLicense = true,
                LicenseType = "High Risk Work Licence",
                LicenseClass = "Forklift Truck (LF)",

                // Settings
                Status = CourseStatus.DRAFT,
                PassingScore = 95,
                IsHighRisk = true,
                MinCompletionTime = 10,

                InstructorId = _instructorId
            };

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            Console.WriteLine($"   ✅ Course created: {course.Title}");
            return course;
        }

        private async Task CreateModulesWithLessons(Course course, List<string> images)
        {
            // Module definitions with page ranges and lesson structure
            var moduleDefinitions = new List<ModuleDefinition>
            {
                new ModuleDefinition
                {
                    ModuleNumber = 1,
                    Title = "Introduction to Forklift Operations",
                    Description = "Course overview, forklift types, workplace safety culture, and licensing requirements.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "Welcome to TLILIC003", Pages = "1", Duration = 5 },
                        new LessonDefinition { Title = "What is a Forklift?", Pages = "5-6", Duration = 10 },
                        new LessonDefinition { Title = "Types of Forklifts", Pages = "7-8", Duration = 15 },
                        new LessonDefinition { Title = "Licensing Requirements", Pages = "9", Duration = 10 }
                    }
                },
                new ModuleDefinition
                {
                    ModuleNumber = 2,
                    Title = "Legislative Requirements & Duties",
                    Description = "WHS legislation, worker and employer duties, regulatory requirements.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "⚠️ WHS Legislation (NEW CONTENT REQUIRED)", Pages = "", Duration = 20, IsPlaceholder = true },
                        new LessonDefinition { Title = "⚠️ Worker's Duty of Care (NEW CONTENT REQUIRED)", Pages = "", Duration = 15, IsPlaceholder = true },
                        new LessonDefinition { Title = "⚠️ Employer Responsibilities (NEW CONTENT REQUIRED)", Pages = "", Duration = 15, IsPlaceholder = true }
                    }
                },
                new ModuleDefinition
                {
                    ModuleNumber = 3,
                    Title = "Hazard Identification & Risk Controls",
                    Description = "Workplace hazards, risk assessment, communication, and control measures.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "Workplace Hazards", Pages = "10-11", Duration = 15 },
                        new LessonDefinition { Title = "Risk Assessment and Controls", Pages = "12-13", Duration = 15 },
                        new LessonDefinition { Title = "Communication Protocols", Pages = "14-15", Duration = 10 },
                        new LessonDefinition { Title = "Pedestrian Protection", Pages = "16-17", Duration = 10 },
                        new LessonDefinition { Title = "Environmental Hazards", Pages = "18", Duration = 10 }
                    }
                },
                new ModuleDefinition
                {
                    ModuleNumber = 4,
                    Title = "🔴 Electric Power Line Safety (CRITICAL)",
                    Description = "Safe distances, emergency procedures, jump-clear technique, visual indicators.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "🔴 Power Line Safe Distances (NEW CONTENT REQUIRED)", Pages = "", Duration = 15, IsPlaceholder = true },
                        new LessonDefinition { Title = "🔴 Emergency Contact Procedures (NEW CONTENT REQUIRED)", Pages = "", Duration = 20, IsPlaceholder = true }
                    }
                },
                new ModuleDefinition
                {
                    ModuleNumber = 5,
                    Title = "Forklift Components & Stability",
                    Description = "Forklift anatomy, stability triangle, load theory, capacity plates.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "Forklift Components", Pages = "19-22", Duration = 15 },
                        new LessonDefinition { Title = "The Stability Triangle", Pages = "23-26", Duration = 20 },
                        new LessonDefinition { Title = "Load Theory and Physics", Pages = "27-30", Duration = 20 }
                    }
                },
                new ModuleDefinition
                {
                    ModuleNumber = 6,
                    Title = "Pre-Start Inspection Procedures",
                    Description = "Visual inspections, checklist requirements, operational checks.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "Pre-Start Visual Inspection", Pages = "31-34", Duration = 15 },
                        new LessonDefinition { Title = "Operational Checks", Pages = "35-37", Duration = 15 },
                        new LessonDefinition { Title = "Safety Equipment Checks", Pages = "38-40", Duration = 15 }
                    }
                },
                new ModuleDefinition
                {
                    ModuleNumber = 7,
                    Title = "Load Theory & Calculations",
                    Description = "Load weight calculations, load centre, capacity assessment, data plate reading.",
                    Lessons = new List<LessonDefinition>
                    {
                        new LessonDefinition { Title = "Understanding Load Weight", Pages = "41-43", Duration = 15 },
                        new LessonDefinition { Title = "Load Centre Calculations", Pages = "44-46", Duration = 20 },
                        new LessonDefinition { Title = "Data Plate Interpretation", Pages = "47-48", Duration = 15 }
                    }
                },
                new ModuleDefinition
                    {
                        ModuleNumber = 8,
                        Title = "Safe Operating Procedures",
                        Description = "Travel procedures, maneuvering, ramps, loading, unloading, stacking.",
                        Lessons = new List<LessonDefinition>
                        {
                            new LessonDefinition { Title = "Safe Travel Procedures", Pages = "49-51", Duration = 15 },
                            new LessonDefinition { Title = "Maneuvering and Turning", Pages = "52-53", Duration = 10 },
                            new LessonDefinition { Title = "Operating on Ramps and Slopes", Pages = "54-55", Duration = 15 },
                            new LessonDefinition { Title = "Loading and Unloading", Pages = "56-57", Duration = 15 },
                            new LessonDefinition { Title = "Stacking Operations", Pages = "58", Duration = 10 }
                        }
                    },
                    new ModuleDefinition
                    {
                        ModuleNumber = 9,
                        Title = "Forklift Attachments & Variations",
                        Description = "Types of attachments, jib operations, side-shift, personnel platforms.",
                        Lessons = new List<LessonDefinition>
                        {
                            new LessonDefinition { Title = "Forklift Attachments Overview", Pages = "59-60", Duration = 15 },
                            new LessonDefinition { Title = "Jib Operations and Safety", Pages = "61-62", Duration = 15 }
                        }
                    },
                    new ModuleDefinition
                    {
                        ModuleNumber = 10,
                        Title = "🔴 Emergency Procedures (CRITICAL)",
                        Description = "Tip-over response, equipment failure, lock-out/tag-out, incident reporting.",
                        Lessons = new List<LessonDefinition>
                        {
                            new LessonDefinition { Title = "🔴 Tip-Over Response (NEW CONTENT REQUIRED)", Pages = "", Duration = 15, IsPlaceholder = true },
                            new LessonDefinition { Title = "🔴 Equipment Failure Procedures (NEW CONTENT REQUIRED)", Pages = "", Duration = 15, IsPlaceholder = true }
                        }
                    },
                    new ModuleDefinition
                    {
                        ModuleNumber = 11,
                        Title = "Parking & Shutdown Procedures",
                        Description = "Safe parking locations, shutdown steps, securing forklift.",
                        Lessons = new List<LessonDefinition>
                        {
                            new LessonDefinition { Title = "Safe Parking Procedures", Pages = "63-64", Duration = 10 },
                            new LessonDefinition { Title = "Shutdown and Securing", Pages = "65", Duration = 10 }
                        }
                    },
                    new ModuleDefinition
                    {
                        ModuleNumber = 12,
                        Title = "Assessment Preparation",
                        Description = "Practice quizzes, mock exams, practical skills checklist.",
                        Lessons = new List<LessonDefinition>
                        {
                            new LessonDefinition { Title = "Course Review", Pages = "66-67", Duration = 20 },
                            new LessonDefinition { Title = "Assessment Overview", Pages = "68-69", Duration = 15 }
                        }
                    }
                };

                // Create modules
                int moduleOrder = 0;
                foreach (var moduleDef in moduleDefinitions)
                {
                    var module = new Module
                    {
                        Id = Guid.NewGuid().ToString(),
                        CourseId = course.Id,
                        Title = $"{moduleDef.ModuleNumber}. {moduleDef.Title}",
                        Description = moduleDef.Description,
                        OrderIndex = moduleOrder++,
                        IsPublished = false
                    };

                    _db.Modules.Add(module);
                    await _db.SaveChangesAsync();

                    Console.WriteLine($"   📂 Module {moduleDef.ModuleNumber}: {moduleDef.Title}");

                    // Create lessons for this module
                    int lessonOrder = 0;
                    foreach (var lessonDef in moduleDef.Lessons)
                    {
                        var lesson = new Lesson
                        {
                            Id = Guid.NewGuid().ToString(),
                            ModuleId = module.Id,
                            Title = lessonDef.Title,
                            Content = lessonDef.IsPlaceholder
                                ? GetPlaceholderContent(lessonDef.Title)
                                : GetDefaultLessonContent(lessonDef.Title),
                            Type = LessonType.TEXT,
                            OrderIndex = lessonOrder++,
                            Duration = lessonDef.Duration,
                            IsRequired = true,
                            IsPublished = false
                        };

                        _db.Lessons.Add(lesson);
                        await _db.SaveChangesAsync();

                        // Link images for this lesson
                        if (!lessonDef.IsPlaceholder && !string.IsNullOrEmpty(lessonDef.Pages))
                        {
                            var lessonImages = GetImagesForPageRange(images, lessonDef.Pages);
                            int mediaOrder = 0;
                            foreach (var imageName in lessonImages)
                            {
                                var media = new LessonMedia
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    LessonId = lesson.Id,
                                    Title = Path.GetFileNameWithoutExtension(imageName),
                                    Caption = $"Page {GetPageNumber(imageName)}",
                                    FilePath = $"/images/forklift/{imageName}",
                                    MediaType = "IMAGE",
                                    OrderIndex = mediaOrder++
                                };
                                _db.LessonMedia.Add(media);
                            }
                        }

                        var imageCount = lessonDef.IsPlaceholder ? 0 : GetImagesForPageRange(images, lessonDef.Pages).Count;
                        var marker = lessonDef.IsPlaceholder ? "⚠️" : "📄";
                        Console.WriteLine($"      {marker} {lessonDef.Title} ({imageCount} images)");
                    }

                    await _db.SaveChangesAsync();
                }

                Console.WriteLine($"\n   ✅ Created {moduleDefinitions.Count} modules");
                Console.WriteLine($"   ✅ Created {moduleDefinitions.Sum(m => m.Lessons.Count)} lessons");
            }

            private List<string> GetImagesForPageRange(List<string> allImages, string pageRange)
            {
                if (string.IsNullOrEmpty(pageRange)) return new List<string>();

                var result = new List<string>();
                var ranges = pageRange.Split(',');

                foreach (var range in ranges)
                {
                    if (range.Contains('-'))
                    {
                        // Range like "10-15"
                        var parts = range.Trim().Split('-');
                        int start = int.Parse(parts[0]);
                        int end = int.Parse(parts[1]);

                        for (int page = start; page <= end; page++)
                        {
                            result.AddRange(allImages.Where(img => GetPageNumber(img) == page));
                        }
                    }
                    else
                    {
                        // Single page like "5"
                        int page = int.Parse(range.Trim());
                        result.AddRange(allImages.Where(img => GetPageNumber(img) == page));
                    }
                }

                return result;
            }

            private int GetPageNumber(string filename)
            {
                // Extract page number from "page_001_image_01.jpeg" -> 1
                var match = Regex.Match(filename, @"page_(\d+)_");
                return match.Success ? int.Parse(match.Groups[1].Value) : 0;
            }

            private string GetPlaceholderContent(string title)
            {
                return $@"<div class='alert alert-warning' role='alert'>
    <h4 class='alert-heading'>🚧 Content Under Development</h4>
    <p><strong>{title}</strong></p>
    <hr>
    <p class='mb-0'>This is critical content identified in the gap analysis. Content development is required to meet assessment requirements.</p>
    <p class='mb-0'>Refer to: <code>FORKLIFT_COURSE_GAP_ANALYSIS_AND_RECOMMENDATIONS.md</code></p>
</div>

<p>This lesson will cover essential life-safety and compliance content required for the TLILIC003 assessment.</p>";
            }

            private string GetDefaultLessonContent(string title)
            {
                return $@"<h2>{title}</h2>
<p>This lesson covers important aspects of forklift operation related to {title.ToLower()}.</p>
<p>Review the images and diagrams below to understand the key concepts.</p>

<div class='alert alert-info'>
    <strong>Learning Tip:</strong> Take your time reviewing each image. Click to enlarge and study the details.
</div>";
            }

            private async Task VerifyImport(string courseId)
            {
                var course = await _db.Courses
                    .Include(c => c.Modules)
                    .ThenInclude(m => m.Lessons)
                    .ThenInclude(l => l.Media)
                    .FirstOrDefaultAsync(c => c.Id == courseId);

                if (course == null) throw new Exception("Course not found!");

                var totalLessons = course.Modules.Sum(m => m.Lessons.Count);
                var totalMedia = course.Modules.Sum(m => m.Lessons.Sum(l => l.Media.Count));
                var placeholderLessons = course.Modules.Sum(m => m.Lessons.Count(l => l.Title.Contains("NEW CONTENT REQUIRED")));

                Console.WriteLine($"\n   ╔═══════════════════════════════════════════════════╗");
                Console.WriteLine($"   ║              IMPORT STATISTICS                    ║");
                Console.WriteLine($"   ╚═══════════════════════════════════════════════════╝");
                Console.WriteLine($"   📚 Course: {course.Title}");
                Console.WriteLine($"   📂 Modules: {course.Modules.Count}");
                Console.WriteLine($"   📄 Lessons: {totalLessons}");
                Console.WriteLine($"   🖼️  Images Linked: {totalMedia}");
                Console.WriteLine($"   ⚠️  Placeholder Lessons: {placeholderLessons}");
                Console.WriteLine($"   🔗 View at: /courses/{course.Slug}");
            }
        }

    }

    // Helper classes for ImportForkliftCourseSimple
    internal class ModuleDefinition
    {
        public int ModuleNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<LessonDefinition> Lessons { get; set; } = new List<LessonDefinition>();
    }

    internal class LessonDefinition
    {
        public string Title { get; set; } = string.Empty;
        public string Pages { get; set; } = string.Empty; // e.g., "1-5" or "10" or "15-20,25"
        public int Duration { get; set; }
        public bool IsPlaceholder { get; set; } = false;
    }
