using SemanticKernelCore.Connectors.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors
{
    public interface IChatCompletionConnector 
    {
        void AddChatCompletion<T>(T connectorConfiguration ) where T : IAIConnectorConfiguration;
    }
}