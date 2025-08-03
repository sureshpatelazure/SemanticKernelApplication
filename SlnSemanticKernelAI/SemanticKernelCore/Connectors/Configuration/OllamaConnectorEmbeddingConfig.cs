using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors.Configuration
{
    public class OllamaConnectorEmbeddingConfig : IAIConnectorConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
    }
}
