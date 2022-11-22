using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;

namespace Amina.Infrastructure.Multitenancy;

public class TenantDbContext : EFCoreStoreDbContext<MultiTenantInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
    }
}