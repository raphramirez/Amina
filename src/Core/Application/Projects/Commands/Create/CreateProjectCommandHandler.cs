using Amina.Application.Common.Interfaces.Persistence;
using Amina.Contracts.Projects;
using Amina.Domain.Project;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace Amina.Application.Projects.Commands.Create;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ErrorOr<Project>>
{
    private readonly IProjectRepository _projectRepository;

    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Project>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var newProject = Project.Create(request.Name, request.Description);

        await _projectRepository.AddAsync(newProject);

        return newProject;
    }
}