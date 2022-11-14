using Amina.Infrastructure.Multitenancy;

namespace Amina.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);

    Task InitializeIdentityDbForTenantAsync(MultiTenantInfo tenant, CancellationToken cancellationToken);

    Task InitializeApplicationDbForTenantAsync(MultiTenantInfo tenant, CancellationToken cancellationToken);
}