using Microsoft.SemanticKernel;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors.HuggingFace
{
    public class HuggingFaceConnector : IChatCompletionConnector
    {
        public  IKernelService KernelService { get; set; }

        public HuggingFaceConnector() { }
        public void AddChatCompletion<T>(T connectorConfiguration) where T : IAIConnectorConfiguration
        {
            if (connectorConfiguration is HuggingFaceConnectorChatCompletionConfig hf)
            {
                KernelService.KernelBuilder.AddHuggingFaceChatCompletion(hf.ModelId, new Uri(hf.Uri), hf.ApiKey);
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}
