using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RTOWebLMS.Enums;

namespace RTOWebLMS.Models
{
    public class Certificate
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Multi-tenancy: Certificate belongs to tenant
        [Required]
        public string TenantId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string CertificateNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? CertificateType { get; set; } = "Statement of Attainment";

        public CertificateStatus Status { get; set; } = CertificateStatus.ISSUED;

        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        [MaxLength(255)]
        public string? PdfUrl { get; set; }

        [MaxLength(255)]
        public string? VerificationCode { get; set; }

        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string CourseId { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual Tenant? Tenant { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
