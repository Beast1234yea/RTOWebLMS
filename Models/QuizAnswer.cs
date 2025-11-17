using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class QuizAnswer
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Inherited from QuizQuestion
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        public string AnswerText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; } = false;

        // Foreign Keys
        [Required]
        public string QuestionId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }

        [ForeignKey("QuestionId")]
        public virtual QuizQuestion? Question { get; set; }
    }
}
