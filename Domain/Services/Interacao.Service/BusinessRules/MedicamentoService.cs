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
using Interacao.Domain.DAO;

namespace Interacao.Service.BusinessRules
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoRepository _repository;

        public MedicamentoService(IMedicamentoRepository repository)
        {
            _repository = repository;
        }

        public List<Medicamento> ListarMedicamentosDaBase()
        {
            var retorno = _repository.ObterTodosOsMedicamentos();

            return retorno.Result;
        }

        public object ObterMedicamento(string medicamentoNome)
        {
            string urlComplementar = $"/drugs.json?name={medicamentoNome}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            var jsonString = JsonSerializer.Deserialize<object>(result);

            return jsonString; 
        }

        public List<ResultadoInteracaoDTO> VerificaInteracoes(string rxcui1, string rxcui2)
        {
            string urlComplementar = $"/interaction/list.json?rxcuis={rxcui1}+{rxcui2}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            try
            {
                var data = JObject.Parse(result);

                List<ResultadoInteracaoDTO> listaRetorno = new List<ResultadoInteracaoDTO>();

                var interacoesList = data["fullInteractionTypeGroup"];

                foreach (var interacao in interacoesList)
                {
                    listaRetorno.Add(new ResultadoInteracaoDTO
                    {
                            Severidade = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["severity"],
                            Descricao = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["description"],
                            Fonte = (string)interacao["sourceName"]
                    }
                    );
                }

                //ResultadoInteracaoDTO retorno = new ResultadoInteracaoDTO()
                //{
                //    Severidade = (string)data["fullInteractionTypeGroup"][0]["fullInteractionType"][0]["interactionPair"][0]["severity"],
                //    Descricao = (string)data["fullInteractionTypeGroup"][0]["fullInteractionType"][0]["interactionPair"][0]["description"],
                //    Fonte = (string)data["fullInteractionTypeGroup"][0]["sourceName"]
                //};
                return listaRetorno;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResultadoInteracaoDTO ObtemMaiorInteracao(string rxcui1, string rxcui2)
        {
            string urlComplementar = $"/interaction/list.json?rxcuis={rxcui1}+{rxcui2}";

            var result = HttpClientDrugInteraction.RealizarRequisiscao(urlComplementar);

            try
            {
                var data = JObject.Parse(result);
                ResultadoInteracaoDTO retorno = null;

                var interacoesList = data["fullInteractionTypeGroup"];
                foreach (var interacao in interacoesList)
                {
                    string severidade = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["severity"];

                    if (severidade == "high") 
                    {
                        retorno = new ResultadoInteracaoDTO()
                        {
                            Severidade = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["severity"],
                            Descricao = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["description"],
                            Fonte = (string)interacao["sourceName"]
                        };
                    }
                    else if (retorno?.Severidade != "high")
                    {
                        retorno = new ResultadoInteracaoDTO()
                        {
                            Severidade = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["severity"],
                            Descricao = (string)interacao["fullInteractionType"][0]["interactionPair"][0]["description"],
                            Fonte = (string)interacao["sourceName"]
                        };
                    }
                }

                return retorno;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
