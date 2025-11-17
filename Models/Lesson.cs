using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class Lesson
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Inherited from Module/Course for query performance
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public LessonType Type { get; set; } = LessonType.TEXT;

        public int OrderIndex { get; set; } = 0;

        // Legacy property for backward compatibility with old code
        // Not mapped to database - only OrderIndex column exists
        [NotMapped]
        public int Order
        {
            get => OrderIndex;
            set => OrderIndex = value;
        }

        public int? Duration { get; set; }

        [MaxLength(255)]
        public string? VideoUrl { get; set; }

        public bool IsRequired { get; set; } = true;

        public bool IsPublished { get; set; } = false;

        // Foreign Keys
        [Required]
        public string ModuleId { get; set; } = string.Empty;

        public string? SimulationId { get; set; }

        public string? QuizId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }

        [ForeignKey("ModuleId")]
        public virtual Module? Module { get; set; }

        [ForeignKey("SimulationId")]
        public virtual UnitySimulation? Simulation { get; set; }

        [ForeignKey("QuizId")]
        public virtual Quiz? Quiz { get; set; }

        public virtual ICollection<LessonProgress> LessonProgress { get; set; } = new List<LessonProgress>();
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        public virtual ICollection<LessonMedia> Media { get; set; } = new List<LessonMedia>();
    }
}
