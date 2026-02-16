namespace Bam.Blobs;

/// <summary>
/// Defines the contract for a blob handle that provides access to blob metadata including chunk information, hash, and properties.
/// </summary>
public interface IBlobHandle
{
    /// <summary>
    /// Gets the total number of chunks that compose this blob.
    /// </summary>
    long ChunkCount { get; }

    /// <summary>
    /// Gets the size, in bytes, of each chunk.
    /// </summary>
    int ChunkSize { get; }

    /// <summary>
    /// Gets the SHA-256 hash that uniquely identifies this blob.
    /// </summary>
    string BlobHash { get; }

    /// <summary>
    /// Gets the total length, in bytes, of the blob data.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// Gets the metadata properties associated with this blob.
    /// </summary>
    /// <returns>An enumerable of <see cref="BlobProperty"/> instances.</returns>
    IEnumerable<BlobProperty> GetBlobProperties();
}