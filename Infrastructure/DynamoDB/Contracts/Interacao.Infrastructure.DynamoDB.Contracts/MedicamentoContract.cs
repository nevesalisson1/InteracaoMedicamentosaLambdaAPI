using Amazon.DynamoDBv2.DataModel;

namespace Interacao.Infrastructure.DynamoDB.Contracts
{
    [DynamoDBTable("medicamento")]
    public class MedicamentoContract
    {
        [DynamoDBProperty("id")]
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBProperty("nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("dosagem")]
        public string Dosagem { get; set; }
    }
}