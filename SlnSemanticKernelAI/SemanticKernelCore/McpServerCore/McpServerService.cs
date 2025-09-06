using SemanticKernelCore.KernelCore;
using SemanticKernelCore.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.McpServerCore
{
    public class McpServerService : IMcpServerService
    {
        public IKernelService KernelService { get; set; }
        public void AddTool(List<object> plugins)
        {
            IPLuginObject pLuginObject = new PLuginObject();
            pLuginObject.KernelService = KernelService;
            
            pLuginObject.AddPluginObject(plugins);

        }
    }
}
