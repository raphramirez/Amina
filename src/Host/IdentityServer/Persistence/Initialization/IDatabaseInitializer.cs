using Amina.IdentityServer.Multitenancy;

namespace Amina.IdentityServer.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);

    Task InitializeApplicationDbForTenantAsync(MultiTenantInfo tenant, CancellationToken cancellationToken);
}