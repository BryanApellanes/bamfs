

//using Bam.Net.Services.Data;

namespace Bam.Chunking
{
    /// <summary>
    /// Represents a local file and its chunks.  Should
    /// not be persisted use ChunkedFileDescriptor instead
    /// </summary>
    public class ChunkedFileReader : IChunkedFileDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkedFileReader"/> class for the specified file.
        /// </summary>
        /// <param name="file">The file to read as chunks.</param>
        /// <param name="chunkLength">The length, in bytes, of each chunk. Defaults to 256000.</param>
        public ChunkedFileReader(FileInfo file, int chunkLength = 256000)
        {
            Args.ThrowIfNull(file, "file");
            Args.ThrowIf(!file.Exists, "File {0} doesn't exist", file.FullName);
            File = file;
            OriginalFileName = file.Name;
            OriginalDirectory = file.Directory?.FullName;
            ChunkLength = chunkLength;
            FileHash = file.Sha256();
            FileLength = file.Length;
            WholeChunkCount = Math.Floor((decimal)file.Length / (decimal)chunkLength);
            TailLength = FileLength % ChunkLength;
            PartialTail = TailLength > 0;
        }

        /// <summary>
        /// Gets or sets the original file name.
        /// </summary>
        public string? OriginalFileName { get; set; }
        /// <summary>
        ///  The original directory the file was in
        ///  when chunked
        /// </summary>
        public string? OriginalDirectory { get; }
        /// <summary>
        /// Gets the length, in bytes, of each chunk.
        /// </summary>
        public int ChunkLength { get; }

        /// <summary>
        /// Gets the SHA-256 hash of the file.
        /// </summary>
        public string FileHash { get; }

        /// <summary>
        /// Gets the total length, in bytes, of the file.
        /// </summary>
        public long FileLength { get; }

        /// <summary>
        /// Gets the total number of chunks, including a partial tail chunk if the file size is not evenly divisible by chunk length.
        /// </summary>
        public long ChunkCount
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
        /// Gets the <see cref="FileChunk"/> at the specified index by reading the corresponding segment from the file.
        /// </summary>
        /// <param name="chunkIndex">The zero-based index of the chunk to retrieve.</param>
        /// <returns>The <see cref="FileChunk"/> at the specified index.</returns>
        public FileChunk this[int chunkIndex]
        {
            get
            {
                FileChunk chunk = new FileChunk()
                {
                    ChunkIndex = chunkIndex,
                    FileHash = FileHash,
                    ByteData = ReadChunk(chunkIndex, out long streamIndex),
                    StreamIndex = streamIndex
                };
                chunk.ChunkLength = chunk.ByteData.Length;
                return chunk;
            }
        }

        /// <summary>
        /// Reads the chunk data at the specified index from the file.
        /// </summary>
        /// <param name="chunkIndex">The zero-based index of the chunk to read.</param>
        /// <param name="streamIndex">When this method returns, contains the byte offset in the file where the chunk begins.</param>
        /// <returns>A byte array containing the chunk data.</returns>
        public byte[] ReadChunk(int chunkIndex, out long streamIndex)
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

        /// <summary>
        /// Creates a <see cref="ChunkedDataDescriptor"/> from this file reader's metadata.
        /// </summary>
        /// <param name="description">An optional description of the file.</param>
        /// <returns>A <see cref="ChunkedDataDescriptor"/> representing the file's chunk metadata.</returns>
        public ChunkedDataDescriptor ToChunkedFileDescriptor(string? description = null)
        {
            return new ChunkedDataDescriptor
            {
                DataHash = FileHash,
                OriginalFileName = OriginalFileName,
                Description = description,
                OriginalDirectory = OriginalDirectory,
                FileLength = FileLength,
                ChunkLength = ChunkLength,
                ChunkCount = ChunkCount
            };
        }

        /// <summary>
        /// Yields all chunks of the file as <see cref="ChunkedFileDataDescriptor"/> instances, each containing chunk data and its relationship to the file.
        /// </summary>
        /// <returns>An enumerable of <see cref="ChunkedFileDataDescriptor"/> for each chunk.</returns>
        public IEnumerable<ChunkedFileDataDescriptor> ToChunkedFileDataDescriptor()
        {
            for(int i = 0; i < ChunkCount; i++)
            {
                FileChunk chunk = this[i];
                yield return new ChunkedFileDataDescriptor
                {
                    ChunkData = chunk.ToChunkData(),
                    ChunkedDataDataRelationship = chunk.ToChunkDataDescriptor()
                };
            }
        }

        protected internal decimal WholeChunkCount { get; set; }
        protected internal long TailLength { get; set; }
        protected internal bool PartialTail { get; set; }
        protected long TailStreamIndex => (long)WholeChunkCount * ChunkLength;

        protected FileInfo File { get; set; }
        private byte[] ReadWholeFileSystemChunk(int chunkIndex, out long streamIndex)
        {
            streamIndex = chunkIndex * ChunkLength;
            byte[] buffer = new byte[ChunkLength];
            using (FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(streamIndex, SeekOrigin.Begin);
                fs.Read(buffer, 0, ChunkLength);
            }

            return buffer;
        }

        private byte[] ReadTailFileSystemChunk(out long streamIndex)
        {
            streamIndex = TailStreamIndex;
            byte[] buffer = new byte[TailLength];
            using (FileStream fs = new FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(TailStreamIndex, SeekOrigin.Begin);
                fs.Read(buffer, 0, (int)TailLength);
            }
            return buffer;            
        }
    }
}
