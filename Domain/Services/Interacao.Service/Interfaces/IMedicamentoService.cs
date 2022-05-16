using Interacao.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interacao.Service.Interfaces
{
    public interface IMedicamentoService
    {
        public ResultadoInteracaoDTO VerificaInteracoes(string rxcui1, string rxcui2);
        public object ObterMedicamento(string medicamentoNome);
    }
}
