using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.McpServerCore
{
    public interface IMcpServerService
    {
        public IKernelService KernelService { get; set; }
        void AddTool(List<object> plugins); 
    }
}
