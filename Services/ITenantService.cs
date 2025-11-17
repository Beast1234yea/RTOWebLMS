namespace RTOWebLMS.Services
{
    /// <summary>
    /// Service for managing the current tenant context
    /// Used throughout the application to ensure data isolation
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// Gets the current tenant ID for the request
        /// </summary>
        string? GetCurrentTenantId();

        /// <summary>
        /// Sets the current tenant ID for the request
        /// </summary>
        void SetCurrentTenantId(string tenantId);

        /// <summary>
        /// Gets the current tenant name (for display purposes)
        /// </summary>
        string? GetCurrentTenantName();

        /// <summary>
        /// Checks if a tenant is currently set
        /// </summary>
        bool HasTenant();
    }
}
