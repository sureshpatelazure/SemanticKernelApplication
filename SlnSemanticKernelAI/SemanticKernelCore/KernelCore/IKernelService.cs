using Microsoft.SemanticKernel;

namespace SemanticKernelCore.KernelCore
{
    public interface IKernelService
    {
        public void CreatekernelBuilder();
        public void BuildKernel();

        IKernelBuilder KernelBuilder { get; }
        Kernel Kernel { get; }
    }
}