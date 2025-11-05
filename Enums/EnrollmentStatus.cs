namespace RTOWebLMS.Enums
{
    public enum EnrollmentStatus
    {
        PENDING,        // Awaiting confirmation
        CONFIRMED,      // Confirmed and ready to start
        IN_PROGRESS,    // Currently active/in progress
        ACTIVE,         // Legacy - same as IN_PROGRESS
        COMPLETED,      // Successfully completed
        WITHDRAWN,      // Student withdrew
        CANCELLED,      // Cancelled by RTO
        EXPIRED,        // Enrollment expired
        FAILED          // Failed to complete
    }
}
