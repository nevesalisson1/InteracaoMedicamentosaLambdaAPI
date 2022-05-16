using Interacao.Entity;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interacao.Infrasctructure.Http;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Interacao.Domain.DTO;
using Interacao.Service.Interfaces;

namespace Interacao.Service.BusinessRules
{
    public class MedicamentoService : IMedicamentoService
    {
        public object ObterMedicamento(string medicamentoNome)
        {
            string urlComplementar = $"/drugs.json?name={medicamentoNome}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            var jsonString = JsonSerializer.Deserialize<object>(result);

            return jsonString; 
        }

        public ResultadoInteracaoDTO VerificaInteracoes(string rxcui1, string rxcui2)
        {
            string urlComplementar = $"/interaction/list.json?rxcuis={rxcui1}+{rxcui2}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            try
            {
                var data = JObject.Parse(result);

                ResultadoInteracaoDTO retorno = new ResultadoInteracaoDTO()
                {
                    Severidade = (string)data["fullInteractionTypeGroup"][0]["fullInteractionType"][0]["interactionPair"][0]["severity"],
                    Descricao = (string)data["fullInteractionTypeGroup"][0]["fullInteractionType"][0]["interactionPair"][0]["description"],
                    Fonte = (string)data["fullInteractionTypeGroup"][0]["sourceName"]
                };
                return retorno;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
