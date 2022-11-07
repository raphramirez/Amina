using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;

namespace Amina.IdentityServer.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<MultiTenantInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
    {
    }
}