using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.Plugin;

namespace SemanticKernelCore.AIServiceCore.ChatCompletionService
{
    public class AIChatCompletionService
    {
        private IKernelService _KernelService { get; set; }

        public AIChatCompletionService(IKernelService KernelService)
        {
            _KernelService = KernelService;
            _KernelService.CreatekernelBuilder();
        }
        public void AddChatCompletionService(IChatCompletionConnector chatCompletionConnector, IAIConnectorConfiguration connectorConfiguration)
        {
            if (_KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            chatCompletionConnector.KernelService = _KernelService;

            chatCompletionConnector.AddChatCompletion(connectorConfiguration);
        }

        public void AddEmbeddingGenerator(IEmbeddingGeneratorConnector embeddingGeneratorConnector, IAIConnectorConfiguration embeddingConfiguration)
        {
            if (_KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            embeddingGeneratorConnector.KernelService = _KernelService;

            embeddingGeneratorConnector.AddEmbeddingGenerator(embeddingConfiguration);
        }

        public void AddPluginObject(List<object> plugins)
        {
            if (plugins == null || !plugins.Any())
            {
                return;
            }
            if (_KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before adding plugins.");
            }

            IPLuginObject pluginObject = new PLuginObject
            {
                KernelService = _KernelService
            };
            pluginObject.AddPluginObject(plugins);
        }
        public ChatCompletionAgent CreateAgent(string yamContent)
        {
            if (_KernelService == null)
            {
                throw new InvalidOperationException("KernelService is not initialized. Please set KernelService before creating an agent.");
            }

            if (string.IsNullOrWhiteSpace(yamContent))
            {
                throw new ArgumentException("YAML content cannot be null or empty.", nameof(yamContent));
            }
            _KernelService.BuildKernel();
            IAIAgent agent = new AIAgent(_KernelService);
            return agent.CreateAIAgent(yamContent);
        }

        public IChatCompletion RunAgent(ChatCompletionAgent chatCompletionAgent)
        {
            return new ChatCompletion(chatCompletionAgent);
        }
    }
}