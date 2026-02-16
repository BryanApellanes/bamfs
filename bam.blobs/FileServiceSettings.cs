using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Blobs
{
    /// <summary>
    /// Configuration settings for the <see cref="FileService"/>, used when copying settings between instances.
    /// </summary>
    public class FileServiceSettings // Used by Copy from within FileService
    {
        /// <summary>
        /// Gets or sets the file system directory where chunks are stored.
        /// </summary>
        public string ChunkDirectory { get; set; }

        /// <summary>
        /// Gets or sets the number of chunk data descriptors to retrieve per batch.
        /// </summary>
        public int ChunkDataBatchSize { get; set; }

        /// <summary>
        /// Gets or sets the length, in bytes, of each chunk when chunking files.
        /// </summary>
        public int ChunkLength { get; set; }

        /// <summary>
        /// Gets or sets the timeout for asynchronous check operations.
        /// </summary>
        public TimeSpan AsyncCheckTimeout { get; set; }
    }
}
