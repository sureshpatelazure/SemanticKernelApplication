using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernelCore.VectorStoreCore.DataLoader
{
    public sealed class RawContent
    {
        public string? Text { get; init; }
        public int PageNumber { get; init; }
    }
}
