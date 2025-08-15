using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors
{
    public interface IChatCompletionConnector 
    {
        public  IKernelService KernelService { get; set; }
        void AddChatCompletion<T>(T connectorConfiguration ) where T : IAIConnectorConfiguration;
    }
}