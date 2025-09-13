using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;
using SemanticKernelCore.Connectors.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors.VectorStore
{
    public class QdrantVectorStoreConnector : IVectorStoreConnector
    {
        public QdrantVectorStore AddVectorStore<T>(T connectorConfiguration, IEmbeddingGenerator embeddingGenerator  ) where T : IAIConnectorConfiguration
        {
            QdrantVectorStorConfiguration qdrantVectorStorConfiguration = connectorConfiguration as QdrantVectorStorConfiguration;
            QdrantClient qdrantClient = new QdrantClient(new Uri(qdrantVectorStorConfiguration.Uri), qdrantVectorStorConfiguration.ApiKey);

             return new QdrantVectorStore(qdrantClient, true,
               new QdrantVectorStoreOptions
               {
                   EmbeddingGenerator = embeddingGenerator,
               });
        }
    }
}
