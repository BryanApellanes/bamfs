using Bam.Net.CoreServices.Files;
using Bam.Storage;

namespace Bam.Blobs;

/// <summary>
/// An <see cref="IChunkStorage"/> implementation that stores and retrieves chunks using <see cref="FsSlottedStorage"/>.
/// </summary>
public class FsChunkStorage : IChunkStorage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FsChunkStorage"/> class with a default <see cref="FsSlottedStorage"/>.
    /// </summary>
    public FsChunkStorage()
    {
        this.SlottedStorage = new FsSlottedStorage();
    }

    private FsSlottedStorage SlottedStorage { get; init; }
    
    /// <summary>
    /// Retrieves a chunk from slotted storage by its hash hex string.
    /// </summary>
    /// <param name="hash">The SHA-256 hash of the chunk to retrieve.</param>
    /// <returns>A <see cref="Chunk"/> containing the stored data.</returns>
    public IChunk? GetChunk(string hash)
    {
        return new Chunk()
        {
            ChunkHash = hash,
            Data = SlottedStorage.LoadHashHexString(hash).Value
        };
    }

    /// <summary>
    /// Stores a chunk's data in slotted storage.
    /// </summary>
    /// <param name="chunk">The chunk to store.</param>
    public void SetChunk(IChunk chunk)
    {
        SlottedStorage.Save(chunk.Data);
    }
}