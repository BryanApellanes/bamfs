using Bam.Logging;
using Bam.Net.CoreServices.Files;

namespace Bam.Chunking
{
    /// <summary>
    /// Writes a chunked file from chunk storage to a local file path, reassembling chunks into the original file.
    /// </summary>
    public class ChunkedFileWriter: IChunkedFileDescriptor
    {
        internal ChunkedFileWriter()
        {

        }

        internal ChunkedFileWriter(IFileService fileService)
        {
            FileService = fileService;
        }

        /// <summary>
        /// Gets or sets the file service used for chunk retrieval.
        /// </summary>
        public IFileService FileService { get; set; }

        /// <summary>
        /// Gets or sets the total number of chunks for this file.
        /// </summary>
        public long ChunkCount { get; set; }

        /// <summary>
        /// Gets or sets the length, in bytes, of each chunk.
        /// </summary>
        public int ChunkLength { get; set; }

        /// <summary>
        /// Gets or sets the SHA-256 hash of the file.
        /// </summary>
        public string FileHash { get; set; }

        /// <summary>
        /// Gets or sets the total length, in bytes, of the file.
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// Gets or sets the original file name.
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets the original directory path of the file.
        /// </summary>
        public string OriginalDirectory { get; set; }

        /// <summary>
        /// Gets or sets the logger instance.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a <see cref="ChunkedFileWriter"/> from the specified file hash using the given file service.
        /// </summary>
        /// <param name="svc">The file service to use for file descriptor lookup and chunk retrieval.</param>
        /// <param name="fileHash">The SHA-256 hash of the file to write.</param>
        /// <param name="logger">An optional logger instance.</param>
        /// <returns>A new <see cref="ChunkedFileWriter"/> configured for the specified file.</returns>
        public static ChunkedFileWriter FromFileHash(IFileService svc, string fileHash, ILogger logger = null)
        {
            ChunkedDataDescriptor descriptor = svc.GetFileDescriptor(fileHash);
            ChunkedFileWriter writer = new ChunkedFileWriter(svc)
            {
                FileHash = fileHash,
                ChunkCount = descriptor.ChunkCount,
                ChunkLength = descriptor.ChunkLength,
                FileLength = descriptor.FileLength,
                OriginalFileName = descriptor.OriginalFileName,
                OriginalDirectory = descriptor.OriginalDirectory, 
                Logger = logger
            };
            return writer;
        }
        
        /// <summary>
        /// Asynchronously writes the file to the specified local path by restoring it from chunk storage.
        /// </summary>
        /// <param name="localPath">The local file path to write to.</param>
        /// <returns>A task representing the asynchronous write operation.</returns>
        public Task Write(string localPath)
        {
            return Task.Run(() => FileService.RestoreFile(FileHash, localPath));
        }
    }
}
