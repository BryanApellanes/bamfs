

//using Bam.Net.Services.Data;

namespace Bam.Blobs
{
    /// <summary>
    /// Represents a local file and its chunks.
    /// </summary>
    public class FileBlob : Blob, IFileBlobHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileBlob"/> class from a file path.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        public FileBlob(string filePath) : this(new FileInfo(filePath))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileBlob"/> class from a <see cref="FileInfo"/>.
        /// </summary>
        /// <param name="file">The file to represent as a blob.</param>
        /// <param name="chunkSize">The size, in bytes, of each chunk. Defaults to 256000.</param>
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

        /// <summary>
        /// Gets the blob properties including the file name and directory.
        /// </summary>
        /// <returns>An enumerable of <see cref="BlobProperty"/> instances for FileName and Directory.</returns>
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

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string? FileName { get; }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        public string? Directory { get; }

        /// <summary>
        /// Gets the total number of chunks, including a partial tail chunk if the file size is not evenly divisible by chunk size.
        /// </summary>
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

        /// <summary>
        /// Gets the <see cref="BlobChunk"/> at the specified index by reading the corresponding segment from the file.
        /// </summary>
        /// <param name="chunkIndex">The zero-based index of the chunk to retrieve.</param>
        /// <returns>The <see cref="BlobChunk"/> at the specified index.</returns>
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

        /// <summary>
        /// Reads the chunk data at the specified index from the file.
        /// </summary>
        /// <param name="chunkIndex">The zero-based index of the chunk to read.</param>
        /// <param name="streamIndex">When this method returns, contains the byte offset in the file where the chunk begins.</param>
        /// <returns>A byte array containing the chunk data.</returns>
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
