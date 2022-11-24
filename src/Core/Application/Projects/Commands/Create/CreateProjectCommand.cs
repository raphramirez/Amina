using Amina.Contracts.Projects;
using Amina.Domain.Project;
using ErrorOr;
using MediatR;

namespace Amina.Application.Projects.Commands.Create;

public record CreateProjectCommand(string Name, string Description) : IRequest<ErrorOr<Project>>;