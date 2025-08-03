using Microsoft.SemanticKernel;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors.Ollama
{
    public class OllamaConnector : IChatCompletionConnector, IEmbeddingGenerationConnector
    {
        private readonly IKernelService _kernelService;

        public OllamaConnector(IKernelService kernelService)
        {
            _kernelService = kernelService;
        }
        public void AddChatCompletion<T>(T connectorConfiguration) where T : IAIConnectorConfiguration
        {
            if (connectorConfiguration is OllamaConnectorChatCompletionConfig ollama)
            {
                var builder =_kernelService.CreatekernelBuilder();
                builder.AddOllamaChatCompletion(ollama.ModelId, new Uri(ollama.Uri));
                _kernelService.BuildKernel(builder);  // Get Kernel instance after adding the connector 
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }

        public void AddEmbeddingGeneration<T>(T connectorConfiguration) where T : IAIConnectorConfiguration
        {
            if (connectorConfiguration is OllamaConnectorEmbeddingConfig ollama)
            {
                var builder = _kernelService.CreatekernelBuilder();
                builder.AddOllamaEmbeddingGenerator(ollama.ModelId, new Uri(ollama.Uri));
                _kernelService.BuildKernel(builder);  // Get Kernel instance after adding the connector 
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}
