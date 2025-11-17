using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Models;

namespace RTOWebLMS.Data
{
    public class LmsDbContext : DbContext
    {
        public LmsDbContext(DbContextOptions<LmsDbContext> options) : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<Tenant> Tenants { get; set; }  // Multi-tenancy core
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<UnitySimulation> UnitySimulations { get; set; }
        public DbSet<SimulationResult> SimulationResults { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<LessonMedia> LessonMedia { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== MULTI-TENANCY CONFIGURATION =====

            // Tenant indexes
            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.Subdomain)
                .IsUnique();

            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.RTOCode);

            // Tenant -> Users
            modelBuilder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tenant -> Courses
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Tenant)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== EXISTING CONFIGURATION =====

            // Configure unique indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.UnitCode);

            modelBuilder.Entity<Certificate>()
                .HasIndex(c => c.CertificateNumber)
                .IsUnique();

            modelBuilder.Entity<Certificate>()
                .HasIndex(c => c.VerificationCode)
                .IsUnique();

            // Configure relationships

            // User -> Courses (Instructor)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(u => u.InstructorCourses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Modules
            modelBuilder.Entity<Module>()
                .HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Module -> Lessons
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Module)
                .WithMany(m => m.Lessons)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enrollment relationships
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // LessonProgress relationships
            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.Enrollment)
                .WithMany(e => e.LessonProgress)
                .HasForeignKey(lp => lp.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LessonProgress>()
                .HasOne(lp => lp.Lesson)
                .WithMany(l => l.LessonProgress)
                .HasForeignKey(lp => lp.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Certificate relationships
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.User)
                .WithMany(u => u.Certificates)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Course)
                .WithMany(co => co.Certificates)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Assessment relationships
            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.User)
                .WithMany(u => u.Assessments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assessments)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assessment>()
                .HasOne(a => a.GradedBy)
                .WithMany()
                .HasForeignKey(a => a.GradedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Competency relationships
            modelBuilder.Entity<Competency>()
                .HasOne(c => c.Assessment)
                .WithMany(a => a.Competencies)
                .HasForeignKey(c => c.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Competency>()
                .HasOne(c => c.Course)
                .WithMany(co => co.Competencies)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unity Simulation relationships
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Simulation)
                .WithMany(s => s.Lessons)
                .HasForeignKey(l => l.SimulationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SimulationResult>()
                .HasOne(sr => sr.Simulation)
                .WithMany(s => s.SimulationResults)
                .HasForeignKey(sr => sr.SimulationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz relationships
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Quiz)
                .WithMany(q => q.Lessons)
                .HasForeignKey(l => l.QuizId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<QuizQuestion>()
                .HasOne(qq => qq.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizAnswer>()
                .HasOne(qa => qa.Question)
                .WithMany(qq => qq.Answers)
                .HasForeignKey(qa => qa.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Quiz)
                .WithMany(q => q.Attempts)
                .HasForeignKey(qa => qa.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Document relationships
            modelBuilder.Entity<Document>()
                .HasOne(d => d.Lesson)
                .WithMany(l => l.Documents)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // LessonMedia relationships
            modelBuilder.Entity<LessonMedia>()
                .HasOne(lm => lm.Lesson)
                .WithMany(l => l.Media)
                .HasForeignKey(lm => lm.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // AuditLog relationships
            modelBuilder.Entity<AuditLog>()
                .HasOne(al => al.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.SetNull)  // Keep audit logs even if user is deleted
                .IsRequired(false);  // Allow null UserId for system actions
        }
    }
}
