using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;
using SemanticKernelCore.Connectors.Configuration;

namespace SemanticKernelCore.VectorStoreCore.QdrantVector
{
    public class QdrantVectorStoreService : IVectorStoreService
    {
        private readonly QdrantVectorStore? _vectorStore;
        private readonly QdrantCollection<ulong, DataLoader.DataContent>? _collection;
        public QdrantVectorStoreService(QdrantVectorStore qdrantVectorStore, string collectionName)
        {
            _collection = qdrantVectorStore.GetCollection<ulong, DataLoader.DataContent>(collectionName);

            _collection.EnsureCollectionExistsAsync().GetAwaiter().GetResult();
        }

        public async Task UpSert(IEnumerable<DataLoader.DataContent> dataContents)
        {
            var data = dataContents.Select(snippet => new DataLoader.DataContent
            {
                Key = snippet.Key,
                Text = snippet.Text, // You can modify the text as needed
            });

            await _collection.UpsertAsync(data);
        }
    }
}
