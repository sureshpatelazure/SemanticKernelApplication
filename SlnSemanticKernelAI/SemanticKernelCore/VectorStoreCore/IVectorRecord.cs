using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.VectorStoreCore
{
    public interface IVectorRecord
    {
        ulong Key { get; set; }
        string Text { get; set; }
    }
}
