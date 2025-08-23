using Microsoft.Extensions.Configuration;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;

namespace AIServiceCore.AIService
{
    public class HuggingFaceService : AIService
    {
        public override IChatCompletion RunChatCompletionService(IAIConnectorConfiguration iAIConnectorConfiguration,string yamContent, List<object> plugins    )
        {
            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
            return RunChatService(iAIConnectorConfiguration, chatCompletionConnector, yamContent, plugins);

        }
    }
}