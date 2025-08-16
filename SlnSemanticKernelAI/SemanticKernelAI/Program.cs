using AIApplication;
using AIApplication.PizzaOrder;
using AIApplication.Plugin.Plugin;
using SemanticKernelCore.AIAgentCore;

namespace SemanticKernelAI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ymlContent = File.ReadAllText("C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\PizzaOrder\\Prompt\\pizzaorder.yaml");
            List<Object> Plugins = new List<object>();
            Plugins.Add(new PizzaPlugin());

            IChatCompletion chatCompletion = Application.RunApplication(ConnectorType.AzureAiInference, ymlContent, Plugins);
            Chat(chatCompletion);
        }

        static void Chat(IChatCompletion chatCompletion)
        {
            Console.WriteLine();
            Console.Write("AI Agent> Please wait......");

            // Start the chat with the AI agent by sending an initial message   
            var firstresponse = chatCompletion.GetAgentResponseAsync("Please introduce yourself").GetAwaiter().GetResult();
            Console.WriteLine();
            Console.Write(firstresponse);
            Console.WriteLine();

            bool isComplete = false;

            do
            {
                Console.WriteLine();
                Console.Write("User> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    isComplete = true;
                    break;
                }

                Console.WriteLine();
                Console.Write("AI Agent> Please wait......");
                var response = chatCompletion.GetAgentResponseAsync(input).GetAwaiter().GetResult();
                Console.Write("AI Agent>  " + response);

                Console.WriteLine();

            } while (!isComplete);
        }
    }
}
