using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Amina.WebAPI.Controllers;

[Route("[controller]")]
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
        await Task.CompletedTask;

        return Ok();
    }
}