using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class Course
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Each course belongs to one RTO (Tenant)
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Thumbnail { get; set; }

        public bool IsHighRisk { get; set; } = false;

        public CourseStatus Status { get; set; } = CourseStatus.DRAFT;

        public int PassingScore { get; set; } = 80;

        public int? MinCompletionTime { get; set; }

        public int? ExpiryMonths { get; set; }

        // RTO Compliance Fields - Training.gov.au data
        [MaxLength(50)]
        public string? UnitCode { get; set; }

        [MaxLength(500)]
        public string? UnitTitle { get; set; }

        [MaxLength(100)]
        public string? TrainingPackage { get; set; }

        public bool IsNationallyRecognised { get; set; } = false;

        public int? NominalHours { get; set; }

        public bool RequiresLicense { get; set; } = false;

        [MaxLength(100)]
        public string? LicenseType { get; set; }

        [MaxLength(100)]
        public string? LicenseClass { get; set; }

        // Foreign Keys
        [Required]
        public string InstructorId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }

        [ForeignKey("InstructorId")]
        public virtual User? Instructor { get; set; }

        public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
        public virtual ICollection<Competency> Competencies { get; set; } = new List<Competency>();
    }
}
