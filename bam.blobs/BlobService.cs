using Bam.Blobs.Data.Local;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Data;
using Bam.Net.CoreServices.Files;

namespace Bam.Blobs;

/// <summary>
/// Provides blob storage operations, persisting blob metadata to a repository and chunk data to an <see cref="IChunkStorage"/>.
/// </summary>
public class BlobService : IBlobService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlobService"/> class.
    /// </summary>
    /// <param name="chunkStorage">An optional chunk storage instance. Uses a default <see cref="DataDirectoryChunkStorage"/> if not provided.</param>
    public BlobService(IChunkStorage? chunkStorage = null)
    {
        BlobRepository = new LocalBlobDataRepository();
        ChunkStorage = chunkStorage ?? new DataDirectoryChunkStorage();
    }

    protected LocalBlobDataRepository BlobRepository { get; init; }
    protected IChunkStorage ChunkStorage { get; init; }

    /// <summary>
    /// Saves a blob by persisting its handle, chunks, and properties asynchronously.
    /// </summary>
    /// <param name="blob">The blob to save.</param>
    /// <returns>The saved <see cref="BlobHandleData"/> containing the blob metadata.</returns>
    public async Task<BlobHandleData> SaveBlobAsync(Blob blob)
    {
        BlobHandleData handle = await BlobRepository.SaveAsync(new BlobHandleData()
        {
            BlobHash = blob.BlobHash
        });

        List<Task> tasks = new List<Task>();
        for (long chunkIndex = 0; chunkIndex < blob.ChunkCount; chunkIndex++)
        {
            BlobChunk chunk = blob[chunkIndex];
            tasks.Add(SaveDistributableChunk(chunk));
            tasks.Add(SaveBlobChunk(chunk));
        }

        foreach (BlobProperty property in blob.GetBlobProperties())
        {
            tasks.Add(SaveBlobProperty(property));
        }

        tasks.Add(SaveBlobProperty(new BlobProperty
        {
            BlobHash = blob.BlobHash,
            Name = "ChunkSize",
            Value = blob.ChunkSize.ToString()
        }));

        tasks.Add(SaveBlobProperty(new BlobProperty
        {
            BlobHash = blob.BlobHash,
            Name = "Length",
            Value = blob.Length.ToString()
        }));

        Task.WaitAll(tasks.ToArray());

        return handle;
    }

    /// <summary>
    /// Loads a blob by its hash.
    /// </summary>
    /// <param name="blobHash">The SHA-256 hash of the blob to load.</param>
    /// <returns>The loaded <see cref="Blob"/>, or null if not found.</returns>
    public Task<Blob?> LoadBlobAsync(string blobHash)
    {
        return Task.Run<Blob?>(() =>
        {
            BlobHandleData handle = BlobRepository.OneBlobHandleDataWhere(c => c.BlobHash == blobHash);
            if (handle == null)
            {
                return null;
            }

            List<BlobChunkData> chunkDescriptors = BlobRepository
                .BlobChunkDatasWhere(
                    c => c.BlobHash == blobHash,
                    new OrderBy<Bam.Blobs.Data.Local.Dao.BlobChunkDataColumns>(c => c.ChunkIndex, SortOrder.Ascending))
                .ToList();

            List<BlobPropertyData> properties = BlobRepository
                .BlobPropertyDatasWhere(c => c.BlobHash == blobHash)
                .ToList();

            int chunkSize = 256000;
            long length = 0;

            BlobPropertyData? chunkSizeProp = properties.FirstOrDefault(p => p.Name == "ChunkSize");
            if (chunkSizeProp?.Value != null)
            {
                int.TryParse(chunkSizeProp.Value, out chunkSize);
            }

            BlobPropertyData? lengthProp = properties.FirstOrDefault(p => p.Name == "Length");
            if (lengthProp?.Value != null)
            {
                long.TryParse(lengthProp.Value, out length);
            }

            return new StoredBlob(blobHash, chunkSize, length, chunkDescriptors, properties, ChunkStorage);
        });
    }

    private Task<IChunk> SaveDistributableChunk(BlobChunk chunk)
    {
        return Task.Run(IChunk () =>
        {
            ChunkStorage.SetChunk(chunk);
            return chunk;
        });
    }

    private Task<BlobChunkData> SaveBlobChunk(BlobChunk blobChunk)
    {
        return Task.Run(() => BlobRepository.Save(new BlobChunkData()
        {
            BlobHash = blobChunk.BlobHash,
            ChunkHash = blobChunk.ChunkHash,
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
