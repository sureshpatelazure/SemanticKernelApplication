using SemanticKernelCore.KernelCore;
using SemanticKernelCore.Connectors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SemanticKernelCore.Connectors.Ollama;
using Microsoft.SemanticKernel.Services;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;
using Microsoft.SemanticKernel.Agents.Runtime;
using SemanticKernelCore.AIAgentCore;
using Microsoft.SemanticKernel.Agents;

namespace SemanticKernelAI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //RunOllama();

            RunHuggingFace();

        }

        static void RunOllama()
        {
            IKernelService kernelService = new KernelService();
            kernelService.CreatekernelBuilder();

            IChatCompletionConnector chatCompletionConnector = new OllamaConnector(kernelService);
            OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

            ollamConfig.ModelId = "llama3.2:latest";
            ollamConfig.Uri = "http://localhost:11434/";
            chatCompletionConnector.AddChatCompletion(ollamConfig);

            kernelService.BuildKernel();

            string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\pizzaorder.yaml";
            string yamContent = File.ReadAllText(filePath);

            IAIAgent agent = new AIAgent(kernelService);
            ChatCompletionAgent chatCompletionAgent = agent.CreateAIAgent(yamContent);

            IChatCompletion chatCompletion = new ChatCompletion(chatCompletionAgent);
           var response = chatCompletion.GetAgentResponseAsync("What is the capital of France?").GetAwaiter().GetResult();    
        }

        static void RunHuggingFace()
        {
            IKernelService kernelService = new KernelService();
            kernelService.CreatekernelBuilder();
            
            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector(kernelService);
            HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();
            
            hfConfig.ModelId = "google/gemma-2-2b-it";
            hfConfig.Uri = "https://router.huggingface.co/";
            hfConfig.ApiKey = ""; // Replace with your actual API key
            chatCompletionConnector.AddChatCompletion(hfConfig);
            
            kernelService.BuildKernel();

            string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\pizzaorder.yaml";
            string yamContent = File.ReadAllText(filePath);

            IAIAgent agent = new AIAgent(kernelService);
            ChatCompletionAgent chatCompletionAgent = agent.CreateAIAgent(yamContent);

            IChatCompletion chatCompletion = new ChatCompletion(chatCompletionAgent);
            var response = chatCompletion.GetAgentResponseAsync("What is the capital of France?").GetAwaiter().GetResult();
        }
    }
}
