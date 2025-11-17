using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RTOWebLMS.Models
{
    public class Quiz
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Quiz belongs to tenant
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int PassingScore { get; set; } = 80;

        public int TimeLimit { get; set; } = 30;

        public int MaxAttempts { get; set; } = 3;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public virtual ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
    }
}
