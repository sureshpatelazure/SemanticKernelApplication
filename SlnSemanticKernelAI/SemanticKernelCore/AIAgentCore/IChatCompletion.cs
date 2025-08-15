using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.AIAgentCore
{
    public interface IChatCompletion
    {
        public Task<string?> GetAgentResponseAsync(string input);
    }
}
