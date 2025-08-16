using AIApplication.AIService;
using SemanticKernelCore.AIAgentCore;
using SemanticKernelCore.KernelCore;

namespace AIApplication.PizzaOrder
{
    public class PizzaOrder
    {
        public IChatCompletion RunPizzaOrderAgent()
        {
            //AIService.AIService aIChatCompletionService = new OllamaService();
            AIService.AIService aIChatCompletionService = new HuggingFaceService();
            IKernelService kernelService = new KernelService();

            aIChatCompletionService.KernelService = kernelService;

            string yamContent = File.ReadAllText ("C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\AIApplication\\PizzaOrder\\Prompt\\pizzaorder.yaml"); 
            return aIChatCompletionService.RunChatCompletionService(yamContent);  
        }
    }
}
