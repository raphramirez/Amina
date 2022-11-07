using Finbuckle.MultiTenant;

namespace Amina.IdentityServer.Multitenancy;

public class MultiTenantInfo : ITenantInfo
{
    public string Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
}