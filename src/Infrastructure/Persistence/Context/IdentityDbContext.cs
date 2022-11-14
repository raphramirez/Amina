using Amina.Infrastructure.Identity;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Amina.Infrastructure.Persistence.Context;

public class IdentityDbContext : MultiTenantIdentityDbContext<ApplicationUser>
{
    private readonly ITenantInfo _tenant;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;

    public IdentityDbContext(ITenantInfo tenantInfo, DbContextOptions<IdentityDbContext> options, IConfiguration configuration, IWebHostEnvironment env) : base(tenantInfo, options)
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

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            optionsBuilder.UseDatabase(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }
}