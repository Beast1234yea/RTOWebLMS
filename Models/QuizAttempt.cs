using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class QuizAttempt
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Score { get; set; } = 0;

        public bool Passed { get; set; } = false;

        public int TimeSpent { get; set; } = 0;

        public DateTime? CompletedAt { get; set; }

        public string? Answers { get; set; }

        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string QuizId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("QuizId")]
        public virtual Quiz? Quiz { get; set; }
    }
}
