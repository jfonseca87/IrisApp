using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using IrisAppRepository.Domain;
using IrisAppRepository.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IrisAppRepository.Implementations
{
    public class DynamoClientInformationRepository : IClientInformationRepository
    {
        private readonly Table _table;

        public DynamoClientInformationRepository(IOptions<AwsSettings> settings)
        {
            var dynamoClient = new AmazonDynamoDBClient(settings.Value.UserId, settings.Value.SecretAccessKey);
            _table = Table.LoadTable(dynamoClient, settings.Value.DynamoTable);
        }

        public Task<object> GetDatabaseData()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveClientInformationData(Dictionary<string, string> data)
        {
            Document documentObject = new Document();
            documentObject["PK"] = DateTime.Now.Ticks.ToString();
            documentObject["ClientInformation"] = JsonConvert.SerializeObject(data);

            await _table.PutItemAsync(documentObject);
            return true;
        }
    }
}
