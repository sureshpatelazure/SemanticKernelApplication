using AIApplication.AIService;
using SemanticKernelCore.AIAgentCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIApplication.PizzaOrder
{
    public class PizzaOrder
    {
        public IChatCompletion RunPizzaOrderAgent()
        {
            AIChatCompletionService aIChatCompletionService = new OllamaChatCompletionService();
            return aIChatCompletionService.RunChatCompletionService("C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\AIApplication\\PizzaOrder\\Prompt\\pizzaorder.yaml");  
        }
    }
}
