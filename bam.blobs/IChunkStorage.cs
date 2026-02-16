namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// Defines the contract for storing and retrieving chunks by hash.
    /// </summary>
    public interface IChunkStorage
    {
        /// <summary>
        /// Retrieves a chunk by its hash.
        /// </summary>
        /// <param name="hash">The SHA-256 hash of the chunk to retrieve.</param>
        /// <returns>The chunk if found; otherwise, null.</returns>
        IChunk? GetChunk(string hash);

        /// <summary>
        /// Stores a chunk.
        /// </summary>
        /// <param name="chunk">The chunk to store.</param>
        void SetChunk(IChunk chunk);
    }
}
