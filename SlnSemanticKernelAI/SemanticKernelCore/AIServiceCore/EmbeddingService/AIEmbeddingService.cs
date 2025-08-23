using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.AIServiceCore.EmbeddingService
{
    public abstract class AIEmbeddingService
    {
        public IKernelService KernelService { get; set; }
        public abstract void AddOllamaEmbeddingGenerator(IEmbeddingGenerationConnector  embeddingGenerationConnector ,  IAIConnectorConfiguration connectorConfiguration);
    }
}
