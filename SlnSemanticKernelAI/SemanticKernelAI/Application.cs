using AIServiceCore.AIService;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelAIApplication.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;

namespace SemanticKernelAIApplication
{
    public static class Application
    {
        public static IChatCompletion RunApplication(ConnectorType connectorType, string ymlContent, List<Object> Plugins)
        {
            AIService aIChatCompletionService;

            if (connectorType == ConnectorType.AzureAiInference)
            {
                aIChatCompletionService = new AzureAiInferenceService();
            }
            else if (connectorType == ConnectorType.Ollama)
            {
                aIChatCompletionService = new OllamaService();
            }
            else if (connectorType == ConnectorType.HuggingFace)
            {
                aIChatCompletionService = new HuggingFaceService();
            }
            else
            {
                throw new ArgumentException("Unsupported connector type.", nameof(connectorType));
            }

            IAIConnectorConfiguration connectorConfig = GetConnectorConfig(connectorType);  
            if (connectorConfig == null)
            {
                throw new InvalidOperationException("Failed to get connector configuration.");
            }

            IKernelService kernelService = new KernelService();

            aIChatCompletionService.KernelService = kernelService;

            return aIChatCompletionService.RunChatCompletionService(connectorConfig, ymlContent, Plugins);
        }

        public static IAIConnectorConfiguration GetConnectorConfig(ConnectorType connectorType)
        {
            AppConfiguration appConfiguration = new AppConfiguration();

            if (connectorType == ConnectorType.AzureAiInference)
            {
                var config = appConfiguration.GetConnectorConfiguration("azureaiinference");

                AzureAIInferenceConnectorChatCompletionConfig azureAiInferenceConfig = new AzureAIInferenceConnectorChatCompletionConfig(); 
                azureAiInferenceConfig.ModelId = config.GetValue<string>("ModelId");
                azureAiInferenceConfig.Uri = config.GetValue<string>("Uri");
                azureAiInferenceConfig.ApiKey = config.GetValue<string>("ApiKey");
                return azureAiInferenceConfig;
            }
            else if (connectorType == ConnectorType.Ollama)
            {
                OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

                var config = appConfiguration.GetConnectorConfiguration("ollama");
                ollamConfig.ModelId = config.GetValue<string>("ModelId");
                ollamConfig.Uri = config.GetValue<string>("Uri");

                return ollamConfig;
            }
            else if (connectorType == ConnectorType.HuggingFace)
            {
                HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();

                var config = appConfiguration.GetConnectorConfiguration("huggingface");

                hfConfig.ModelId = config.GetValue<string>("ModelId");
                hfConfig.Uri = config.GetValue<string>("Uri");
                hfConfig.ApiKey = config.GetValue<string>("ApiKey");

                return hfConfig;
            }

            return null;
        }

    }
}