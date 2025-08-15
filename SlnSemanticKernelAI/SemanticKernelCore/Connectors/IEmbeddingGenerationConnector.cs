using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors
{
    public interface IEmbeddingGenerationConnector
    {
        public IKernelService KernelService { get; set; }
        void AddEmbeddingGeneration<T>(T connectorConfiguration) where T : IAIConnectorConfiguration;
    }
}
