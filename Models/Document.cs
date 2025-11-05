using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class Document
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? FileName { get; set; }

        [MaxLength(255)]
        public string? FilePath { get; set; }

        [MaxLength(50)]
        public string? FileType { get; set; }

        public long? FileSize { get; set; }

        // Foreign Keys
        [Required]
        public string LessonId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("LessonId")]
        public virtual Lesson? Lesson { get; set; }
    }
}
