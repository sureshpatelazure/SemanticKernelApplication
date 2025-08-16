using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;

namespace AIApplication.AIService
{
    public abstract class AIChatCompletionService
    {
        public abstract IChatCompletion RunChatCompletionService(string agentPromptFilePath);

        protected IChatCompletion RunChatCompletion(IChatCompletionConnector chatCompletionConnector,
            IAIConnectorConfiguration connectorConfiguration, string yamContent)
        {
            IKernelService kernelService = new KernelService();
            kernelService.CreatekernelBuilder();

            chatCompletionConnector.KernelService = kernelService;

            chatCompletionConnector.AddChatCompletion(connectorConfiguration);

            kernelService.BuildKernel();

            IAIAgent agent = new AIAgent(kernelService);
            ChatCompletionAgent chatCompletionAgent = agent.CreateAIAgent(yamContent);

            IChatCompletion chatCompletion = new ChatCompletion(chatCompletionAgent);

            return chatCompletion;
        }
    }
}
