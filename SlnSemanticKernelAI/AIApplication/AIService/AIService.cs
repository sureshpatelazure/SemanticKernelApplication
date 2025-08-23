using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.Plugin;

namespace AIServiceCore.AIService
{
    public abstract class AIService
    {
        public IKernelService KernelService { get; set; }

        private void AddChatCompletionService(IChatCompletionConnector chatCompletionConnector, IAIConnectorConfiguration connectorConfiguration)
        {
            if (KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            chatCompletionConnector.KernelService = KernelService;

            chatCompletionConnector.AddChatCompletion(connectorConfiguration);
        }

        private ChatCompletionAgent CreateAgent(string yamContent)
        {
            if (KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            if (string.IsNullOrWhiteSpace(yamContent))
            {
                throw new ArgumentException("YAML content cannot be null or empty.", nameof(yamContent));
            }

            IAIAgent agent = new AIAgent(KernelService);
            return agent.CreateAIAgent(yamContent);
        }
        private IChatCompletion GetChatCompletion(ChatCompletionAgent chatCompletionAgent)
        {
            return new ChatCompletion(chatCompletionAgent);
        }

        private void AddPluginObject(List<object> plugins)
        {
            if (plugins == null || !plugins.Any())
            {
                return;
            }
            if (KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before adding plugins.");
            }

            IPLuginObject pluginObject = new PLuginObject
            {
                KernelService = KernelService
            };
            pluginObject.AddPluginObject(plugins);
        }
        public abstract IChatCompletion RunChatCompletionService(IAIConnectorConfiguration iAIConnectorConfiguration,string agentPromptFilePath, List<object> plugins);

        protected IChatCompletion RunChatService(IAIConnectorConfiguration iAIConnectorConfiguration, IChatCompletionConnector chatCompletionConnector,string yamContent, List<object> plugins)
        {
            KernelService.CreatekernelBuilder();

            AddChatCompletionService(chatCompletionConnector, iAIConnectorConfiguration);
            AddPluginObject(plugins);
            KernelService.BuildKernel();

            ChatCompletionAgent chatCompletionAgent = CreateAgent(yamContent);

            return GetChatCompletion(chatCompletionAgent);
        }
    }
}