using Bam.Logging;
using Bam.Net.CoreServices.Files;

namespace Bam.Chunking
{
    public class ChunkedFileWriter: IChunkedFileDescriptor
    {
        internal ChunkedFileWriter()
        {

        }

        internal ChunkedFileWriter(IFileService fileService)
        {
            FileService = fileService;
        }

        public IFileService FileService { get; set; }

        public long ChunkCount { get; set; }

        public int ChunkLength { get; set; }

        public string FileHash { get; set; }

        public long FileLength { get; set; }

        public string OriginalFileName { get; set; }

        public string OriginalDirectory { get; set; }
        public ILogger Logger { get; set; }

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
        
        public Task Write(string localPath)
        {
            return Task.Run(() => FileService.RestoreFile(FileHash, localPath));
        }
    }
}
