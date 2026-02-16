using Bam.Net.CoreServices.Files;

namespace Bam.Blobs
{
    /// <summary>
    /// Represents a chunk of data identified by its SHA-256 hash.
    /// </summary>
    public class Chunk : IChunk
    {
        private string? _chunkHash;
        /// <summary>
        /// Gets or sets the SHA-256 hash of the chunk data. Computed lazily from <see cref="Data"/> if not explicitly set.
        /// </summary>
        public string ChunkHash
        {
            get
            {
                if (string.IsNullOrEmpty(_chunkHash))
                {
                    _chunkHash = Data?.Sha256();
                }
                return _chunkHash ?? string.Empty;
            }
            set => _chunkHash = value;
        }

        /// <summary>
        /// Gets or sets the raw byte data of this chunk.
        /// </summary>
        public virtual byte[] Data { get; set; }
    }

}
