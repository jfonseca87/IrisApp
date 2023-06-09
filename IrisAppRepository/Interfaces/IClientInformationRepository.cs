namespace IrisAppRepository.Interfaces
{
    public interface IClientInformationRepository
    {
        Task<bool> SaveClientInformationData(Dictionary<string, string> data);
        Task<object> GetDatabaseData();
    }
}
