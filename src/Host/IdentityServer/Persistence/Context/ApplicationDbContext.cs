using Amina.IdentityServer.Identity;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;

namespace Amina.IdentityServer.Persistence.Context;

public class ApplicationDbContext : MultiTenantIdentityDbContext<ApplicationUser>
{
    private readonly ITenantInfo _tenant;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, IWebHostEnvironment env) : base(tenantInfo, options)
    {
        _configuration = configuration;
        _env = env;

        _tenant = tenantInfo;
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