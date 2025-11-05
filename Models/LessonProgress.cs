using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class LessonProgress
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public bool Completed { get; set; } = false;

        public int TimeSpent { get; set; } = 0;

        public DateTime? CompletedAt { get; set; }

        // Foreign Keys
        [Required]
        public string EnrollmentId { get; set; } = string.Empty;

        [Required]
        public string LessonId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment? Enrollment { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson? Lesson { get; set; }
    }
}
