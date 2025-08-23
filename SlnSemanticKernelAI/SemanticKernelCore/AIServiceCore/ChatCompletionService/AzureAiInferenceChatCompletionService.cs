using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.AzureAIInference;
using SemanticKernelCore.Connectors.Configuration;

namespace SemanticKernelCore.AIServiceCore.ChatCompletionService
{
    public  class AzureAiInferenceChatCompletionService : AIChatCompletionService
    {
        public override IChatCompletion RunChatCompletionService(IAIConnectorConfiguration iAIConnectorConfiguration,string yamContent, List<object> plugins)
        {
            IChatCompletionConnector chatCompletionConnector = new AzureAIInferenceConnector();
            return RunChatService(iAIConnectorConfiguration, chatCompletionConnector, yamContent, plugins);
        }
    }
}