using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IKernelBuilder KernelBuilder => _kernelBuilder; 
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
