namespace Bam.Blobs;

/// <summary>
/// Abstract base class representing a blob of data composed of one or more chunks.
/// </summary>
public abstract class Blob: IBlobHandle
{
    /// <summary>
    /// Gets the total number of chunks that compose this blob.
    /// </summary>
    public virtual long ChunkCount { get; }

    /// <summary>
    /// Gets the size, in bytes, of each chunk.
    /// </summary>
    public int ChunkSize { get; protected init; }

    /// <summary>
    /// Gets the SHA-256 hash that uniquely identifies this blob.
    /// </summary>
    public string BlobHash { get; protected init; } = null!;

    /// <summary>
    /// Gets the total length, in bytes, of the blob data.
    /// </summary>
    public long Length { get; protected init; }

    /// <summary>
    /// Gets the <see cref="BlobChunk"/> at the specified chunk index.
    /// </summary>
    /// <param name="chunkIndex">The zero-based index of the chunk to retrieve.</param>
    /// <returns>The <see cref="BlobChunk"/> at the specified index.</returns>
    public abstract BlobChunk this[long chunkIndex] { get; }

    /// <summary>
    /// Gets the metadata properties associated with this blob.
    /// </summary>
    /// <returns>An enumerable of <see cref="BlobProperty"/> instances; empty by default.</returns>
    public virtual IEnumerable<BlobProperty> GetBlobProperties()
    {
        return Array.Empty<BlobProperty>();
    }
}