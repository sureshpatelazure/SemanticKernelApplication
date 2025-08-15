using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

namespace SemanticKernelCore.AIAgentCore
{
    public interface IAIAgent
    {
        public ChatCompletionAgent CreateAIAgent(string yamlContent);
    }
}
