using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;

namespace SemanticKernelCore.Connectors
{
    public interface IEmbeddingGenerationConnector
    {
        public IKernelService KernelService { get; set; }
        void AddEmbeddingGeneration<T>(T connectorConfiguration) where T : IAIConnectorConfiguration;
    }
}
