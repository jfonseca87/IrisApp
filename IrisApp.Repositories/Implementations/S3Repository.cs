using IrisApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisApp.Repositories.Implementations
{
    public class S3Repository : IFileRepository
    {
        public Task<IEnumerable<object>> GetFiles()
        {
            throw new NotImplementedException();
        }
    }
}
