using Amazon.S3;
using IrisApp.Services.Interfaces;
using IrisAppRepository.Domain;
using Microsoft.Extensions.Options;
using System.Text;

namespace IrisApp.Services.Implementations
{
    public class S3FileService : IFileService
    {
        private readonly AmazonS3Client _s3Client;
        private readonly IOptions<AwsSettings> _settings;

        public S3FileService(IOptions<AwsSettings> settings)
        {
            _s3Client = new AmazonS3Client(settings.Value.UserId, settings.Value.SecretAccessKey);
            _settings = settings;
        }

        public async Task<bool> DeleteFile(string filePath)
        {
            await _s3Client.DeleteObjectAsync(_settings.Value.S3BucketName, filePath);
            return true;
        }

        public async Task<string[]> GetFiles()
        {
            var files = await _s3Client.ListObjectsAsync(_settings.Value.S3BucketName);

            return files != null
                ? files.S3Objects.Select(o => o.Key).ToArray()
                : Array.Empty<string>();
        }

        public async Task<Dictionary<string, string>> GetFileContentAsync(string filePath)
        {
            Dictionary<string, string> content = new Dictionary<string, string>();
            var file = await _s3Client.GetObjectAsync(_settings.Value.S3BucketName, filePath);
            var fileBytes = new byte[file.ResponseStream.Length];

            Stream memoryStream = new MemoryStream(fileBytes);
            using var streamReader = new StreamReader(memoryStream, Encoding.UTF8);
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
