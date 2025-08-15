using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Runtime;
using Microsoft.SemanticKernel.Services;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.Connectors;
using SemanticKernelCore.Connectors.Configuration;
using SemanticKernelCore.Connectors.HuggingFace;
using SemanticKernelCore.Connectors.Ollama;
using SemanticKernelCore.KernelCore;

namespace SemanticKernelAI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //RunOllama();

            RunHuggingFace();

        }

        static void RunChatCompletion(IChatCompletionConnector chatCompletionConnector, 
            IAIConnectorConfiguration connectorConfiguration,string yamContent)
        {
            IKernelService kernelService = new KernelService();
            kernelService.CreatekernelBuilder();

            chatCompletionConnector.KernelService = kernelService;

            chatCompletionConnector.AddChatCompletion(connectorConfiguration);

            kernelService.BuildKernel();

            IAIAgent agent = new AIAgent(kernelService);
            ChatCompletionAgent chatCompletionAgent = agent.CreateAIAgent(yamContent);

            IChatCompletion chatCompletion = new ChatCompletion(chatCompletionAgent);
            var response = chatCompletion.GetAgentResponseAsync("What is the capital of France?").GetAwaiter().GetResult();

        }
        static void RunOllama()
        {
            IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
            OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

            ollamConfig.ModelId = "llama3.2:latest";
            ollamConfig.Uri = "http://localhost:11434/";
            chatCompletionConnector.AddChatCompletion(ollamConfig);

            string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\pizzaorder.yaml";
            string yamContent = File.ReadAllText(filePath);

            RunChatCompletion(chatCompletionConnector, ollamConfig, yamContent);
        }

        static void RunHuggingFace()
        {
            
            IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
            HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();
            
            hfConfig.ModelId = "google/gemma-2-2b-it";
            hfConfig.Uri = "https://router.huggingface.co/";
            hfConfig.ApiKey = ""; // Replace with your actual API key

            string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\pizzaorder.yaml";
            string yamContent = File.ReadAllText(filePath); 

            RunChatCompletion(chatCompletionConnector, hfConfig, yamContent);
        }
    }
}
