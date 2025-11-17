using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RTOWebLMS.Models
{
    public class UnitySimulation
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Simulation belongs to tenant
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(255)]
        public string? BuildPath { get; set; }

        [MaxLength(50)]
        public string? Version { get; set; }

        public bool IsHighRisk { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<SimulationResult> SimulationResults { get; set; } = new List<SimulationResult>();
    }
}
