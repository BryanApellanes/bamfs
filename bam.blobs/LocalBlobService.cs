using Bam.Blobs;
using Bam.Blobs.Data.Local;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Net.CoreServices.Files;

namespace Bam.Files;

/// <summary>
/// Provides local blob storage operations, persisting blob metadata to a local repository and chunk data to a data directory.
/// </summary>
public class LocalBlobService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalBlobService"/> class.
    /// </summary>
    /// <param name="fileSystemChunkStorage">An optional data directory chunk storage instance. Uses a default instance if not provided.</param>
    public LocalBlobService(DataDirectoryChunkStorage? fileSystemChunkStorage = null)
    {
        this.BlobRepository = new LocalBlobDataRepository();
        this.DataDirectoryChunkStorage = fileSystemChunkStorage ?? new DataDirectoryChunkStorage();
    }
    protected LocalBlobDataRepository BlobRepository { get; init; }
    protected DataDirectoryChunkStorage DataDirectoryChunkStorage { get; init; }

    /// <summary>
    /// Saves a blob by persisting its handle, chunks, and properties asynchronously.
    /// </summary>
    /// <param name="blobHandle">The blob to save.</param>
    /// <returns>The saved <see cref="BlobHandleData"/> containing the blob metadata.</returns>
    public async Task<BlobHandleData> SaveBlobAsync(Blob blobHandle)
    {
        BlobHandleData handle = await BlobRepository.SaveAsync(new BlobHandleData()
        {
            BlobHash = blobHandle.BlobHash
        });

        List<Task> tasks = new List<Task>();
        for (long chunkIndex = 0; chunkIndex < blobHandle.ChunkCount; chunkIndex++)
        {
            BlobChunk chunk = blobHandle[chunkIndex];
            tasks.Add(SaveDistributableChunk(chunk));
            tasks.Add(SaveBlobChunk(chunk));
        }

        foreach (BlobProperty property in blobHandle.GetBlobProperties())
        {
            tasks.Add(SaveBlobProperty(property));
        }
        
        Task.WaitAll(tasks.ToArray());
        
        return handle;
    }

    private Task<IChunk> SaveDistributableChunk(BlobChunk chunk)
    {
        return Task.Run(IChunk () =>
        {
            DataDirectoryChunkStorage.SetChunk(chunk);
            return chunk;
        });
    }
    
    private Task<BlobChunkData> SaveBlobChunk(BlobChunk blobChunk)
    {
        return Task.Run(() => BlobRepository.Save(new BlobChunkData()
        {
            BlobHash = blobChunk.BlobHash,
            ChunkIndex = blobChunk.ChunkIndex,
            BlobIndex = blobChunk.BlobIndex,
        }));
    }

    private Task<BlobPropertyData> SaveBlobProperty(BlobProperty blobProperty)
    {
        return Task.Run(() => BlobRepository.Save(new BlobPropertyData()
        {
            BlobHash = blobProperty.BlobHash,
            Name = blobProperty.Name,
            Value = blobProperty.Value
        }));
    }
}