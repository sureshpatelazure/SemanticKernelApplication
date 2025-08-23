﻿using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Data;

namespace SemanticKernelCore.VectorStoreCore.DataLoader
{
    public sealed class DataContent
    {
        [VectorStoreKey]
        [TextSearchResultName]
        public ulong Key { get; set; }

        [VectorStoreData]
        [TextSearchResultValue]
        public string Text { get; set; } = string.Empty;

        // Note that the vector property is typed as a string, and
        // its value is derived from the Text property. The string
        // value will however be converted to a vector on upsert and
        // stored in the database as a vector.
        [VectorStoreVector(384)]
        public string Embedding => this.Text;
    }
}
