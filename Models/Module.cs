using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class Module
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int OrderIndex { get; set; } = 0;

        // Legacy property for backward compatibility with old code
        // Not mapped to database - only OrderIndex column exists
        [NotMapped]
        public int Order
        {
            get => OrderIndex;
            set => OrderIndex = value;
        }

        public bool IsPublished { get; set; } = false;

        // Foreign Keys
        [Required]
        public string CourseId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
