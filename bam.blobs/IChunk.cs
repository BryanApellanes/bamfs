namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// Defines the contract for a chunk of data identified by its SHA-256 hash.
    /// </summary>
    public interface IChunk
    {
        /// <summary>
        /// Gets the SHA-256 hash of the chunk data.
        /// </summary>
        string ChunkHash { get; }

        /// <summary>
        /// Gets or sets the raw byte data of this chunk.
        /// </summary>
        byte[] Data { get; set; }
    }
}
