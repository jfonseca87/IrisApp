using IrisAppRepository.Interfaces;
using Newtonsoft.Json;

namespace IrisAppRepository.Implementations
{
    public class LocalClientInformationRepository : IClientInformationRepository
    {
        private readonly static List<DataModel> clientData = new List<DataModel>();

        public async Task<object> GetDatabaseData()
        {
            return await Task.Run(() =>
            {
                return clientData;
            });
        }

        public async Task<bool> SaveClientInformationData(Dictionary<string, string> data)
        {
            await Task.Run(() => 
            {
                clientData.Add(new DataModel
                {
                    TimeStamp = DateTime.Now.Ticks.ToString(),
                    ClientInformation = JsonConvert.SerializeObject(data)
                });
            });

            return true;
        }


    }

    public class DataModel
    {
        public string TimeStamp { get; set; }
        public string ClientInformation { get; set; }
    }
}
