using IrisApp.Services.Implementations;
using IrisApp.Services.Interfaces;
using IrisAppRepository.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace IrisApp.Tests.Services
{
    public class ProcessFileServiceTest: BaseClass
    {
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<IClientInformationRepository> _clientInformationRepisitoryMock;
        private readonly Mock<ISecurityService> _securityServiceMock;
        private readonly Mock<ILogger<ProcessFileService>> _loggerMock;
        private readonly ProcessFileService _processFileService;
        private Dictionary<string, string> _fakeContent = new Dictionary<string, string>();

        public ProcessFileServiceTest()
        {
            _fileServiceMock = new Mock<IFileService>();
            _clientInformationRepisitoryMock = new Mock<IClientInformationRepository>();
            _securityServiceMock = new Mock<ISecurityService>();
            _loggerMock = new Mock<ILogger<ProcessFileService>>();

            _processFileService = new ProcessFileService
                (
                    _fileServiceMock.Object,
                    _clientInformationRepisitoryMock.Object,
                    _securityServiceMock.Object,
                    _loggerMock.Object
                );

            SetupFakeData();
        }

        [Fact]
        public async Task ProcessFileIsSuccessfull()
        {
            _fileServiceMock.Setup(x => x.GetFiles())
                .ReturnsAsync(new string[] { "file.txt" });

            _fileServiceMock.Setup(x => x.GetFileContentAsync(It.IsAny<string>()))
                .ReturnsAsync(_fakeContent);

            _securityServiceMock.Setup(x => x.GenerateMd5Hash(It.IsAny<Dictionary<string, string>>()))
                .Returns("2f941516446dce09bc2841da60bf811f");

            _clientInformationRepisitoryMock.Setup(x => x.SaveClientInformationData(It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(true);

            _fileServiceMock.Setup(x => x.DeleteFile(It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _processFileService.ProcessClientInformation();
            
            Assert.True(result);

            _clientInformationRepisitoryMock
                .Verify(x => x.SaveClientInformationData(It.IsAny<Dictionary<string, string>>()), Times.Once);
            _fileServiceMock
                .Verify(x => x.DeleteFile(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ProcessFileDontProcessFileBecauseHashIsNotTheSame()
        {
            _fileServiceMock.Setup(x => x.GetFiles())
                .ReturnsAsync(new string[] { "file.txt" });

            _fileServiceMock.Setup(x => x.GetFileContentAsync(It.IsAny<string>()))
                .ReturnsAsync(_fakeContent);

            _securityServiceMock.Setup(x => x.GenerateMd5Hash(It.IsAny<Dictionary<string, string>>()))
                .Returns("2f941516446dce09bc2841da60bf81zz");

            _clientInformationRepisitoryMock.Setup(x => x.SaveClientInformationData(It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(true);

            _fileServiceMock.Setup(x => x.DeleteFile(It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _processFileService.ProcessClientInformation();

            Assert.True(result);

            _clientInformationRepisitoryMock
                .Verify(x => x.SaveClientInformationData(It.IsAny<Dictionary<string, string>>()), Times.Never);
            _fileServiceMock
                .Verify(x => x.DeleteFile(It.IsAny<string>()), Times.Never);
        }

        private void SetupFakeData()
        {
            _fakeContent = new Dictionary<string, string>();
            _fakeContent.Add("totalContactoClientes", "250");
            _fakeContent.Add("motivoReclamo", "25");
            _fakeContent.Add("motivoGarantia", "10");
            _fakeContent.Add("motivoDuda", "100");
            _fakeContent.Add("motivoCompra", "100");
            _fakeContent.Add("motivoFelicitaciones", "7");
            _fakeContent.Add("motivoCambio", "8");
            _fakeContent.Add("hash", "2f941516446dce09bc2841da60bf811f");
        }
    }
}
