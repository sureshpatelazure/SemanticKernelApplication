using AIApplication.Configuration;
using Microsoft.Extensions.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.Ollama;

namespace AIApplication.AIService
{
    public class OllamaChatCompletionService : AIChatCompletionService
    {
        public override IChatCompletion RunChatCompletionService(string agentPromptFilePath)
        {
            IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
            OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

            AppConfiguration  appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("ollama");

            ollamConfig.ModelId = config.GetValue<string>("ModelId"); 
            ollamConfig.Uri = config.GetValue<string>("Uri");

            string yamContent = File.ReadAllText(agentPromptFilePath);

            return RunChatCompletion(chatCompletionConnector, ollamConfig, yamContent);
        }
    }
}
