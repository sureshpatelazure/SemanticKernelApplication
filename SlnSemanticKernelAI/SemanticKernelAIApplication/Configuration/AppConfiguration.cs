using Microsoft.Extensions.Configuration;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;

namespace SemanticKernelAIApplication.Configuration
{
    public class AppConfiguration
    {
        IConfiguration _connectorConfiguration;
        public AppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"Configuration\\connectorsettings.json", optional: false, reloadOnChange: true);

            _connectorConfiguration = builder.Build();
        }
        public IConfigurationSection GetChatCompletionConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("chatcompletion").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for chatcompletion:{ConnectorName}");

            return section;

        }

        public IConfigurationSection GetEmbeddingConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("embedding").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for embedding:{ConnectorName}");

            return section;

        }

        public IConfigurationSection GetVectorStoreConnectorConfiguration(string ConnectorName)
        {
            var section = _connectorConfiguration.GetSection("vectorstore").GetSection(ConnectorName);
            if (section == null)
                throw new InvalidOperationException($"Missing configuration for vectorstore:{ConnectorName}");

            return section;

        }
    }

    public static class ConnectorConfiguration
    {
        public static IAIConnectorConfiguration GetChatCompletionConnectorConnectorConfig(ConnectorType connectorType)
        {
            AppConfiguration appConfiguration = new AppConfiguration();

            if (connectorType == ConnectorType.AzureAiInference)
            {
                var config = appConfiguration.GetChatCompletionConnectorConfiguration("azureaiinference");

                AzureAIInferenceConnectorChatCompletionConfig azureAiInferenceConfig = new AzureAIInferenceConnectorChatCompletionConfig();
                azureAiInferenceConfig.ModelId = config.GetValue<string>("ModelId");
                azureAiInferenceConfig.Uri = config.GetValue<string>("Uri");
                azureAiInferenceConfig.ApiKey = config.GetValue<string>("ApiKey");
                return azureAiInferenceConfig;
            }
            else if (connectorType == ConnectorType.Ollama)
            {
                OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

                var config = appConfiguration.GetChatCompletionConnectorConfiguration("ollama");
                ollamConfig.ModelId = config.GetValue<string>("ModelId");
                ollamConfig.Uri = config.GetValue<string>("Uri");

                return ollamConfig;
            }
            else if (connectorType == ConnectorType.HuggingFace)
            {
                HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();

                var config = appConfiguration.GetChatCompletionConnectorConfiguration("huggingface");

                hfConfig.ModelId = config.GetValue<string>("ModelId");
                hfConfig.Uri = config.GetValue<string>("Uri");
                hfConfig.ApiKey = config.GetValue<string>("ApiKey");

                return hfConfig;
            }
            return null;
        }

        public static IAIConnectorConfiguration GetEmbeddingConnectorConfiguration(ConnectorType connectorType)
        {
            AppConfiguration appConfiguration = new AppConfiguration();

            if (connectorType == ConnectorType.Ollama)
            {
                OllamaConnectorEmbeddingConfig ollamConfig = new OllamaConnectorEmbeddingConfig();

                var config = appConfiguration.GetEmbeddingConnectorConfiguration("ollama");
                ollamConfig.ModelId = config.GetValue<string>("ModelId");
                ollamConfig.Uri = config.GetValue<string>("Uri");

                return ollamConfig;
            }
            return null;

        }

        public static IAIConnectorConfiguration GetVectorStoreConnectorConfiguration(VectorStoreType vectorStoreType, string collectionName)
        {
            AppConfiguration appConfiguration = new AppConfiguration();

            if (vectorStoreType == VectorStoreType.Qdrant)
            {
                QdrantVectorStorConfiguration vcConfig = new QdrantVectorStorConfiguration();

                var config = appConfiguration.GetVectorStoreConnectorConfiguration("qdrant");
                vcConfig.Uri = config.GetValue<string>("uri");
                vcConfig.ApiKey = config.GetValue<string>("apikey");
                var collectionsSection = config.GetSection("collectionname");
                var collections = new Dictionary<string, QdrantCollectionConfig>();

                var collection = collectionsSection.GetSection(collectionName);
                if (!collection.Exists())
                    throw new InvalidOperationException($"Collection '{collectionName}' not found in configuration.");

                var collectionConfig = new QdrantCollectionConfig
                {
                    FolderPath = collection.GetValue<string>("folderPath"),
                    Filepath = collection.GetSection("filepath").Get<string[]>(),
                    BatchSize = collection.GetValue<int>("batchSize"),
                    BatchDivision = collection.GetValue<int>("batchDivision")
                };
                collections[collectionName] = collectionConfig;

                vcConfig.Collection = collections;

                return vcConfig;
            }
            return null;

        }

    }
}