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
        public Dictionary<string, QdrantCollectionConfig> Collection { get; set; }
    }

        public class QdrantCollectionConfig
        {
            public string FolderPath { get; set; }
            public string[] Filepath { get; set; }
            public int BatchSize { get; set; }
            public int BatchDivision { get; set; }
        }
}