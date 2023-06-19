using Microsoft.AspNetCore.Mvc;
using Exercicio.ModelViews;

namespace Exercicio.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult Index()
    {
        return StatusCode(200, new Home {
            Message = "Bem vindo a minha api"
        });
    }
}
