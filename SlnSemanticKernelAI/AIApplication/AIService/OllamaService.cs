using AIApplication.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.Ollama;
using SemanticKernelCore.KernelCore;

namespace AIApplication.AIService
{
    public class OllamaService : AIService
    {
        public override IChatCompletion RunChatCompletionService(string yamContent)
        {
            KernelService.CreatekernelBuilder();

            IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
            OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

            AppConfiguration  appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("ollama");

            ollamConfig.ModelId = config.GetValue<string>("ModelId"); 
            ollamConfig.Uri = config.GetValue<string>("Uri");

            AddChatCompletionService(chatCompletionConnector, ollamConfig);
            KernelService.BuildKernel();    

            ChatCompletionAgent chatCompletionAgent = CreateAgent(yamContent);

           return GetChatCompletion(chatCompletionAgent);

        }
    }
}
