using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.Ollama;

namespace SemanticKernelCore.AIServiceCore.ChatCompletionService
{
    public class OllamaChatCompletionService : AIChatCompletionService
    {
        public override IChatCompletion RunChatCompletionService(
            IAIConnectorConfiguration iAIConnectorConfiguration,
            IAIConnectorConfiguration embeddingConfiguration,
            string yamContent, List<object> plugins)
        {
            IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
            IEmbeddingGeneratorConnector embeddingGeneratorConnector = new OllamaConnector();   
            return RunChatService(iAIConnectorConfiguration, chatCompletionConnector, embeddingGeneratorConnector, embeddingConfiguration,yamContent, plugins);
        }
    }
}