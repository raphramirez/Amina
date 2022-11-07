using Amina.IdentityServer.Identity;
using Amina.IdentityServer.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Amina.IdentityServer.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly MultiTenantInfo _tenant;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMultiTenantContextAccessor<MultiTenantInfo> accessor,
        IWebHostEnvironment env,
        IConfiguration configuration
        ) : base(options)
    {
        _tenant = accessor.MultiTenantContext?.TenantInfo;
        _env = env;
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString;

        if (_tenant is null && _env.IsDevelopment())
        {
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        else
        {
            connectionString = _tenant.ConnectionString;
        }

        optionsBuilder.UseNpgsql(connectionString);

        base.OnConfiguring(optionsBuilder);
    }
}