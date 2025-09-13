using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;

namespace SemanticKernelCore.AIServiceCore.ChatCompletionService
{
    public class HuggingFaceChatCompletionService : AIChatCompletionService
    {
        public override IChatCompletion RunChatCompletionService(
            IAIConnectorConfiguration iAIConnectorConfiguration,
            IAIConnectorConfiguration embeddingConfiguration,
            string yamContent, List<object> plugins    )
        {
            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
            IEmbeddingGeneratorConnector embeddingGeneratorConnector = null;
            return RunChatService(iAIConnectorConfiguration, chatCompletionConnector, embeddingGeneratorConnector, embeddingConfiguration, yamContent, plugins);

        }
    }
}