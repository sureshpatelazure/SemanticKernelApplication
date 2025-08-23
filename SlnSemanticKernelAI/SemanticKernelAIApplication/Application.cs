using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using SemanticKernelAIApplication.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.AIServiceCore.ChatCompletionService;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.VectorStoreCore;
using SemanticKernelCore.VectorStoreCore.DataLoader;
using SemanticKernelCore.VectorStoreCore.QdrantVector;

namespace SemanticKernelAIApplication
{
    public static class Application
    {
        public static IChatCompletion RunApplication(ConnectorType connectorType, string ymlContent, List<Object> Plugins)
        {
            AIChatCompletionService aIChatCompletionService;

            if (connectorType == ConnectorType.AzureAiInference)
            {
                aIChatCompletionService = new AzureAiInferenceChatCompletionService();
            }
            else if (connectorType == ConnectorType.Ollama)
            {
                aIChatCompletionService = new OllamaChatCompletionService();
            }
            else if (connectorType == ConnectorType.HuggingFace)
            {
                aIChatCompletionService = new HuggingFaceChatCompletionService();
            }
            else
            {
                throw new ArgumentException("Unsupported connector type.", nameof(connectorType));
            }

            IAIConnectorConfiguration connectorConfig = GetChatCompletionConnectorConnectorConfig(connectorType);  
            if (connectorConfig == null)
            {
                throw new InvalidOperationException("Failed to get connector configuration.");
            }

            IKernelService kernelService = new KernelService();

            aIChatCompletionService.KernelService = kernelService;

            return aIChatCompletionService.RunChatCompletionService(connectorConfig, ymlContent, Plugins);
        }

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

        public static IAIConnectorConfiguration GetVectorStoreConnectorConfiguration(ConnectorType connectorType)
        {
            AppConfiguration appConfiguration = new AppConfiguration();

            if (connectorType == ConnectorType.VectorStore)
            {
                QdrantVectorStorConfiguration vcConfig = new QdrantVectorStorConfiguration();

                var config = appConfiguration.GetEmbeddingConnectorConfiguration("qdrant");
                vcConfig.Uri = config.GetValue<string>("uri");
                vcConfig.ApiKey = config.GetValue<string>("apikey");
                vcConfig.CollectionName = config.GetValue<string>("collectionname");

                return vcConfig;
            }
            return null;

        }

        public static void  CreateAnStoreEmbedding()
        {
            IKernelService kernelService = new KernelService();

            IAIConnectorConfiguration iAIConnectorConfiguration = GetVectorStoreConnectorConfiguration(ConnectorType.VectorStore);
            IDataLoader pDFLoader = new PDFLoader();
            IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator = kernelService.Kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
            IVectorStoreService vectorStoreService = new QdrantVectorStoreService(_embeddingGenerator, iAIConnectorConfiguration);


        }
    }
}