using Microsoft.Extensions.Azure;
using Microsoft.SemanticKernel;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Assuming this is the correct namespace for Azure AI Inference SDK  


namespace SemanticKernelCore.Connectors.AzureAIInference
{
    public class AzureAIInferenceConnector : IChatCompletionConnector
    {
        public IKernelService KernelService { get; set; }

        public AzureAIInferenceConnector() { }
        public void AddChatCompletion<T>(T connectorConfiguration) where T : IAIConnectorConfiguration
        {
            if (connectorConfiguration is AzureAIInferenceConnectorChatCompletionConfig az)
            {
                KernelService.KernelBuilder.AddAzureAIInferenceChatCompletion (az.ModelId, az.ApiKey, new Uri(az.Uri));
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}
