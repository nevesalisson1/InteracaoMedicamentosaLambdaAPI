using Interacao.Entity;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interacao.Infrasctructure.Http;
using System.Text.Json;

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
    }
}
