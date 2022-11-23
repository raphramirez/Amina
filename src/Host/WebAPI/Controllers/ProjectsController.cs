using Amina.Application.Projects.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Amina.WebAPI.Controllers;

public class ProjectsController : ApiController
{
    private readonly ISender _mediator;

    public ProjectsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var result = await _mediator.Send(new ProjectsListQuery());

        return Ok(result);
    }
}