using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;

namespace SemanticKernelCore.AIServiceCore.ChatCompletionService
{
    public class HuggingFaceChatCompletionService : AIChatCompletionService
    {
        public override IChatCompletion RunChatCompletionService(IAIConnectorConfiguration iAIConnectorConfiguration,string yamContent, List<object> plugins    )
        {
            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
            return RunChatService(iAIConnectorConfiguration, chatCompletionConnector, yamContent, plugins);

        }
    }
}