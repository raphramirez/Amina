﻿using Amina.Infrastructure.Multitenancy;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Amina.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    private readonly MultiTenantInfo _tenantInfo;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public ApplicationDbContext(MultiTenantInfo tenantInfo, IConfiguration configuration, IWebHostEnvironment env) : base(tenantInfo)
    {
        _tenantInfo = tenantInfo;
        _configuration = configuration;
        _env = env;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString;
        if (_tenantInfo is null && _env.IsDevelopment())
        {
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        else
        {
            connectionString = _tenantInfo.ApplicationConnectionString;
        }

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            optionsBuilder.UseDatabase(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }
}