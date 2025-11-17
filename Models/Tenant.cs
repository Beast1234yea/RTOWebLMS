using System;
using System.ComponentModel.DataAnnotations;

namespace RTOWebLMS.Models
{
    /// <summary>
    /// Represents a Registered Training Organization (RTO) tenant
    /// Each tenant is a separate customer with isolated data
    /// </summary>
    public class Tenant
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Subdomain { get; set; }  // e.g., "acme" for acme.rtowebms.com.au

        // RTO Details
        [MaxLength(50)]
        public string? RTOCode { get; set; }  // e.g., "12345"

        [MaxLength(50)]
        public string? ABN { get; set; }  // Australian Business Number

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Website { get; set; }

        // Subscription Details
        public SubscriptionPlan Plan { get; set; } = SubscriptionPlan.Trial;

        public int MaxStudents { get; set; } = 50;  // Based on plan

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? SubscriptionStartDate { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public DateTime? TrialEndsAt { get; set; }

        // Branding (for white-label)
        [MaxLength(500)]
        public string? LogoUrl { get; set; }

        [MaxLength(50)]
        public string? PrimaryColor { get; set; }  // Hex color code

        // Relationships
        public virtual ICollection<User>? Users { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }

    public enum SubscriptionPlan
    {
        Trial,          // 14 days free
        Starter,        // $99/month - 50 students
        Professional,   // $299/month - 200 students
        Enterprise      // $799/month - 1000 students
    }
}
