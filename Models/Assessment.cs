using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class Assessment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public AssessmentStatus Status { get; set; } = AssessmentStatus.PENDING;

        public DateTime? SubmittedAt { get; set; }

        public DateTime? GradedAt { get; set; }

        public int? Score { get; set; }

        public string? Feedback { get; set; }

        // RTO-specific fields
        public string? EnrollmentId { get; set; }

        [MaxLength(100)]
        public string? AssessmentMethod { get; set; } // e.g., "Practical Observation", "Written Test", "Interview"

        public DateTime? AssessmentDate { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string CourseId { get; set; } = string.Empty;

        public string? GradedById { get; set; } // This is the assessor

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        [ForeignKey("GradedById")]
        public virtual User? GradedBy { get; set; }

        [ForeignKey("EnrollmentId")]
        public virtual Enrollment? Enrollment { get; set; }

        public virtual ICollection<Competency> Competencies { get; set; } = new List<Competency>();
    }
}
