using AIServiceCore.Configuration;
using Microsoft.Extensions.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.AzureAIInference;
using SemanticKernelCore.Connectors.Configuration;

namespace AIServiceCore.AIService
{
    public  class AzureAiInferenceService : AIService
    {
        public override IChatCompletion RunChatCompletionService(string yamContent, List<object> plugins)
        {

            IChatCompletionConnector chatCompletionConnector = new AzureAIInferenceConnector();
            AzureAIInferenceConnectorChatCompletionConfig azureAiInferenceConfig = new AzureAIInferenceConnectorChatCompletionConfig();

            AppConfiguration appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("azureaiinference");

            azureAiInferenceConfig.ModelId = config.GetValue<string>("ModelId");
            azureAiInferenceConfig.Uri = config.GetValue<string>("Uri");
            azureAiInferenceConfig.ApiKey = config.GetValue<string>("ApiKey");

            return RunChatService(azureAiInferenceConfig, chatCompletionConnector, yamContent, plugins);
        }
    }
}