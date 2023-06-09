using IrisApp.Services.Interfaces;
using System.Text;

namespace IrisApp.Services.Implementations
{
    public class LocalFileService : IFileService
    {
        private readonly string folderPath;

        public LocalFileService()
        {
            folderPath = @"c:\temporal\";
        }

        public async Task<bool> DeleteFile(string filePath)
        {
            return await Task.Run(() =>
            {
                File.Delete(filePath);
                return true;
            });
        }

        public async Task<string[]> GetFiles()
        {
            return await Task.Run(() => 
            {
                return Directory.GetFiles(folderPath);
            });
        }

        public async Task<Dictionary<string, string>> GetFileContentAsync(string filePath)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
            string line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                string[] props = line.Split('=');
                content[props[0]] = props[1];
            }

            return content;
        }
    }
}
