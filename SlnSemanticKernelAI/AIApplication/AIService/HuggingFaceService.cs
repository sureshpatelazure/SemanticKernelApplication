using AIApplication.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;
using SemanticKernelCore.Connectors.Ollama;

namespace AIApplication.AIService
{
    public class HuggingFaceService : AIService
    {
        public override IChatCompletion RunChatCompletionService(string yamContent)
        {
            KernelService.CreatekernelBuilder();

            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
            HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();

            AppConfiguration appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("ollama");

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
