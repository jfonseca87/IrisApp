namespace IrisApp.Services.Interfaces
{
    public interface IFileService
    {
        Task<string[]> GetFiles();
        Task<bool> DeleteFile(string filePath);
        Task<Dictionary<string, string>> GetFileContentAsync(string filePath);
    }
}
