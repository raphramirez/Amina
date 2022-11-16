using Amina.Infrastructure.Multitenancy;

namespace Amina.Infrastructure.Persistence.Initialization;

internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);

    Task InitializeDbForTenantAsync(MultiTenantInfo tenant, CancellationToken cancellationToken);
}