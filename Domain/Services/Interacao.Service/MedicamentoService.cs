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

namespace Interacao.Service
{
    public class MedicamentoService
    {
        public object ObterMedicamento(string medicamentoNome)
        {
            string urlComplementar = $"/drugs.json?name={medicamentoNome}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            var jsonString = JsonSerializer.Deserialize<object>(result);

            return jsonString; 
        }

        public ResultadoInteracao VerificaInteracoes(string rxcui1, string rxcui2)
        {
            string urlComplementar = $"/interaction/list.json?rxcuis={rxcui1}+{rxcui2}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            try
            {
                var data = JObject.Parse(result);

                ResultadoInteracao retorno = new ResultadoInteracao()
                {
                    severidade = (string)data["fullInteractionTypeGroup"][0]["fullInteractionType"][0]["interactionPair"][0]["severity"],
                    descricao = (string)data["fullInteractionTypeGroup"][0]["fullInteractionType"][0]["interactionPair"][0]["description"],
                    fonte = (string)data["fullInteractionTypeGroup"][0]["sourceName"]
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
