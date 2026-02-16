using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Exception thrown when a chunk operation fails, wrapping the server's <see cref="ChunkResponse"/>.
    /// </summary>
    public class ChunkException: Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkException"/> class with the specified chunk response.
        /// </summary>
        /// <param name="response">The chunk response containing the error message.</param>
        public ChunkException(ChunkResponse response): base(response.Message)
        {
            ChunkResponse = response;
        }
        /// <summary>
        /// Gets or sets the chunk response that caused this exception.
        /// </summary>
        public ChunkResponse ChunkResponse { get; set; }
    }
}
