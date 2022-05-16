using Microsoft.AspNetCore.Mvc;

namespace InteracaoMedicamentosaLambdaAPI.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ProntuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
