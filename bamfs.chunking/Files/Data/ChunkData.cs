using Bam.Data.Repositories;
using Bam.Net.Services.Chunking;

namespace Bam.Chunking
{
    /// <summary>
    /// Represent an arbitrary chunk of data (base 64 encoded)
    /// identified by its hash (Sha256)
    /// </summary>
    [Serializable]
    public class ChunkData: RepoData, IChunkable
    {
        /// <summary>
        /// The Sha256 hash of the base 64 decoded
        /// value of this chunks Data
        /// </summary>
        public string ChunkHash { get; set; } = null!;

        /// <summary>
        /// The length of the base 64 decoded
        /// value of this chunks Data
        /// </summary>
        public int ChunkLength { get; set; }

        /// <summary>
        /// Base64 encoded data
        /// </summary>
        public string Data { get; set; } = null!;

        /// <summary>
        /// Determines equality based on the <see cref="ChunkHash"/> value.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the other object is a <see cref="ChunkData"/> with the same ChunkHash; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is ChunkData data)
            {
                return data.ChunkHash.Equals(ChunkHash);
            }
            return false;
        }

        /// <summary>
        /// Converts this <see cref="ChunkData"/> to an <see cref="IChunk"/> by decoding the base64 data.
        /// </summary>
        /// <returns>An <see cref="IChunk"/> with the decoded data.</returns>
        public IChunk ToChunk()
        {
            return new Chunk { Hash = ChunkHash, Data = Data.FromBase64() };
        }

        /// <summary>
        /// Creates a <see cref="ChunkData"/> from an <see cref="IChunk"/> by base64-encoding its data.
        /// </summary>
        /// <param name="chunk">The chunk to convert.</param>
        /// <returns>A new <see cref="ChunkData"/> with the chunk's hash, data length, and base64-encoded data.</returns>
        public static ChunkData FromChunk(IChunk chunk)
        {
            return new ChunkData { ChunkHash = chunk.Hash, Data = chunk.Data.ToBase64(), ChunkLength = chunk.Data.Length };
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return ChunkHash.GetHashCode();
        }
    }
}
