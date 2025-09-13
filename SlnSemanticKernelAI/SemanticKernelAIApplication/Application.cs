using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using SemanticKernelAIApplication.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.AIServiceCore.ChatCompletionService;
using SemanticKernelCore.AIServiceCore.EmbeddingService;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.AzureAIInference;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;
using SemanticKernelCore.Connectors.Ollama;
using SemanticKernelCore.Connectors.VectorStore;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.VectorStoreCore;
using SemanticKernelCore.VectorStoreCore.DataLoader;
using SemanticKernelCore.VectorStoreCore.QdrantVector;
using static OllamaSharp.OllamaApiClient;

namespace SemanticKernelAIApplication
{
    public static class Application
    {
        public static IChatCompletion RunApplication(ConnectorType connectorType, string ymlContent, List<Object> Plugins, bool AddEmbeddingGenerator = false)
        {
            IKernelService kernelService = new KernelService();
            AIChatCompletionService aIChatCompletionService = new AIChatCompletionService(kernelService);

            IAIConnectorConfiguration connectorConfig = GetChatCompletionConnectorConnectorConfig(connectorType);  
            if (connectorConfig == null)
            {
                throw new InvalidOperationException("Failed to get connector configuration.");
            }

            if (connectorType == ConnectorType.AzureAiInference)
                aIChatCompletionService.AddChatCompletionService(new AzureAIInferenceConnector(), connectorConfig);   
            else if (connectorType == ConnectorType.HuggingFace)
                aIChatCompletionService.AddChatCompletionService(new HuggingFaceConnector(), connectorConfig);   
            else if (connectorType == ConnectorType.Ollama) 
                aIChatCompletionService.AddChatCompletionService(new OllamaConnector(), connectorConfig);  

            IAIConnectorConfiguration embeddingConfiguration = null;
            if (AddEmbeddingGenerator) // modify code for other embedding connectors as needed  
            {
                embeddingConfiguration = GetEmbeddingConnectorConfiguration(ConnectorType.Ollama);
                aIChatCompletionService.AddEmbeddingGenerator(new OllamaConnector(), embeddingConfiguration);
            }
            
            if (Plugins != null && Plugins.Any())
            {
                aIChatCompletionService.AddPluginObject(Plugins);
            }
            ChatCompletionAgent chatCompletionAgent =  aIChatCompletionService.CreateAgent(ymlContent);

            return aIChatCompletionService.RunAgent(chatCompletionAgent);
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

        public static void  CreateAnStoreEmbedding()
        {
            IKernelService kernelService = new KernelService();
            kernelService.CreatekernelBuilder();

            AIEmbeddingService embeddingService = new OllamaEmbeddingService();
            embeddingService.KernelService = kernelService;

            IEmbeddingGeneratorConnector embeddingGeneratorConnector = new OllamaConnector();
            embeddingGeneratorConnector.KernelService = kernelService;

            IAIConnectorConfiguration embeddingConfiguration = GetEmbeddingConnectorConfiguration(ConnectorType.Ollama);
            embeddingGeneratorConnector.AddEmbeddingGenerator(embeddingConfiguration);
            kernelService.BuildKernel();

            string collectionName = "IndianBailJudgments";  
            IAIConnectorConfiguration iAIConnectorConfiguration = GetVectorStoreConnectorConfiguration(VectorStoreType.Qdrant, collectionName);
            QdrantVectorStorConfiguration qdrantVectorStorConfiguration = iAIConnectorConfiguration as QdrantVectorStorConfiguration;
            IDataLoader pDFLoader = new PDFLoader();
            IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator = kernelService.Kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
            IVectorStoreConnector vectorStoreConnector = new QdrantVectorStoreConnector();
            
             embeddingService.AddEmbeddingGenerator(embeddingGeneratorConnector, embeddingConfiguration);
            var vectorstoreconnector = (QdrantVectorStore) vectorStoreConnector.AddVectorStore(iAIConnectorConfiguration, _embeddingGenerator);
            IVectorStoreService vectorStoreService = new QdrantVectorStoreService(vectorstoreconnector, collectionName);

            // Use the actual collection name variable, not the string "collectionName"

            if (qdrantVectorStorConfiguration == null)
                throw new InvalidOperationException("No QdrantVectorStorConfiguration found");

            if (qdrantVectorStorConfiguration.Collection == null)
                throw new InvalidOperationException("No collections found in QdrantVectorStorConfiguration.");

            if (!qdrantVectorStorConfiguration.Collection.TryGetValue(collectionName, out var collectionConfig) || collectionConfig == null)
                throw new InvalidOperationException($"Collection '{collectionName}' not found in QdrantVectorStorConfiguration.");

            if (string.IsNullOrWhiteSpace(collectionConfig.FolderPath) && collectionConfig.Filepath == null)
                throw new InvalidOperationException($"'folderPath' is missing or empty for collection '{collectionName}'.");

            if (collectionConfig.BatchSize <= 0)
                throw new InvalidOperationException($"'batchSize' must be greater than zero for collection '{collectionName}'.");

            if (collectionConfig.BatchDivision <= 0)
                throw new InvalidOperationException($"'batchDivision' must be greater than zero for collection '{collectionName}'.");

            var folderPath = collectionConfig.FolderPath;
            string[] filePaths = collectionConfig.Filepath;
            int batchSize = collectionConfig.BatchSize;
            int batchDivision = collectionConfig.BatchDivision;

            if (Directory.Exists(folderPath))
            {
                var folderFiles = Directory.GetFiles(folderPath);
                if (filePaths == null || filePaths.Length == 0)
                {
                    filePaths = new string[0];
                }
                filePaths = filePaths.Concat(folderFiles).ToArray();
            }

            embeddingService.UploadEmbedding(pDFLoader, vectorStoreService, filePaths, batchDivision, batchSize);
        }
    }
}