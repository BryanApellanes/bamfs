using System.Collections.Generic;
using System.IO;
using Bam.Blobs;
using Bam.Blobs.Data;

namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// When implemented provides a mechanism for saving and restoring
    /// files of any kind.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Gets the number of chunk data descriptors to retrieve per batch.
        /// </summary>
        int ChunkDataBatchSize { get; }

        /// <summary>
        /// Gets the file system directory path where chunks are stored.
        /// </summary>
        string ChunkDirectory { get; }

        /// <summary>
        /// Gets the length, in bytes, used when chunking files.
        /// </summary>
        int ChunkLength { get; }

        /// <summary>
        /// Retrieves chunk data by its hash from the composite chunk storage.
        /// </summary>
        /// <param name="chunkHash">The SHA-256 hash of the chunk.</param>
        /// <returns>The matching <see cref="ChunkData"/>.</returns>
        ChunkData GetChunkData(string chunkHash);

        /// <summary>
        /// Retrieves chunk data directly from the file system.
        /// </summary>
        /// <param name="chunkHash">The SHA-256 hash of the chunk.</param>
        /// <returns>The matching <see cref="ChunkData"/>.</returns>
        ChunkData GetChunkDataFromFileSystem(string chunkHash);

        /// <summary>
        /// Retrieves chunk data from the repository.
        /// </summary>
        /// <param name="chunkHash">The SHA-256 hash of the chunk.</param>
        /// <returns>The matching <see cref="ChunkData"/>.</returns>
        ChunkData GetChunkDataFromRepository(string chunkHash);

        /// <summary>
        /// Gets file chunks starting after the specified index using the default batch size.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <param name="fromIndex">The exclusive chunk index to start from.</param>
        /// <returns>A sorted list of <see cref="FileChunk"/> instances.</returns>
        List<FileChunk> GetFileChunks(string fileHash, int fromIndex);

        /// <summary>
        /// Gets file chunks starting after the specified index up to the specified batch size.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <param name="fromIndex">The exclusive chunk index to start from.</param>
        /// <param name="batchSize">The number of chunks to return.</param>
        /// <returns>A sorted list of <see cref="FileChunk"/> instances.</returns>
        List<FileChunk> GetFileChunks(string fileHash, int fromIndex, int batchSize);

        /// <summary>
        /// Gets a file descriptor by file hash or file name.
        /// </summary>
        /// <param name="fileHashOrName">The SHA-256 hash or file name to look up.</param>
        /// <returns>The matching file descriptor, or null if not found.</returns>
        BlobDescriptorData GetFileDescriptor(string fileHashOrName);

        /// <summary>
        /// Gets a file descriptor by its SHA-256 file hash.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <returns>The matching file descriptor, or null if not found.</returns>
        BlobDescriptorData GetFileDescriptorByFileHash(string fileHash);

        /// <summary>
        /// Gets file descriptors matching the specified file name and optionally a directory.
        /// </summary>
        /// <param name="fileName">The file name to search for.</param>
        /// <param name="originalDirectory">An optional directory to filter by.</param>
        /// <returns>An enumerable of matching file descriptors.</returns>
        IEnumerable<BlobDescriptorData> GetFileDescriptorsByFileName(string fileName, string originalDirectory = null);

        /// <summary>
        /// Gets a file writer for the specified file hash.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <returns>A <see cref="FileWriter"/> for writing the file data.</returns>
        FileWriter GetFileWriter(string fileHash);

        /// <summary>
        /// Restores a file from chunk storage to the local file system using its file descriptor.
        /// </summary>
        /// <param name="fileDescriptor">The file descriptor containing metadata about the file.</param>
        /// <param name="localPath">The local path to write to. Defaults to the original path.</param>
        /// <returns>A <see cref="FileInfo"/> for the restored file.</returns>
        FileInfo RestoreFile(BlobDescriptorData fileDescriptor, string localPath = null);

        /// <summary>
        /// Restores a file from chunk storage to the specified local path.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <param name="localPath">The local file path to write to.</param>
        /// <param name="overwrite">Whether to overwrite an existing file. Defaults to true.</param>
        /// <returns>A <see cref="FileInfo"/> for the restored file.</returns>
        FileInfo RestoreFile(string fileHash, string localPath, bool overwrite = true);

        /// <summary>
        /// Saves chunk data to the composite chunk storage after validating its hash.
        /// </summary>
        /// <param name="chunk">The chunk data to save.</param>
        void SaveChunkData(ChunkData chunk);

        /// <summary>
        /// Saves a chunk data descriptor, creating it if it does not already exist.
        /// </summary>
        /// <param name="xref">The blob-chunk association data to save.</param>
        /// <returns>The existing or newly saved association data.</returns>
        BlobChunkAssociationData SaveChunkDataDescriptor(BlobChunkAssociationData xref);

        /// <summary>
        /// Saves a file descriptor, creating it if it does not already exist.
        /// </summary>
        /// <param name="fileDescriptor">The file descriptor to save.</param>
        /// <returns>The existing or newly saved file descriptor.</returns>
        BlobDescriptorData SaveFileDescriptor(BlobDescriptorData fileDescriptor);

        /// <summary>
        /// Chunks and stores a file in chunk storage.
        /// </summary>
        /// <param name="file">The file to store.</param>
        /// <param name="description">An optional description of the file.</param>
        /// <returns>The file descriptor for the stored file.</returns>
        BlobDescriptorData StoreFileChunks(FileInfo file, string description = null);

        /// <summary>
        /// Writes all chunks for the specified file hash to a stream in order.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <param name="fs">The stream to write to.</param>
        void WriteFileHashToStream(string fileHash, Stream fs);

        /// <summary>
        /// Writes file data to the specified directory using the file's original name.
        /// </summary>
        /// <param name="fileNameOrHash">The file name or SHA-256 hash identifying the file.</param>
        /// <param name="directoryPath">The directory to write the file to.</param>
        /// <returns>A <see cref="FileInfo"/> for the written file.</returns>
        FileInfo WriteFileDataToDirectory(string fileNameOrHash, string directoryPath);

        /// <summary>
        /// Writes file data to the specified stream.
        /// </summary>
        /// <param name="fileNameOrHash">The file name or SHA-256 hash identifying the file.</param>
        /// <param name="stream">The stream to write to.</param>
        void WriteFileToStream(string fileNameOrHash, Stream stream);

        /// <summary>
        /// Lazily yields file chunks for the specified file hash starting after the given index.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <param name="fromIndex">The exclusive chunk index to start from.</param>
        /// <param name="batchSize">The number of chunks to yield.</param>
        /// <returns>An enumerable of <see cref="FileChunk"/> instances.</returns>
        IEnumerable<FileChunk> YieldFileChunks(string fileHash, int fromIndex, int batchSize);

        /// <summary>
        /// Lists all file descriptors in the specified root path.
        /// </summary>
        /// <param name="rootPath">The root directory path to list files from. Defaults to ".".</param>
        /// <returns>An enumerable of file descriptors.</returns>
        IEnumerable<BlobDescriptorData> ListFiles(string rootPath = ".");
    }
}