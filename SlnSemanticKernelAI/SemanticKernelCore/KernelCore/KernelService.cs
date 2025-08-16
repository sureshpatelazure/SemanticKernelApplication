using Microsoft.SemanticKernel;

namespace SemanticKernelCore.KernelCore
{
    public class KernelService : IKernelService
    {
        private  IKernelBuilder? _kernelBuilder;
        private  Kernel? _kernel;
        public KernelService()
        {
            _kernelBuilder = null;
            _kernel = null;
        }

        public IKernelBuilder KernelBuilder => _kernelBuilder ?? throw new InvalidOperationException("KernelBuilder is not created. Call CreatekernelBuilder() before accessing KernelBuilder.");   
        public Kernel Kernel => _kernel ?? throw new InvalidOperationException("Kernel is not built. Call BuildKernel() after CreatekernelBuilder().");
        public void CreatekernelBuilder()
        {
            _kernelBuilder = Kernel.CreateBuilder();
        }

        public void BuildKernel()
        {
            if (_kernelBuilder == null)
            {
                throw new InvalidOperationException("_kernelBuilder is null. Call CreatekernelBuilder() before BuildKernel().");
            }

            _kernel = _kernelBuilder.Build();
        }

    }
}
