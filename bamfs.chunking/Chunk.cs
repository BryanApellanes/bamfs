using Bam.Chunking;

namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Represents a chunk of data identified by its hash.
    /// </summary>
    public class Chunk : IChunk
    {
        /// <summary>
        /// Gets or sets the SHA-256 hash of the chunk data.
        /// </summary>
        public string Hash { get; set; } = null!;

        /// <summary>
        /// Gets or sets the raw byte data of this chunk.
        /// </summary>
        public byte[] Data { get; set; } = null!;

        /// <summary>
        /// Returns this instance as an <see cref="IChunk"/>.
        /// </summary>
        /// <returns>This chunk instance.</returns>
        public IChunk ToChunk()
        {
            return this;
        }
    }
}
