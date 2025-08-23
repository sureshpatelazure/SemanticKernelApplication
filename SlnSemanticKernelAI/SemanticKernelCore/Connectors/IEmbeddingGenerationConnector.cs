using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;

namespace SemanticKernelCore.Connectors
{
    public interface IEmbeddingGeneratorConnector
    {
        public IKernelService KernelService { get; set; }
        void AddEmbeddingGenerator<T>(T connectorConfiguration) where T : IAIConnectorConfiguration;
    }
}
