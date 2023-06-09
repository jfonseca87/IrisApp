using IrisApp.Services.Interfaces;
using IrisAppRepository.Interfaces;
using Microsoft.Extensions.Logging;

namespace IrisApp.Services.Implementations
{
    public class ProcessFileService : IProcessFileService
    {
        private readonly IFileService _fileService;
        private readonly IClientInformationRepository _clientInformationRepository;
        private readonly ISecurityService _securityService;
        private readonly ILogger<ProcessFileService> _logger;

        public ProcessFileService(
            IFileService fileService, 
            IClientInformationRepository clientInformationRepository,
            ISecurityService securityService,
            ILogger<ProcessFileService> logger)
        {
            _fileService = fileService;
            _clientInformationRepository = clientInformationRepository;
            _securityService = securityService;
            _logger = logger;
        }

        public async Task<bool> ProcessClientInformation()
        {
            string[] files = await _fileService.GetFiles();

            foreach (string file in files)
            {
                var fileContent = await _fileService.GetFileContentAsync(file);
                string fileHash = fileContent["hash"];
                string generatedHash = _securityService.GenerateMd5Hash(fileContent);

                if (fileHash != generatedHash.ToLower())
                {
                    _logger.LogInformation($"Generated hash it's different from saved hash in file: {file}");
                    continue;
                }

                await _clientInformationRepository.SaveClientInformationData(fileContent);
                await _fileService.DeleteFile(file);
            }

            return true;
        }
    }
}
