namespace SemanticKernelCore.VectorStoreCore
{
    public interface IVectorRecord
    {
        ulong Key { get; set; }
        string Text { get; set; }
    }
}
