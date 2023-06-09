namespace IrisApp.Services.Interfaces
{
    public interface ISecurityService
    {
        string GenerateMd5Hash(Dictionary<string, string> fileContent);
    }
}
