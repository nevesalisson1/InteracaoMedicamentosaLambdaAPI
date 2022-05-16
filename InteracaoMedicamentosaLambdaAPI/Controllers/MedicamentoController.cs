using Interacao.Domain.DTO;
using Interacao.Entity;
using Interacao.Service.BusinessRules;
using Interacao.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InteracaoMedicamentosaLambdaAPI.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private Medicamento medicamento;
        private readonly IMedicamentoService _medicamentoService;

        public MedicamentoController(IMedicamentoService medicamentoService)
        {
            _medicamentoService = medicamentoService;
        }

        [HttpGet]
        public ActionResult<string> ObterMedicamento(string nome)
        {
            object medicamento = _medicamentoService.ObterMedicamento(nome);
            if(medicamento == null)
            {
                return BadRequest();
            }
            return Ok(medicamento);
        }

        [HttpGet]
        public ActionResult<ResultadoInteracaoDTO> VerificaInteracoes(string rxcui1, string rxcui2)
        {
            ResultadoInteracaoDTO resultadoInteracao = _medicamentoService.VerificaInteracoes(rxcui1, rxcui2);

            if(resultadoInteracao == null)
            {
                return BadRequest();
            }

            return Ok(resultadoInteracao);
        }

    }
}
