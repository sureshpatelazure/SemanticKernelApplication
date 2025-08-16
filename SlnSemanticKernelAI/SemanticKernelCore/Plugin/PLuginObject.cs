using Microsoft.SemanticKernel;
using SemanticKernelCore.KernelCore;

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
