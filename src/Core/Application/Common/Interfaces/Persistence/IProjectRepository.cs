using Amina.Domain.Project;
using Amina.Domain.Project.ValueObjects;

namespace Amina.Application.Common.Interfaces.Persistence;

public interface IProjectRepository : IRepository<Project>
{
    IAsyncEnumerable<Project> GetAllAsync();

    Task<Project?> GetByIdAsync(ProjectId id);

    Task AddAsync(Project project);
}