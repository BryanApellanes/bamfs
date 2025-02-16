namespace Bam.Blobs;

public abstract class Blob: IBlobHandle
{
    public virtual long ChunkCount { get; }
    public int ChunkSize { get; protected init; }
    public string BlobHash { get; protected init; }
    public long Length { get; protected init; }

    public abstract BlobChunk this[long chunkIndex] { get; }
    
    public virtual IEnumerable<BlobProperty> GetBlobProperties()
    {
        return Array.Empty<BlobProperty>();
    } 
}