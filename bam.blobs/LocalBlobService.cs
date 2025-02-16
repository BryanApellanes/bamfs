using Bam.Blobs;
using Bam.Blobs.Data.Local;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Net.CoreServices.Files;

namespace Bam.Files;

public class LocalBlobService
{
    public LocalBlobService(FileSystemChunkStorage? fileSystemChunkStorage = null)
    {
        this.BlobRepository = new LocalBlobDataRepository();
        this.FileSystemChunkStorage = fileSystemChunkStorage ?? new FileSystemChunkStorage();
    }
    protected LocalBlobDataRepository BlobRepository { get; init; }
    protected FileSystemChunkStorage FileSystemChunkStorage { get; init; }

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
            FileSystemChunkStorage.SetChunk(chunk);
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