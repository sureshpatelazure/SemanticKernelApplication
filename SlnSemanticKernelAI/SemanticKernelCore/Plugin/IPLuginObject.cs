using SemanticKernelCore.KernelCore;

namespace SemanticKernelCore.Plugin
{
    public interface IPLuginObject
    {
        public IKernelService KernelService { get; set; }
        public void AddPluginObject(List<object> Plugins);
    }
}
