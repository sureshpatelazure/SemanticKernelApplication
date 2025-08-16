using AIApplication.PizzaOrder;
using SemanticKernelCore.AIAgentCore;

namespace SemanticKernelAI
{
    internal class Program
    {
        static void Main(string[] args)
        {

            PizzaOrder pizzaOrder = new PizzaOrder();
            IChatCompletion chatCompletion = pizzaOrder.RunPizzaOrderAgent();
            Chat(chatCompletion);
            // RunOllama();

            // RunHuggingFace();
            //    RunAzureAIInference();

        }

        //static void RunChatCompletion(IChatCompletionConnector chatCompletionConnector, 
        //    IAIConnectorConfiguration connectorConfiguration,string yamContent)
        //{
        //    IKernelService kernelService = new KernelService();
        //    kernelService.CreatekernelBuilder();

        //    chatCompletionConnector.KernelService = kernelService;

        //    chatCompletionConnector.AddChatCompletion(connectorConfiguration);

        //    IPLuginObject pLuginObject = new PLuginObject();
        //    pLuginObject.KernelService = kernelService;

        //    List<Object> Plugins = new List<object>();
        //    // Pizza  Order
        //    Plugins.Add(new PizzaPlugin());

        //    pLuginObject.AddPluginObject(Plugins);

        //    kernelService.BuildKernel();

        //    IAIAgent agent = new AIAgent(kernelService);
        //    ChatCompletionAgent chatCompletionAgent = agent.CreateAIAgent(yamContent);

        //    IChatCompletion chatCompletion = new ChatCompletion(chatCompletionAgent);
        //    // var response = chatCompletion.GetAgentResponseAsync("What is the capital of France?").GetAwaiter().GetResult();
        //    Chat(chatCompletion);

        //}

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
        //static void RunOllama()
        //{
        //    IChatCompletionConnector chatCompletionConnector = new OllamaConnector();
        //    OllamaConnectorChatCompletionConfig ollamConfig = new OllamaConnectorChatCompletionConfig();

        //    ollamConfig.ModelId = "llama3.2:latest";
        //    ollamConfig.Uri = "http://localhost:11434/";

        //    string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\PizzaOrderAIAgentDemo\\Prompt\\pizzaorder.yaml";
        //    string yamContent = File.ReadAllText(filePath);

        //    RunChatCompletion(chatCompletionConnector, ollamConfig, yamContent);
        //}

        //static void RunHuggingFace()
        //{

        //    IChatCompletionConnector chatCompletionConnector = new HuggingFaceConnector();
        //    HuggingFaceConnectorChatCompletionConfig hfConfig = new HuggingFaceConnectorChatCompletionConfig();

        //    hfConfig.ModelId = "google/gemma-2-2b-it";
        //    hfConfig.Uri = "https://router.huggingface.co/";
        //    hfConfig.ApiKey = ""; // Replace with your actual API key

        //    string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\PizzaOrderAIAgentDemo\\Prompt\\pizzaorder.yaml";
        //    string yamContent = File.ReadAllText(filePath); 

        //    RunChatCompletion(chatCompletionConnector, hfConfig, yamContent);
        //}

        //static void RunAzureAIInference()
        //{

        //    IChatCompletionConnector chatCompletionConnector = new AzureAIInferenceConnector();
        //    AzureAIInferenceConnectorChatCompletionConfig azConfig = new AzureAIInferenceConnectorChatCompletionConfig();

        //    azConfig.ModelId = "deepseek/DeepSeek-R1-0528";
        //    azConfig.Uri = "https://models.github.ai/inference";
        //    azConfig.ApiKey = ""; // Replace with your actual API key

        //    string filePath = "C:\\GenAI\\GitHub - Semantic Kernel Application\\SlnSemanticKernelAI\\SemanticKernelAI\\PizzaOrderAIAgentDemo\\Prompt\\pizzaorder.yaml";
        //    string yamContent = File.ReadAllText(filePath);

        //    RunChatCompletion(chatCompletionConnector, azConfig, yamContent);
        //}
    }
}
