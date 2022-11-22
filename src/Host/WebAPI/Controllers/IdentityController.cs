using Microsoft.AspNetCore.Mvc;

namespace Amina.WebAPI.Controllers;

[Route("[controller]")]
public class IdentityController : ApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
    }
}