using SemanticKernelCore.KernelCore;
using SemanticKernelCore.Connectors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SemanticKernelCore.Connectors.Ollama;
using Microsoft.SemanticKernel.Services;
using SemanticKernelCore.Connectors.Configuration;

namespace SemanticKernelAI
{
    internal class Program
    {
        static void Main(string[] args)
        {
  
            //var builder = Host.CreateApplicationBuilder(args);

            //builder.Services.AddSingleton<IKernelService, KernelService>();
           

            //var app = builder.Build();
            // Resolve and use your services as needed

            //Console.WriteLine("Hello, World!");
            IKernelService kernelService = new KernelService();
            kernelService.CreatekernelBuilder();

            IChatCompletionConnector chatCompletionConnector = new OllamaConnector(kernelService);
            OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

            ollamConfig.ModelId = "llama3.1:latest";
            ollamConfig.Uri = "http://localhost:11434/";    
            chatCompletionConnector.AddChatCompletion(ollamConfig);

            kernelService.BuildKernel();

        }
    }
}
