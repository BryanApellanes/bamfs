using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Server.Streaming;

namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Represents a streaming request for a chunk operation, containing the operation type and chunk data.
    /// </summary>
    public class ChunkRequest: StreamingRequest
    {
        /// <summary>
        /// Gets or sets the chunk data to store (used for Set operations).
        /// </summary>
        public Chunk Chunk { get; set; }

        /// <summary>
        /// Gets or sets the hash of the chunk to retrieve (used for Get operations).
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the type of chunk operation to perform.
        /// </summary>
        public ChunkOperation Operation { get; set; }


    }
}
