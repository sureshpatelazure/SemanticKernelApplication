namespace SemanticKernelCore.AIAgentCore
{
    public interface IChatCompletion
    {
        public Task<string?> GetAgentResponseAsync(string input);
    }
}
