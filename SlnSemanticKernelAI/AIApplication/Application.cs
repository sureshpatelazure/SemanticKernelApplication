using AIServiceCore.AIService;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.KernelCore;

namespace AIServiceCore
{
    public static class Application
    {
        public static IChatCompletion RunApplication(ConnectorType connectorType, string ymlContent, List<Object> Plugins)
        {
            AIService.AIService aIChatCompletionService;

            if (connectorType == ConnectorType.AzureAiInference)
            {
                aIChatCompletionService = new AzureAiInferenceService();
            }
            else if (connectorType == ConnectorType.Ollama)
            {
                aIChatCompletionService = new OllamaService();
            }
            else if (connectorType == ConnectorType.HuggingFace)
            {
                aIChatCompletionService = new HuggingFaceService();
            }
            else
            {
                throw new ArgumentException("Unsupported connector type.", nameof(connectorType));
            }

            IKernelService kernelService = new KernelService();

            aIChatCompletionService.KernelService = kernelService;

            return aIChatCompletionService.RunChatCompletionService(ymlContent, Plugins);
        }
    }
}