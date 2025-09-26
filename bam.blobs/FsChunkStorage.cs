using Bam.Net.CoreServices.Files;
using Bam.Storage;

namespace Bam.Blobs;

public class FsChunkStorage : IChunkStorage
{
    public FsChunkStorage()
    {
        this.SlottedStorage = new FsSlottedStorage();
    }

    private FsSlottedStorage SlottedStorage { get; init; }
    
    public IChunk? GetChunk(string hash)
    {
        return new Chunk()
        {
            ChunkHash = hash,
            Data = SlottedStorage.LoadHashHexString(hash).Value
        };
    }

    public void SetChunk(IChunk chunk)
    {
        SlottedStorage.Save(chunk.Data);
    }
}