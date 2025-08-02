using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors.Configuration
{
    public class OllamaConnectorConfiguration : IAIConnectorConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
        public bool useEmbeddingModel { get; set; } = false;
        public string EmbeddingModelId { get; set; }
        public string EmbeddingUrl { get; set; }
    }
}
