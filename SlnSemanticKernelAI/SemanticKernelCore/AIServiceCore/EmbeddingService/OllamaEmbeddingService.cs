using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.AIServiceCore.EmbeddingService
{
    public class OllamaEmbeddingService : AIEmbeddingService
    {
        public override void AddOllamaEmbeddingGenerator(IEmbeddingGenerationConnector embeddingGenerationConnector, IAIConnectorConfiguration connectorConfiguration)
        {
            if (KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            embeddingGenerationConnector.KernelService = KernelService;
            embeddingGenerationConnector.AddEmbeddingGeneration(connectorConfiguration);
        }
    }
}
