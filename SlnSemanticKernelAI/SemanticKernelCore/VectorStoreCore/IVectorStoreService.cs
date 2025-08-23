namespace SemanticKernelCore.VectorStoreCore
{
    public interface IVectorStoreService
    {
        public Task UpSert(IEnumerable<DataLoader.DataContent> dataContents);
    }
}
