using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Net.CoreServices.Files;

namespace Bam.Blobs;

/// <summary>
/// Writes blob data to a local repository and distributed chunk storage.
/// </summary>
public class BlobWriter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlobWriter"/> class.
    /// </summary>
    /// <param name="blobRepository">The local blob data repository to write blob metadata to.</param>
    /// <param name="distributedChunkStorage">The distributed chunk storage to write chunk data to.</param>
    public BlobWriter(LocalBlobDataRepository blobRepository, IChunkStorage distributedChunkStorage)
    {
        this.BlobRepository = blobRepository;
        this.DistributedChunkStorage = distributedChunkStorage;
    }

    /// <summary>
    /// Gets the local blob data repository used for storing blob metadata.
    /// </summary>
    public LocalBlobDataRepository BlobRepository { get; }

    /// <summary>
    /// Gets the distributed chunk storage used for storing chunk data.
    /// </summary>
    public IChunkStorage DistributedChunkStorage { get; }

    /*public FileInfo WriteBlobToFile(string blobHash, string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        
        
    }*/
}