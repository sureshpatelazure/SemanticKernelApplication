using SemanticKernelCore.KernelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.Plugin
{
    public interface IPLuginObject
    {
        public IKernelService KernelService { get; set; }
        public void AddPluginObject(List<object> Plugins);
    }
}
