using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Amina.Infrastructure.Persistence.Context;

public abstract class BaseDbContext : DbContext, IMultiTenantDbContext
{
    public ITenantInfo TenantInfo { get; internal set; }
    public TenantMismatchMode TenantMismatchMode { get; set; } = TenantMismatchMode.Throw;
    public TenantNotSetMode TenantNotSetMode { get; set; } = TenantNotSetMode.Throw;

    protected BaseDbContext(ITenantInfo tenantInfo)
    {
        TenantInfo = tenantInfo;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // If necessary call the base class method.
        // Recommended to be called first.
        base.OnModelCreating(modelBuilder);

        // Configure all entity types marked with the [MultiTenant] data attribute
        modelBuilder.ConfigureMultiTenant();

        // Configure an entity type to be multitenant.
        //modelBuilder.Entity<MyEntityType>().IsMultiTenant();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.EnforceMultiTenant();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        this.EnforceMultiTenant();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}