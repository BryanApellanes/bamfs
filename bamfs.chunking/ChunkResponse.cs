using Bam.Net.CoreServices.Files;
using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Represents a streaming response for a chunk operation, containing the result status and optional chunk data.
    /// </summary>
    public class ChunkResponse: StreamingResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether the chunk operation succeeded.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets an error or status message associated with the response.
        /// </summary>
        public new string Message { get; set; }

        /// <summary>
        /// Gets or sets the chunk returned by a Get operation.
        /// </summary>
        public IChunk Chunk { get; set; }
        /// <summary>
        /// Throw an exception if the Data.Hash does not
        /// match Hash
        /// </summary>
        public void Validate()
        {
            if (!Chunk.Data.Sha256().Equals(Chunk.Hash))
            {
                throw new InvalidOperationException("Hash check failed");
            }
        }
    }
}
