using Amina.Application.Common.Interfaces.Persistence;
using Amina.Contracts.Projects;
using MapsterMapper;
using MediatR;

namespace Amina.Application.Projects.Queries.List;

public class ProjectsListQueryHandler : IRequestHandler<ProjectsListQuery, List<ProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectsListQueryHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    async Task<List<ProjectResponse>> IRequestHandler<ProjectsListQuery, List<ProjectResponse>>.Handle(ProjectsListQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllAsync().ToListAsync();

        var response = _mapper.Map<List<ProjectResponse>>(projects);

        return response;
    }
}