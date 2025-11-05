using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class Enrollment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(50)]
        public string? EnrollmentNumber { get; set; }

        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.PENDING;

        public int Progress { get; set; } = 0;
        public int ProgressPercent { get; set; } = 0; // Alias for Progress for UI compatibility

        // Dates
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow; // Legacy field
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // RTO-specific fields
        [MaxLength(200)]
        public string? Location { get; set; }

        public string? TrainerId { get; set; }

        // Payment tracking
        public PaymentStatus? PaymentStatus { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? TotalPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? AmountPaid { get; set; }

        public string? Notes { get; set; }

        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string CourseId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        [ForeignKey("TrainerId")]
        public virtual User? Trainer { get; set; }

        public virtual ICollection<LessonProgress> LessonProgress { get; set; } = new List<LessonProgress>();
    }
}
