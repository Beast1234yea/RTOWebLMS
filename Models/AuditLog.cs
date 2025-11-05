using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTOWebLMS.Models
{
    /// <summary>
    /// Tracks all important actions performed in the system for compliance and security
    /// </summary>
    public class AuditLog
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Action { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? EntityType { get; set; }

        [MaxLength(255)]
        public string? EntityId { get; set; }

        public string? Details { get; set; }

        [MaxLength(50)]
        public string? IpAddress { get; set; }

        /// <summary>
        /// Severity level (INFO, WARNING, ERROR, CRITICAL)
        /// </summary>
        [MaxLength(20)]
        public string Severity { get; set; } = AuditSeverity.INFO;

        // Foreign Keys (nullable for system actions)
        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }

    /// <summary>
    /// Common audit action types
    /// </summary>
    public static class AuditActions
    {
        // Authentication
        public const string USER_LOGIN = "USER_LOGIN";
        public const string USER_LOGOUT = "USER_LOGOUT";
        public const string USER_LOGIN_FAILED = "USER_LOGIN_FAILED";
        public const string USER_REGISTERED = "USER_REGISTERED";
        public const string PASSWORD_CHANGED = "PASSWORD_CHANGED";
        public const string PASSWORD_RESET = "PASSWORD_RESET";

        // User Management
        public const string USER_CREATED = "USER_CREATED";
        public const string USER_UPDATED = "USER_UPDATED";
        public const string USER_DELETED = "USER_DELETED";

        // Course Management
        public const string COURSE_CREATED = "COURSE_CREATED";
        public const string COURSE_UPDATED = "COURSE_UPDATED";
        public const string COURSE_DELETED = "COURSE_DELETED";
        public const string COURSE_IMPORTED = "COURSE_IMPORTED";

        // Enrollment Management
        public const string ENROLLMENT_CREATED = "ENROLLMENT_CREATED";
        public const string ENROLLMENT_UPDATED = "ENROLLMENT_UPDATED";
        public const string ENROLLMENT_DELETED = "ENROLLMENT_DELETED";

        // Assessment Management
        public const string ASSESSMENT_CREATED = "ASSESSMENT_CREATED";
        public const string ASSESSMENT_UPDATED = "ASSESSMENT_UPDATED";
        public const string ASSESSMENT_GRADED = "ASSESSMENT_GRADED";
        public const string ASSESSMENT_DELETED = "ASSESSMENT_DELETED";

        // Certificate Management
        public const string CERTIFICATE_GENERATED = "CERTIFICATE_GENERATED";
        public const string CERTIFICATE_REVOKED = "CERTIFICATE_REVOKED";

        // AVETMISS
        public const string AVETMISS_EXPORTED = "AVETMISS_EXPORTED";

        // System
        public const string SYSTEM_ERROR = "SYSTEM_ERROR";
        public const string DATABASE_INITIALIZED = "DATABASE_INITIALIZED";
    }

    /// <summary>
    /// Severity levels for audit logs
    /// </summary>
    public static class AuditSeverity
    {
        public const string INFO = "INFO";
        public const string WARNING = "WARNING";
        public const string ERROR = "ERROR";
        public const string CRITICAL = "CRITICAL";
    }
}
