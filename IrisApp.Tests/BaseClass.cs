using IrisAppRepository.Domain;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Language.Flow;

namespace IrisApp.Tests
{
    public abstract class BaseClass
    {
        private readonly IReturnsResult<IOptions<AwsSettings>> _settingsMock;

        protected BaseClass()
        {
            _settingsMock = new Mock<IOptions<AwsSettings>>()
                .Setup(x => x.Value)
                .Returns(new AwsSettings 
                {
                    UserId = "UserId",
                    SecretAccessKey = "SecretAccessKey",
                    DynamoTable = "DynamoTable",
                    S3BucketName = "BucketName"
                });
        }
    }
}
