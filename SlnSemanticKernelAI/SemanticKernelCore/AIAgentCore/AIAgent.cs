using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using SemanticKernelCore.Helper;
using SemanticKernelCore.KernelCore;

namespace SemanticKernelCore.AIAgentCore
{
    public class AIAgent :IAIAgent
    {
        private readonly IKernelService _kernelService;
        public AIAgent(IKernelService kernelService)
        {
            _kernelService = kernelService;
        }   
        public ChatCompletionAgent CreateAIAgent(string yamlContent)
        {
            var yamlData = YamlHelper.ReadYaml(yamlContent);

            PromptTemplateConfig templateConfig = new PromptTemplateConfig(yamlContent);
            KernelPromptTemplateFactory templateFactory = new KernelPromptTemplateFactory();

            ChatCompletionAgent agent = new(templateConfig, templateFactory)
            {
                Kernel = _kernelService.Kernel,
                Arguments = new KernelArguments(
                     new PromptExecutionSettings
                     {
                         FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                     }
                ),
                Description = (string)yamlData["description"]
            };

            return agent;

        }
    }   
}
