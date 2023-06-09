using IrisApp.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace IrisApp.Services.Implementations
{
    public class SecurityService : ISecurityService
    {
        public string GenerateMd5Hash(Dictionary<string, string> fileContent)
        {
            var values = fileContent.Values.ToList();
            values.RemoveAt(values.Count - 1);
            string hash = string.Join('~', values);
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(hash);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
