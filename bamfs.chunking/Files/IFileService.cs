using Bam.Chunking;

namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// When implemented provides a mechanism for saving and restoring chunked files.
    /// </summary>
    public interface IFileService
    {
        int ChunkDataBatchSize { get; }
        string ChunkDirectory { get; }
        int ChunkLength { get; }

        ChunkData GetChunkData(string chunkHash);
        ChunkData GetChunkDataFromFileSystem(string chunkHash);
        ChunkData GetChunkDataFromRepository(string chunkHash);
        List<FileChunk> GetFileChunks(string fileHash, int fromIndex);
        List<FileChunk> GetFileChunks(string fileHash, int fromIndex, int batchSize);
        ChunkedDataDescriptor GetFileDescriptor(string fileHashOrName);
        ChunkedDataDescriptor GetFileDescriptorByFileHash(string fileHash);
        IEnumerable<ChunkedDataDescriptor> GetFileDescriptorsByFileName(string fileName, string? originalDirectory = null);
        ChunkedFileWriter GetFileWriter(string fileHash);
        FileInfo RestoreFile(ChunkedDataDescriptor dataDescriptor, string? localPath = null);
        FileInfo RestoreFile(string fileHash, string localPath, bool overwrite = true);
        void SaveChunkData(ChunkData chunk);
        ChunkedDataDataRelationship SaveChunkDataDescriptor(ChunkedDataDataRelationship xref);
        ChunkedDataDescriptor SaveFileDescriptor(ChunkedDataDescriptor dataDescriptor);
        ChunkedDataDescriptor StoreFileChunks(FileInfo file, string? description = null);
        void WriteFileHashToStream(string fileHash, Stream fs);
        FileInfo WriteFileDataToDirectory(string fileNameOrHash, string directoryPath);
        void WriteFileToStream(string fileNameOrHash, Stream stream);
        IEnumerable<FileChunk> YieldFileChunks(string fileHash, int fromIndex, int batchSize);
        IEnumerable<ChunkedDataDescriptor> ListFiles(string rootPath = ".");
    }
}