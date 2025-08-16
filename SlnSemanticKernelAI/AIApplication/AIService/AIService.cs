using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.Plugin;

namespace AIApplication.AIService
{
    public abstract class AIService
    {
        public IKernelService KernelService { get; set; }

        protected void AddChatCompletionService(IChatCompletionConnector chatCompletionConnector, IAIConnectorConfiguration connectorConfiguration)
        {
            if (KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            chatCompletionConnector.KernelService = KernelService;

            chatCompletionConnector.AddChatCompletion(connectorConfiguration);
        }

        protected ChatCompletionAgent CreateAgent(string yamContent)
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
        protected IChatCompletion GetChatCompletion(ChatCompletionAgent chatCompletionAgent)
        {
            return new ChatCompletion(chatCompletionAgent);
        }

        protected void AddPluginObject( List<object> plugins)
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
        public abstract IChatCompletion RunChatCompletionService(string agentPromptFilePath, List<object> plugins);
    }
}