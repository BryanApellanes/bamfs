using Bam.Data;
using Bam.Logging;
using Bam.Net.CoreServices.Files;

namespace Bam.Blobs
{
    /// <summary>
    /// An IChunkStorage implementation that stores chunks in the file system.
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.Files.IChunkStorage" />
    public class FileSystemChunkStorage: IChunkStorage
    {
        /// <summary>
        /// Initializes a new instance using the default data source provider and logger.
        /// </summary>
        public FileSystemChunkStorage()
        {
            DataProvider = DataSourceProvider.Current;
            Logger = Log.Default;
        }

        /// <summary>
        /// Initializes a new instance with the specified data directory provider and optional logger.
        /// </summary>
        /// <param name="dataProvider">The data directory provider that determines where chunks are stored.</param>
        /// <param name="logger">An optional logger instance.</param>
        public FileSystemChunkStorage(IDataDirectoryProvider dataProvider, ILogger? logger = null)
        {
            DataProvider = dataProvider;
            Logger = logger ?? Log.Default;
        }

        /// <summary>
        /// Gets or sets the data directory provider used to determine the chunk storage directory.
        /// </summary>
        public IDataDirectoryProvider DataProvider { get; set; }

        /// <summary>
        /// Gets or sets the logger instance.
        /// </summary>
        public ILogger? Logger { get; set; }

        /// <summary>
        /// Stores a chunk in the file system, overwriting any existing chunk with the same hash.
        /// </summary>
        /// <param name="chunk">The chunk to store.</param>
        public void SetChunk(IChunk chunk)
        {
            SetChunk(chunk, true);
        }

        /// <summary>
        /// Retrieves a chunk from the file system by its hash. Logs a warning if not found.
        /// </summary>
        /// <param name="chunkHash">The SHA-256 hash of the chunk to retrieve.</param>
        /// <returns>The chunk if found; otherwise, null.</returns>
        public IChunk GetChunk(string chunkHash)
        {
            if (ChunkExists(chunkHash, out IChunk chunk))
            {
                return chunk;
            }
            else
            {
                Task.Run(() => Logger!.AddEntry("Chunk not found: {0}", LogEventType.Warning, chunkHash));
            }
            return null!;
        }

        protected IChunk SetChunk(IChunk chunk, bool force)
        {
            if (ChunkExists(chunk.ChunkHash, out IChunk result) && !force)
            {
                return result;
            }

            FileInfo file = new FileInfo(GetChunkFilePath(chunk.ChunkHash));
            if (!file.Directory!.Exists)
            {
                file.Directory.Create();
            }
            File.WriteAllBytes(file.FullName, chunk.Data);
            return chunk;
        }

        private bool ChunkExists(string hash, out IChunk chunk)
        {
            string filePath = GetChunkFilePath(hash);
            bool result = File.Exists(filePath);
            if (!result)
            {
                chunk = null!;
                return result;
            }

            chunk = new Chunk
            {
                ChunkHash = hash,
                Data = File.ReadAllBytes(filePath)
            };
            return result;
        }

        private string GetChunkFilePath(string hash)
        {
            return Path.Combine(GetChunkDirectoryPath(hash), "chunk");
        }

        private string GetChunkDirectoryPath(string hash)
        {
            DirectoryInfo chunksDir = DataProvider.GetChunksDirectory();
            return Path.Combine(chunksDir.FullName, Path.Combine(hash.SplitByLength(2).ToArray()));
        }        
    }
}
