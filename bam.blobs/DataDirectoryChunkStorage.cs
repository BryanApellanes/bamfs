using Bam.Data;
using Bam.Logging;
using Bam.Net.CoreServices.Files;

namespace Bam.Blobs
{
    /// <summary>
    /// An IChunkStorage implementation that stores chunks in the file system.
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.Files.IChunkStorage" />
    public class DataDirectoryChunkStorage: IChunkStorage
    {
        public DataDirectoryChunkStorage()
        {
            DataProvider = DataSourceProvider.Current;
            Logger = Log.Default;
        }

        public DataDirectoryChunkStorage(IDataDirectoryProvider dataProvider, ILogger? logger = null)
        {
            DataProvider = dataProvider;
            Logger = logger ?? Log.Default;
        }

        public IDataDirectoryProvider DataProvider { get; set; }
        public ILogger? Logger { get; set; }
        public void SetChunk(IChunk chunk)
        {
            SetChunk(chunk, true);
        }

        public IChunk GetChunk(string chunkHash)
        {
            if (ChunkExists(chunkHash, out IChunk chunk))
            {
                return chunk;
            }
            else
            {
                Task.Run(() => Logger.AddEntry("Chunk not found: {0}", LogEventType.Warning, chunkHash));
            }
            return null;
        }

        protected IChunk SetChunk(IChunk chunk, bool force)
        {
            if (ChunkExists(chunk.ChunkHash, out IChunk result) && !force)
            {
                return result;
            }

            FileInfo file = new FileInfo(GetChunkFilePath(chunk.ChunkHash));
            if (!file.Directory.Exists)
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
                chunk = null;
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
