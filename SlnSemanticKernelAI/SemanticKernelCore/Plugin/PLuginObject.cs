using Microsoft.SemanticKernel;
using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Plugin
{
    public class PLuginObject : IPLuginObject
    {
        public IKernelService KernelService { get; set; }
        public void AddPluginObject(List<object> Plugins)
        {
            foreach (var plugin in Plugins)
            {
                KernelService.KernelBuilder.Plugins.AddFromObject(plugin);  
            }
        }
    }
}
