using Microsoft.SemanticKernel;

namespace SemanticKernelCore.KernelCore
{
    public interface IKernelService
    {
        public IKernelBuilder CreatekernelBuilder();
        public Kernel BuildKernel(IKernelBuilder builder);
    }
}