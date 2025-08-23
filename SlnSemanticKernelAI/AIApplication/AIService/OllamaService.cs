using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.Ollama;

namespace AIServiceCore.AIService
{
    public class OllamaService : AIService
    {
        public override IChatCompletion RunChatCompletionService(IAIConnectorConfiguration iAIConnectorConfiguration, string yamContent, List<object> plugins)
        {
            IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
           return RunChatService(iAIConnectorConfiguration, chatCompletionConnector, yamContent, plugins);
        }
    }
}