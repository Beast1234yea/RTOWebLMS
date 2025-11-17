using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class Competency
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Inherited from Assessment/Course
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string UnitCode { get; set; } = string.Empty;

        public CompetencyResult Result { get; set; } = CompetencyResult.NOT_ASSESSED;

        public DateTime? AchievedAt { get; set; }

        public string? Evidence { get; set; }

        // Foreign Keys
        [Required]
        public string AssessmentId { get; set; } = string.Empty;

        [Required]
        public string CourseId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }

        [ForeignKey("AssessmentId")]
        public virtual Assessment? Assessment { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
