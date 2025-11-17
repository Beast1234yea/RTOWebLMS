using Microsoft.EntityFrameworkCore;
using RTOWebLMS.Data;
using RTOWebLMS.Services;

namespace RTOWebLMS.Middleware
{
    /// <summary>
    /// Middleware to resolve and set the current tenant based on subdomain or header
    /// </summary>
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantService tenantService, LmsDbContext dbContext)
        {
            // Strategy 1: Get tenant from subdomain (e.g., acme.rtowebms.com.au)
            var host = context.Request.Host.Host;
            var subdomain = ExtractSubdomain(host);

            string? tenantId = null;

            if (!string.IsNullOrEmpty(subdomain))
            {
                // Look up tenant by subdomain
                var tenant = await dbContext.Tenants
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Subdomain == subdomain && t.IsActive);

                if (tenant != null)
                {
                    tenantId = tenant.Id;
                }
            }

            // Strategy 2: Fallback to header (for API calls or development)
            if (tenantId == null && context.Request.Headers.TryGetValue("X-Tenant-Id", out var headerTenantId))
            {
                tenantId = headerTenantId.ToString();
            }

            // Strategy 3: For development/single-tenant mode, use default tenant
            if (tenantId == null && context.Request.Host.Host.Contains("localhost"))
            {
                // In development, use the first active tenant or create a default one
                var defaultTenant = await dbContext.Tenants
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.IsActive);

                if (defaultTenant != null)
                {
                    tenantId = defaultTenant.Id;
                }
            }

            // Set the tenant ID for this request
            if (tenantId != null)
            {
                tenantService.SetCurrentTenantId(tenantId);
            }

            await _next(context);
        }

        private string? ExtractSubdomain(string host)
        {
            // Remove port if present
            var hostWithoutPort = host.Split(':')[0];

            // Skip localhost and IP addresses
            if (hostWithoutPort == "localhost" ||
                hostWithoutPort.StartsWith("127.") ||
                hostWithoutPort.StartsWith("192.168.") ||
                System.Net.IPAddress.TryParse(hostWithoutPort, out _))
            {
                return null;
            }

            // Split by dots
            var parts = hostWithoutPort.Split('.');

            // Need at least 3 parts for subdomain (subdomain.domain.com)
            if (parts.Length < 3)
            {
                return null;
            }

            // First part is the subdomain
            return parts[0];
        }
    }

    /// <summary>
    /// Extension method to add TenantMiddleware to the pipeline
    /// </summary>
    public static class TenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantResolution(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
