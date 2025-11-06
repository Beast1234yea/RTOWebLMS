using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Models;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Utilities
{
    public static class QuickDataSetup
    {
        public static async Task<bool> CreateTestLessonWithForklift(LmsDbContext db)
        {
            try
            {
                Console.WriteLine("🚛 Creating test lesson with 3D forklift model...\n");

                // Create instructor if doesn't exist
                var instructor = await db.Users.FirstOrDefaultAsync(u => u.Role == UserRole.INSTRUCTOR);
                if (instructor == null)
                {
                    instructor = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test Instructor",
                        Email = "instructor@test.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                        Role = UserRole.INSTRUCTOR,
                        CreatedAt = DateTime.UtcNow
                    };
                    db.Users.Add(instructor);
                }

                // Create course
                var course = new Course
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "TLILIC003 - Operate Forklift Truck",
                    UnitCode = "TLILIC003",
                    UnitTitle = "Operate Forklift Truck",
                    Description = "Complete forklift operator training with interactive 3D models",
                    Status = CourseStatus.PUBLISHED,
                    InstructorId = instructor.Id,
                    TrainingPackage = "TLI Transport and Logistics",
                    NominalHours = 20,
                    IsNationallyRecognised = true,
                    RequiresLicense = true,
                    LicenseType = "Forklift License",
                    LicenseClass = "LF",
                    IsHighRisk = true,
                    PassingScore = 80,
                    CreatedAt = DateTime.UtcNow,
                    Slug = "forklift-operator"
                };
                db.Courses.Add(course);

                // Create module
                var module = new Module
                {
                    Id = Guid.NewGuid().ToString(),
                    CourseId = course.Id,
                    Title = "Forklift Operation and Safety",
                    Description = "Interactive 3D forklift training module",
                    OrderIndex = 1,
                    IsPublished = true,
                    CreatedAt = DateTime.UtcNow
                };
                db.Modules.Add(module);

                // Create lesson with 3D forklift content
                var lesson = new Lesson
                {
                    Id = Guid.NewGuid().ToString(),
                    ModuleId = module.Id,
                    Title = "Interactive 3D Forklift Model",
                    Content = @"
<div class='forklift-3d-container' style='margin: 20px 0; padding: 20px; border: 2px solid #007bff; border-radius: 10px; background: #f8f9fa;'>
    <h3 style='color: #007bff; margin-bottom: 15px;'>🚛 Interactive 3D Forklift Model</h3>
    
    <script type='module' src='https://unpkg.com/@google/model-viewer/dist/model-viewer.min.js'></script>
    
    <model-viewer 
        src='/models/forklift/FWS_Forklift_01_Blue.gltf' 
        alt='Blue Forklift 3D Model'
        auto-rotate 
        camera-controls 
        environment-image='neutral'
        style='width: 100%; height: 400px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);'
        loading='eager'>
        <div slot='progress-bar' style='display: none;'></div>
    </model-viewer>
    
    <div style='margin-top: 15px; padding: 10px; background: white; border-radius: 5px;'>
        <p><strong>Instructions:</strong></p>
        <ul>
            <li>🖱️ Click and drag to rotate the forklift</li>
            <li>🔍 Use mouse wheel to zoom in/out</li>
            <li>📱 Touch and pinch on mobile devices</li>
            <li>🔄 Model auto-rotates when idle</li>
        </ul>
    </div>
</div>

<div style='margin: 20px 0; padding: 15px; background: #fff3cd; border-left: 4px solid #ffc107; border-radius: 5px;'>
    <h4 style='color: #856404;'>📋 Forklift Safety Quiz</h4>
    <p>Study the 3D model above, then answer these questions:</p>
    
    <div class='quiz-question' style='margin: 10px 0; padding: 10px; background: white; border-radius: 5px;'>
        <p><strong>Q1: What safety equipment should be worn when operating a forklift?</strong></p>
        <label><input type='radio' name='q1' value='a'> A) Hard hat only</label><br>
        <label><input type='radio' name='q1' value='b'> B) Safety vest only</label><br>
        <label><input type='radio' name='q1' value='c'> C) Hard hat, safety vest, and steel-toe boots</label><br>
        <label><input type='radio' name='q1' value='d'> D) No special equipment needed</label>
    </div>
    
    <div class='quiz-question' style='margin: 10px 0; padding: 10px; background: white; border-radius: 5px;'>
        <p><strong>Q2: What is the maximum safe speed for forklift operation?</strong></p>
        <label><input type='radio' name='q2' value='a'> A) 25 km/h</label><br>
        <label><input type='radio' name='q2' value='b'> B) 15 km/h</label><br>
        <label><input type='radio' name='q2' value='c'> C) 8 km/h</label><br>
        <label><input type='radio' name='q2' value='d'> D) 5 km/h</label>
    </div>
    
    <button onclick='checkAnswers()' style='background: #28a745; color: white; border: none; padding: 10px 20px; border-radius: 5px; cursor: pointer; margin-top: 10px;'>
        ✅ Check Answers
    </button>
    
    <div id='quiz-results' style='margin-top: 10px; display: none;'></div>
</div>

<script>
function checkAnswers() {
    const q1 = document.querySelector('input[name=""q1""]:checked');
    const q2 = document.querySelector('input[name=""q2""]:checked');
    const results = document.getElementById('quiz-results');
    
    let score = 0;
    let feedback = '<h5>Quiz Results:</h5>';
    
    if (q1) {
        if (q1.value === 'c') {
            score++;
            feedback += '<p style=""color: green;"">✅ Q1: Correct! Hard hat, safety vest, and steel-toe boots are required.</p>';
        } else {
            feedback += '<p style=""color: red;"">❌ Q1: Incorrect. All safety equipment (hard hat, vest, boots) is required.</p>';
        }
    }
    
    if (q2) {
        if (q2.value === 'c') {
            score++;
            feedback += '<p style=""color: green;"">✅ Q2: Correct! 8 km/h is the maximum safe speed.</p>';
        } else {
            feedback += '<p style=""color: red;"">❌ Q2: Incorrect. Maximum safe speed is 8 km/h.</p>';
        }
    }
    
    feedback += `<p><strong>Score: ${score}/2 (${Math.round(score/2*100)}%)</strong></p>`;
    
    if (score === 2) {
        feedback += '<p style=""color: green; font-weight: bold;"">🎉 Excellent! You understand forklift safety!</p>';
    } else {
        feedback += '<p style=""color: orange;"">📚 Review the lesson and try again!</p>';
    }
    
    results.innerHTML = feedback;
    results.style.display = 'block';
    results.style.padding = '15px';
    results.style.background = '#f8f9fa';
    results.style.borderRadius = '5px';
    results.style.border = '1px solid #dee2e6';
}
</script>",
                    Type = LessonType.UNITY_SIMULATION,
                    Duration = 30,
                    IsRequired = true,
                    IsPublished = true,
                    OrderIndex = 1,
                    CreatedAt = DateTime.UtcNow
                };
                db.Lessons.Add(lesson);

                await db.SaveChangesAsync();

                Console.WriteLine("✅ SUCCESS! Test lesson created with:");
                Console.WriteLine($"   📚 Course: {course.Title}");
                Console.WriteLine($"   📖 Module: {module.Title}");
                Console.WriteLine($"   🎯 Lesson: {lesson.Title}");
                Console.WriteLine($"   🚛 3D Model: /models/forklift/FWS_Forklift_01_Blue.gltf");
                Console.WriteLine($"\n🌐 Access at: /lesson/{lesson.Id}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                return false;
            }
        }
    }
}