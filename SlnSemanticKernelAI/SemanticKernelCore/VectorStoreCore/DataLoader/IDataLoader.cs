using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.VectorStoreCore.DataLoader
{
    public interface IDataLoader
    {
        public Task<List<DataContent>> LoadData(string filePath, int blockDivision, int batchSize);
    }
}