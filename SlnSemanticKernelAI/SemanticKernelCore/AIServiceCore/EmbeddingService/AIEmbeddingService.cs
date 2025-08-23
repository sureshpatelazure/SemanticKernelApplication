using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.VectorStoreCore;
using SemanticKernelCore.VectorStoreCore.DataLoader;

namespace SemanticKernelCore.AIServiceCore.EmbeddingService
{
    public abstract class AIEmbeddingService
    {
        public IKernelService KernelService { get; set; }
        public abstract void AddEmbeddingGenerator(IEmbeddingGeneratorConnector embeddingGeneratorConnector ,  IAIConnectorConfiguration connectorConfiguration);

        public void UploadEmbedding(IDataLoader dataLoader, IVectorStoreService vectorStoreService,
            string[] filePaths, int blockDivistion, int batchSize)
        {
            int counter = 0;
            foreach (var path in filePaths)
            {
                var dataContent = dataLoader.LoadData(path, blockDivistion, batchSize).GetAwaiter().GetResult();
                vectorStoreService.UpSert(dataContent).GetAwaiter().GetResult();
            }
        }
    }
}
