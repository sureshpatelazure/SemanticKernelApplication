using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Connectors
{
    public class OllamaConnector : IChatCompletionConnector
    {
        private readonly IKernelService _kernelService;

        public OllamaConnector(IKernelService kernelService)
        {
            _kernelService = kernelService;
        }

        public void AddChatCompletion()
        {
            throw new NotImplementedException();
        }
    }
}
