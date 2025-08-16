using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;

namespace SemanticKernelCore.Connectors
{
    public interface IChatCompletionConnector 
    {
        public  IKernelService KernelService { get; set; }
        void AddChatCompletion<T>(T connectorConfiguration ) where T : IAIConnectorConfiguration;
    }
}