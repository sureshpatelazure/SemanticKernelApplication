using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using SemanticKernelCore.Connectors.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors
{
    public interface IVectorStoreConnector
    {
        public QdrantVectorStore AddVectorStore<T>(T connectorConfiguration, IEmbeddingGenerator embeddingGenerator) where T : IAIConnectorConfiguration;    
    }
}
