using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelCore.AIAgentCore
{
    public class ChatCompletion : IChatCompletion
    {
        private readonly ChatHistoryAgentThread _chatHistoryAgentThread;
        private readonly ChatCompletionAgent _agent;

        public ChatCompletion(ChatCompletionAgent agent)
        {
            _chatHistoryAgentThread = new ChatHistoryAgentThread();
            _agent = agent ?? throw new ArgumentNullException(nameof(agent));
        }   

        public async Task<string?> GetAgentResponseAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var message = new ChatMessageContent(AuthorRole.User, input);

            await foreach (ChatMessageContent response in _agent.InvokeAsync(message, _chatHistoryAgentThread))
            {
                return response.Content;
            }

            return null;
        }
    }   
   
}