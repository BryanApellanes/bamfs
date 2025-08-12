using Bam.Net.CoreServices.Files;
using Bam.Storage;

namespace Bam.Blobs;

public class FsChunkStorage : IChunkStorage
{
    public FsChunkStorage()
    {
        this.ObjectStorage = new FsObjectStorage();
    }

    private FsObjectStorage ObjectStorage { get; init; }
    
    public IChunk? GetChunk(string hash)
    {
        return new Chunk()
        {
            ChunkHash = hash,
            Data = ObjectStorage.LoadHashHexString(hash).Value
        };
    }

    public void SetChunk(IChunk chunk)
    {
        ObjectStorage.Save(chunk.Data);
    }
}