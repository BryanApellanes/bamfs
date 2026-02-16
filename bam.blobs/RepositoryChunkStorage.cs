using Bam.Net.CoreServices.Files;
using Bam.Blobs.Data;
using Bam.Data;
using Bam.Data.Repositories;
using Bam.Generators;
using Bam.Logging;

namespace Bam.Blobs
{
    /// <summary>
    /// An IChunkStorage implementation that stores chunk data in an IRepository.
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.Files.IChunkStorage" />
    public class RepositoryChunkStorage: IChunkStorage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryChunkStorage"/> class.
        /// </summary>
        public RepositoryChunkStorage()
        {
        }

        /// <summary>
        /// Initializes a new instance with the specified data directory provider and optional logger.
        /// </summary>
        /// <param name="dataProvider">The data directory provider.</param>
        /// <param name="logger">An optional logger instance.</param>
        public RepositoryChunkStorage(IDataDirectoryProvider dataProvider, ILogger logger = null)
        {
            DataProvider = dataProvider;
            Repository = new DefaultDaoRepository();
            Repository.AddType<ChunkData>();
        }

        /// <summary>
        /// Initializes a new instance with the specified repository, data directory provider, and optional logger.
        /// </summary>
        /// <param name="repository">The data repository to store chunks in.</param>
        /// <param name="dataSettings">The data directory provider.</param>
        /// <param name="logger">An optional logger instance.</param>
        public RepositoryChunkStorage(IRepository repository, IDataDirectoryProvider dataSettings, ILogger logger = null):this(dataSettings, logger)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets or sets the data directory provider.
        /// </summary>
        public IDataDirectoryProvider DataProvider { get; set; }

        /// <summary>
        /// Gets or sets the data repository used for chunk persistence.
        /// </summary>
        public IRepository Repository { get; set; }

        /// <summary>
        /// Retrieves a chunk from the repository by its hash.
        /// </summary>
        /// <param name="hash">The SHA-256 hash of the chunk to retrieve.</param>
        /// <returns>The chunk if found; otherwise, null.</returns>
        public IChunk? GetChunk(string hash)
        {
            ChunkData? data = Repository.Query(nameof(ChunkData.ChunkHash), hash).CopyAs<ChunkData>().FirstOrDefault();
            return data?.ToChunk();
        }

        /// <summary>
        /// Stores a chunk in the repository asynchronously after validating its hash. Skips storage if the chunk already exists with matching data.
        /// </summary>
        /// <param name="chunk">The chunk to store.</param>
        public void SetChunk(IChunk chunk)
        {
            Args.ThrowIf(!chunk.ChunkHash.Equals(chunk.Data.Sha256()), "Hash validation failed");
            Task.Run(() =>
            {
                ChunkData? existingChunk = Repository.Query<ChunkData>(Filter.Where(nameof(ChunkData.ChunkHash)) == chunk.ChunkHash).FirstOrDefault();
                if (existingChunk == null || !existingChunk.Data.FromBase64().Sha256().Equals(chunk.Data.Sha256()))
                {
                    Repository.Save(ChunkData.FromChunk(chunk));
                }
            });         
        }
    }
}
