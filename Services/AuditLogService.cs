using System;
using System.Threading.Tasks;
using RTOWebLMS.Data;
using RTOWebLMS.Models;

namespace RTOWebLMS.Services
{
    /// <summary>
    /// Service for creating and managing audit logs
    /// </summary>
    public class AuditLogService
    {
        private readonly LmsDbContext _dbContext;

        public AuditLogService(LmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Log an action performed by a user
        /// </summary>
        public async Task LogAsync(
            string action,
            string? userId = null,
            string? entityType = null,
            string? entityId = null,
            string? details = null,
            string severity = AuditSeverity.INFO,
            string? ipAddress = null)
        {
            try
            {
                var auditLog = new AuditLog
                {
                    Action = action,
                    UserId = userId,
                    EntityType = entityType,
                    EntityId = entityId,
                    Details = details,
                    Severity = severity,
                    IpAddress = ipAddress,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.AuditLogs.Add(auditLog);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Don't throw - audit logging should never break the application
                // In production, you might want to log this to a file or monitoring service
                System.Diagnostics.Debug.WriteLine($"Failed to create audit log: {ex.Message}");
            }
        }

        /// <summary>
        /// Log a user login event
        /// </summary>
        public async Task LogLoginAsync(string userId, string email, bool success, string? errorMessage = null)
        {
            await LogAsync(
                action: success ? AuditActions.USER_LOGIN : AuditActions.USER_LOGIN_FAILED,
                userId: success ? userId : null,
                details: success
                    ? $"User {email} logged in successfully"
                    : $"Failed login attempt for {email}: {errorMessage}",
                severity: success ? AuditSeverity.INFO : AuditSeverity.WARNING
            );
        }

        /// <summary>
        /// Log a user logout event
        /// </summary>
        public async Task LogLogoutAsync(string userId, string userName)
        {
            await LogAsync(
                action: AuditActions.USER_LOGOUT,
                userId: userId,
                details: $"User {userName} logged out"
            );
        }

        /// <summary>
        /// Log a user registration event
        /// </summary>
        public async Task LogRegistrationAsync(string userId, string email, string role)
        {
            await LogAsync(
                action: AuditActions.USER_REGISTERED,
                userId: userId,
                entityType: "User",
                entityId: userId,
                details: $"New {role} account created for {email}"
            );
        }

        /// <summary>
        /// Log a password change
        /// </summary>
        public async Task LogPasswordChangeAsync(string userId, string userName, bool isReset = false)
        {
            await LogAsync(
                action: isReset ? AuditActions.PASSWORD_RESET : AuditActions.PASSWORD_CHANGED,
                userId: userId,
                entityType: "User",
                entityId: userId,
                details: isReset
                    ? $"Password reset for user {userName}"
                    : $"Password changed by user {userName}",
                severity: isReset ? AuditSeverity.WARNING : AuditSeverity.INFO
            );
        }

        /// <summary>
        /// Log a course creation
        /// </summary>
        public async Task LogCourseCreatedAsync(string courseId, string courseTitle, string? userId = null, bool isImported = false)
        {
            await LogAsync(
                action: isImported ? AuditActions.COURSE_IMPORTED : AuditActions.COURSE_CREATED,
                userId: userId,
                entityType: "Course",
                entityId: courseId,
                details: $"Course '{courseTitle}' was " + (isImported ? "imported" : "created")
            );
        }

        /// <summary>
        /// Log a course update
        /// </summary>
        public async Task LogCourseUpdatedAsync(string courseId, string courseTitle, string? userId = null, string? changes = null)
        {
            await LogAsync(
                action: AuditActions.COURSE_UPDATED,
                userId: userId,
                entityType: "Course",
                entityId: courseId,
                details: $"Course '{courseTitle}' was updated. {changes}"
            );
        }

        /// <summary>
        /// Log a course deletion
        /// </summary>
        public async Task LogCourseDeletedAsync(string courseId, string courseTitle, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.COURSE_DELETED,
                userId: userId,
                entityType: "Course",
                entityId: courseId,
                details: $"Course '{courseTitle}' was deleted",
                severity: AuditSeverity.WARNING
            );
        }

        /// <summary>
        /// Log an enrollment creation
        /// </summary>
        public async Task LogEnrollmentCreatedAsync(string enrollmentId, string studentName, string courseTitle, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.ENROLLMENT_CREATED,
                userId: userId,
                entityType: "Enrollment",
                entityId: enrollmentId,
                details: $"Student {studentName} enrolled in course '{courseTitle}'"
            );
        }

        /// <summary>
        /// Log an assessment being graded
        /// </summary>
        public async Task LogAssessmentGradedAsync(string assessmentId, string studentName, string courseTitle, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.ASSESSMENT_GRADED,
                userId: userId,
                entityType: "Assessment",
                entityId: assessmentId,
                details: $"Assessment graded for student {studentName} in course '{courseTitle}'"
            );
        }

        /// <summary>
        /// Log a certificate generation
        /// </summary>
        public async Task LogCertificateGeneratedAsync(string certificateId, string certificateNumber, string studentName, string courseTitle, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.CERTIFICATE_GENERATED,
                userId: userId,
                entityType: "Certificate",
                entityId: certificateId,
                details: $"Certificate {certificateNumber} generated for student {studentName} - Course: '{courseTitle}'"
            );
        }

        /// <summary>
        /// Log a certificate revocation
        /// </summary>
        public async Task LogCertificateRevokedAsync(string certificateId, string certificateNumber, string reason, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.CERTIFICATE_REVOKED,
                userId: userId,
                entityType: "Certificate",
                entityId: certificateId,
                details: $"Certificate {certificateNumber} was revoked. Reason: {reason}",
                severity: AuditSeverity.WARNING
            );
        }

        /// <summary>
        /// Log an AVETMISS export
        /// </summary>
        public async Task LogAvetmissExportAsync(string rtoIdentifier, DateTime startDate, DateTime endDate, int fileCount, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.AVETMISS_EXPORTED,
                userId: userId,
                details: $"AVETMISS files exported for RTO {rtoIdentifier}. Period: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}. Files: {fileCount}"
            );
        }

        /// <summary>
        /// Log a system error
        /// </summary>
        public async Task LogErrorAsync(string errorMessage, string? stackTrace = null, string? userId = null)
        {
            await LogAsync(
                action: AuditActions.SYSTEM_ERROR,
                userId: userId,
                details: $"{errorMessage}\n\nStack Trace:\n{stackTrace}",
                severity: AuditSeverity.ERROR
            );
        }

        /// <summary>
        /// Log a critical system event
        /// </summary>
        public async Task LogCriticalAsync(string message, string? details = null, string? userId = null)
        {
            await LogAsync(
                action: message,
                userId: userId,
                details: details,
                severity: AuditSeverity.CRITICAL
            );
        }
    }
}
