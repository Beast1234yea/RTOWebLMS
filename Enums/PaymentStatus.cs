namespace RTOWebLMS.Enums
{
    public enum PaymentStatus
    {
        PENDING,    // Payment not yet received
        PARTIAL,    // Partial payment received
        PAID,       // Fully paid
        REFUNDED,   // Payment refunded
        CANCELLED   // Payment cancelled
    }
}
