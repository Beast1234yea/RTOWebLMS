using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;

namespace RTOWebLMS.Services
{
    /// <summary>
    /// Implementation of ITenantService using AsyncLocal for thread-safe tenant context
    /// </summary>
    public class TenantService : ITenantService
    {
        private static readonly AsyncLocal<string?> _currentTenantId = new AsyncLocal<string?>();
        private static readonly AsyncLocal<string?> _currentTenantName = new AsyncLocal<string?>();
        private readonly LmsDbContext _dbContext;

        public TenantService(LmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string? GetCurrentTenantId()
        {
            return _currentTenantId.Value;
        }

        public void SetCurrentTenantId(string tenantId)
        {
            _currentTenantId.Value = tenantId;

            // Optionally load tenant name for display
            var tenant = _dbContext.Tenants.FirstOrDefault(t => t.Id == tenantId);
            if (tenant != null)
            {
                _currentTenantName.Value = tenant.Name;
            }
        }

        public string? GetCurrentTenantName()
        {
            return _currentTenantName.Value;
        }

        public bool HasTenant()
        {
            return !string.IsNullOrEmpty(_currentTenantId.Value);
        }
    }
}
