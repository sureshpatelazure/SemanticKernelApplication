namespace SemanticKernelCore.VectorStoreCore.DataLoader
{
    public interface IDataLoader
    {
        public Task<List<DataContent>> LoadData(string filePath, int blockDivision, int batchSize);
    }
}