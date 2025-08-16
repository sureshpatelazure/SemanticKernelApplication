using AIApplication.Configuration;
using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.AzureAIInference;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;
using SemanticKernelCore.KernelCore;

using Microsoft.Extensions.Configuration;

namespace AIApplication.AIService
{
    public  class AzureAiInferenceService : AIService
    {
        public override IChatCompletion RunChatCompletionService(string yamContent)
        {
            KernelService.CreatekernelBuilder();

            IChatCompletionConnector chatCompletionConnector = new AzureAIInferenceConnector();
            AzureAIInferenceConnectorChatCompletionConfig hfConfig = new AzureAIInferenceConnectorChatCompletionConfig();

            AppConfiguration appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("azureaiinference");

            hfConfig.ModelId = config.GetValue<string>("ModelId");
            hfConfig.Uri = config.GetValue<string>("Uri");
            hfConfig.ApiKey = config.GetValue<string>("ApiKey");

            AddChatCompletionService(chatCompletionConnector, hfConfig);
            KernelService.BuildKernel();

            ChatCompletionAgent chatCompletionAgent = CreateAgent(yamContent);

            return GetChatCompletion(chatCompletionAgent);

        }
    }
}
