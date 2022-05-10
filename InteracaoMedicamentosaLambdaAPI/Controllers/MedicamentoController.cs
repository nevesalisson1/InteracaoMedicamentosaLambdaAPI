using Interacao.Entity;
using Interacao.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InteracaoMedicamentosaLambdaAPI.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        public Medicamento medicamento;

        // GET api/<MedicamentoController>/5
        [HttpPost]
        public ActionResult<string> ObterMedicamento(string nome)
        {
            MedicamentoService medicamentoService = new MedicamentoService();
            object medicamento = medicamentoService.ObterMedicamento(nome);
            if(medicamento == null)
            {
                return BadRequest();
            }
            return Ok(medicamento);
        }

    }
}
