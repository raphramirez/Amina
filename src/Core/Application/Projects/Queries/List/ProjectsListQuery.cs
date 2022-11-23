using Amina.Contracts.Projects;
using MediatR;

namespace Amina.Application.Projects.Queries.List;
public record ProjectsListQuery() : IRequest<List<ProjectResponse>>;