using Bam.Blobs.Data.Local;
using Bam.Net.CoreServices.Files;

namespace Bam.Blobs;

/// <summary>
/// A <see cref="Blob"/> loaded from storage. Lazy-loads chunk data from <see cref="IChunkStorage"/> on access.
/// </summary>
public class StoredBlob : Blob
{
    private readonly List<BlobChunkData> _chunkDescriptors;
    private readonly List<BlobPropertyData> _properties;
    private readonly IChunkStorage _chunkStorage;

    /// <summary>
    /// Initializes a new instance of the <see cref="StoredBlob"/> class.
    /// </summary>
    /// <param name="blobHash">The SHA-256 hash of the blob.</param>
    /// <param name="chunkSize">The size, in bytes, of each chunk.</param>
    /// <param name="length">The total length, in bytes, of the blob data.</param>
    /// <param name="chunkDescriptors">The chunk descriptors loaded from the repository, ordered by ChunkIndex.</param>
    /// <param name="properties">The blob property data loaded from the repository.</param>
    /// <param name="chunkStorage">The chunk storage to retrieve chunk bytes from.</param>
    public StoredBlob(string blobHash, int chunkSize, long length, List<BlobChunkData> chunkDescriptors, List<BlobPropertyData> properties, IChunkStorage chunkStorage)
    {
        BlobHash = blobHash;
        ChunkSize = chunkSize;
        Length = length;
        _chunkDescriptors = chunkDescriptors;
        _properties = properties;
        _chunkStorage = chunkStorage;
    }

    /// <summary>
    /// Gets the total number of chunks that compose this blob.
    /// </summary>
    public override long ChunkCount => _chunkDescriptors.Count;

    /// <summary>
    /// Gets the <see cref="BlobChunk"/> at the specified chunk index by loading it from chunk storage.
    /// </summary>
    /// <param name="chunkIndex">The zero-based index of the chunk to retrieve.</param>
    /// <returns>The <see cref="BlobChunk"/> at the specified index.</returns>
    public override BlobChunk this[long chunkIndex]
    {
        get
        {
            BlobChunkData descriptor = _chunkDescriptors[(int)chunkIndex];
            IChunk? chunk = _chunkStorage.GetChunk(descriptor.ChunkHash);
            return new BlobChunk
            {
                BlobHash = BlobHash,
                ChunkIndex = descriptor.ChunkIndex,
                BlobIndex = descriptor.BlobIndex,
                Data = chunk?.Data ?? Array.Empty<byte>()
            };
        }
    }

    /// <summary>
    /// Gets the metadata properties associated with this blob.
    /// </summary>
    /// <returns>An enumerable of <see cref="BlobProperty"/> instances.</returns>
    public override IEnumerable<BlobProperty> GetBlobProperties()
    {
        return _properties.Select(p => new BlobProperty
        {
            BlobHash = p.BlobHash,
            Name = p.Name,
            Value = p.Value
        });
    }
}
