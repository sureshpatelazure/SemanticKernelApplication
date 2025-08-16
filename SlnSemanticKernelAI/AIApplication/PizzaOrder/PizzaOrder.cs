using AIApplication.AIService;
using AIApplication.Plugin.Plugin;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.KernelCore;

namespace AIApplication.PizzaOrder
{
    public class PizzaOrder
    {
        public IChatCompletion RunPizzaOrderAgent()
        {
           // AIService.AIService aIChatCompletionService = new OllamaService();
           //  AIService.AIService aIChatCompletionService = new HuggingFaceService();
           AIService.AIService aIChatCompletionService = new AzureAiInferenceService();

             IKernelService kernelService = new KernelService();

            aIChatCompletionService.KernelService = kernelService;

            string yamContent = File.ReadAllText ("C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\AIApplication\\PizzaOrder\\Prompt\\pizzaorder.yaml");
            List<Object> Plugins = new List<object>();
            // Pizza  Order
            Plugins.Add(new PizzaPlugin());

            return aIChatCompletionService.RunChatCompletionService(yamContent, Plugins);  
        }
    }
}
