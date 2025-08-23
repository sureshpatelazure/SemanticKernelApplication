using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors.Configuration
{
    public class QdrantVectorStorConfiguration : IAIConnectorConfiguration
    {
        public string Uri { get; set; }
        public string ApiKey { get; set; }
        public string CollectionName { get; set; }
    }
}
