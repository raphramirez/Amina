using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Amina.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public ApplicationDbContext(ITenantInfo tenantInfo, IConfiguration configuration, IWebHostEnvironment env) : base(tenantInfo)
    {
        _configuration = configuration;
        _env = env;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString;
        if (TenantInfo is null && _env.IsDevelopment())
        {
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        else
        {
            connectionString = TenantInfo.ConnectionString;
        }

        optionsBuilder.UseNpgsql(connectionString);

        base.OnConfiguring(optionsBuilder);
    }
}