using Amina.Application.Projects.Commands.Create;
using Amina.Application.Projects.Queries.List;
using Amina.Contracts.Projects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Amina.WebAPI.Controllers;

public class ProjectsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public ProjectsController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _mediator.Send(new ProjectsListQuery());

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest request)
    {
        var command = _mapper.Map<CreateProjectCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
            project => Ok(_mapper.Map<CreateProjectResponse>(project)),
            errors => Problem(errors));
    }
}