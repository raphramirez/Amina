using Amina.Application.Common.Interfaces.Persistence;
using Amina.Domain.Project;
using Amina.Domain.Project.ValueObjects;
using Amina.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Amina.Infrastructure.Persistence.Repository;

public sealed class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProjectRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task AddAsync(Project project)
    {
        await _applicationDbContext.AddAsync(project);
        await _applicationDbContext.SaveChangesAsync();
    }

    public IAsyncEnumerable<Project> GetAllAsync()
    {
        return _applicationDbContext.Projects.AsAsyncEnumerable();
    }

    public async Task<Project?> GetByIdAsync(ProjectId id)
    {
        return await _applicationDbContext.Projects
            .SingleOrDefaultAsync(project => project.Id == id);
    }
}