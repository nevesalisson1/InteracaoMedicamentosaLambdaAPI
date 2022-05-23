using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Interacao.Domain.DAO;
using Interacao.Entity;
using Interacao.Infrastructure.DynamoDB.Contracts;

namespace Interacao.Infrasctructure.DynamoDB.Repository
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly DynamoDBContext _context;

        public MedicamentoRepository()
        {
            _client = new AmazonDynamoDBClient();
            _context = new DynamoDBContext(_client);
        }

        public Task AdicionarNovoMedicamento(Medicamento entity)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Medicamento>> ObterTodosOsMedicamentos()
        {
            var scanConditions = new List<ScanCondition>() {
                new ScanCondition("Id", ScanOperator.IsNotNull) 
            };

            var searchResults = _context.ScanAsync<MedicamentoContract>(scanConditions, null);
            var tabela = await searchResults.GetNextSetAsync();

            List<Medicamento> retorno = new List<Medicamento>();

            foreach(MedicamentoContract entity in tabela)
            {
                retorno.Add(new Medicamento
                {
                    Id = entity.Id,
                    Nome = entity.Nome,
                    Dosagem = entity.Dosagem
                });
            }

            return retorno;
        }
    }
}