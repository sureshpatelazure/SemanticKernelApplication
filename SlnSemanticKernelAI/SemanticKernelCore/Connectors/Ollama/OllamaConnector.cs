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
        public  IKernelService KernelService { get; set; }

        public OllamaConnector() { }
        public void AddChatCompletion<T>(T connectorConfiguration) where T : IAIConnectorConfiguration
        {
            if (connectorConfiguration is OllamaConnectorChatCompletionConfig ollama)
            {
                KernelService.KernelBuilder.AddOllamaChatCompletion(ollama.ModelId, new Uri(ollama.Uri));
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
                KernelService.KernelBuilder.AddOllamaEmbeddingGenerator(ollama.ModelId, new Uri(ollama.Uri));
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}
