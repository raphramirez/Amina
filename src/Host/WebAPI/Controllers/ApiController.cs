using Microsoft.AspNetCore.Mvc;

namespace Amina.WebAPI.Controllers;

public class ApiController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}