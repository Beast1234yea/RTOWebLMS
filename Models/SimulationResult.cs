using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    public class SimulationResult
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Inherited from User/Simulation
        [Required]
        public string TenantId { get; set; } = string.Empty;

        public int Score { get; set; } = 0;

        public int TimeSpent { get; set; } = 0;

        public bool Completed { get; set; } = false;

        public string? ResultData { get; set; }

        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string SimulationId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("SimulationId")]
        public virtual UnitySimulation? Simulation { get; set; }
    }
}
