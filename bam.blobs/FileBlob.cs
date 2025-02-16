

//using Bam.Net.Services.Data;

namespace Bam.Blobs
{
    /// <summary>
    /// Represents a local file and its chunks.
    /// </summary>
    public class FileBlob : Blob, IFileBlobHandle
    {
        public FileBlob(string filePath) : this(new FileInfo(filePath))
        {
        }

        public FileBlob(FileInfo file, int chunkSize = 256000)
        {
            Args.ThrowIfNull(file, "file");
            Args.ThrowIf(!file.Exists, "File {0} doesn't exist", file.FullName);
            File = file;
            FileName = file.Name;
            Directory = file.Directory?.FullName;
            ChunkSize = chunkSize;
            BlobHash = file.Sha256();
            Length = file.Length;
            WholeChunkCount = Math.Floor((decimal)file.Length / (decimal)chunkSize);
            TailSize = Length % ChunkSize;
            PartialTail = TailSize > 0;
        }

        public override IEnumerable<BlobProperty> GetBlobProperties()
        {
            yield return new BlobProperty()
            {
                BlobHash = BlobHash,
                Name = "FileName",
                Value = FileName,
            };
            yield return new BlobProperty()
            {
                BlobHash = BlobHash,
                Name = "Directory",
                Value = Directory
            };
        }

        public string? FileName { get; }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        public string? Directory { get; }

        public override long ChunkCount
        {
            get
            {
                if (PartialTail)
                {
                    return (long)WholeChunkCount + 1;
                }
                return (long)WholeChunkCount;
            }
        }

        public override BlobChunk this[long chunkIndex]
        {
            get
            {
                BlobChunk chunk = new BlobChunk()
                {
                    ChunkIndex = chunkIndex,
                    BlobHash = BlobHash,
                    Data = ReadChunk(chunkIndex, out long streamIndex),
                    BlobIndex = streamIndex
                };
                return chunk;
            }
        }

        public byte[] ReadChunk(long chunkIndex, out long streamIndex)
        {
            Args.ThrowIf<ArgumentOutOfRangeException>(chunkIndex < 0 || chunkIndex > (ChunkCount - 1), "ChunkIndex out of range: {0}", chunkIndex);
            if (PartialTail && chunkIndex == ChunkCount - 1)
            {
                return ReadTailFileSystemChunk(out streamIndex);
            }
            else
            {
                return ReadWholeFileSystemChunk(chunkIndex, out streamIndex);
            }
        }

        protected internal decimal WholeChunkCount { get; set; }
        protected internal long TailSize { get; set; }
        protected internal bool PartialTail { get; set; }
        protected long TailStreamIndex => (long)WholeChunkCount * ChunkSize;

        protected FileInfo File { get; set; }
        private byte[] ReadWholeFileSystemChunk(long chunkIndex, out long streamIndex)
        {
            streamIndex = chunkIndex * ChunkSize;
            byte[] buffer = new byte[ChunkSize];
            using (FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(streamIndex, SeekOrigin.Begin);
                fs.Read(buffer, 0, ChunkSize);
            }

            return buffer;
        }

        private byte[] ReadTailFileSystemChunk(out long streamIndex)
        {
            streamIndex = TailStreamIndex;
            byte[] buffer = new byte[TailSize];
            using (FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(TailStreamIndex, SeekOrigin.Begin);
                fs.Read(buffer, 0, (int)TailSize);
            }
            return buffer;            
        }
    }
}
