using AIServiceCore.Configuration;
using Microsoft.Extensions.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.Ollama;

namespace AIServiceCore.AIService
{
    public class OllamaService : AIService
    {
        public override IChatCompletion RunChatCompletionService(string yamContent, List<object> plugins)
        {

            IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
            OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

            AppConfiguration  appConfiguration = new AppConfiguration();

            var config = appConfiguration.GetConnectorConfiguration("ollama");

            ollamConfig.ModelId = config.GetValue<string>("ModelId"); 
            ollamConfig.Uri = config.GetValue<string>("Uri");

           return RunChatService( ollamConfig, chatCompletionConnector, yamContent, plugins);
        }
    }
}