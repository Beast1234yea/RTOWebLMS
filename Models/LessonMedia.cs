using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class LessonMedia
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Caption { get; set; }

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string MediaType { get; set; } = "IMAGE"; // IMAGE, SLIDESHOW

        public int OrderIndex { get; set; } = 0;

        // Foreign Keys
        [Required]
        public string LessonId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("LessonId")]
        public virtual Lesson? Lesson { get; set; }
    }
}
