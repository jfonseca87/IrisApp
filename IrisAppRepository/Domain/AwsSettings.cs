using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisAppRepository.Domain
{
    public class AwsSettings
    {
        public string UserId { get; set; }
        public string SecretAccessKey { get; set; }
        public string S3BucketName { get; set; }
        public string DynamoTable { get; set; }
    }
}
