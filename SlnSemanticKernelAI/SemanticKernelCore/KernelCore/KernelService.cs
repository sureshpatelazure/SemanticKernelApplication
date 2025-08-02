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
        public IKernelBuilder CreatekernelBuilder()
        {
            return Kernel.CreateBuilder();
        }

        public Kernel BuildKernel(IKernelBuilder builder)
        {
            return builder.Build();
        }

    }
}
