using Bam.Blobs.Data;
using Bam.Data;
using Bam.Data.Repositories;
using Bam.Logging;
using Bam.Net.CoreServices.Files;

namespace Bam.Blobs
{
    /// <summary>
    /// Provides file chunking, storage, and retrieval services using a repository and chunk storage backends.
    /// </summary>
    public class FileService : IFileService
    {
        protected FileService() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="repository">The data repository for persisting file and chunk metadata.</param>
        /// <param name="dataProvider">The data directory provider for file system chunk storage.</param>
        /// <param name="logger">An optional logger instance.</param>
        public FileService(IRepository repository, IDataDirectoryProvider dataProvider, ILogger logger = null)
        {
            Repository = repository;
            Repository.AddTypes(new Type[]
            {
                typeof(BlobDescriptorData),
                typeof(BlobChunkAssociationData),
                typeof(ChunkData)
            });
            DataProvider = dataProvider;
            Logger = logger ?? Log.Default;
            FileSystemChunkStorage = new FileSystemChunkStorage(DataProvider, Logger);
            RepositoryChunkStorage = new RepositoryChunkStorage(Repository, DataProvider, Logger);
            ChunkStorage = new CompositeChunkStorage();
            ChunkStorage.AddStorage(FileSystemChunkStorage);
            ChunkStorage.AddStorage(RepositoryChunkStorage);

            ChunkDataBatchSize = 10;
            ChunkLength = 256000;
            ChunkDirectory = DataProvider.GetChunksDirectory().FullName;
            SetChunkDataDescriptorRetriever();
        }

        /// <summary>
        /// Gets or sets the data repository used for persisting file and chunk metadata.
        /// </summary>
        public IRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets the logger instance.
        /// </summary>
        public ILogger? Logger { get; set; }

        /// <summary>
        /// Adds an additional chunk storage backend.
        /// </summary>
        /// <param name="storage">The chunk storage to add.</param>
        /// <returns>This <see cref="FileService"/> instance for method chaining.</returns>
        public FileService AddStorage(IChunkStorage storage)
        {
            ChunkStorage.AddStorage(storage);
            return this;
        }

        /// <summary>
        /// Removes all secondary chunk storage backends.
        /// </summary>
        /// <returns>This <see cref="FileService"/> instance for method chaining.</returns>
        public FileService ClearStorage()
        {
            ChunkStorage.ClearStorage();
            return this;
        }

        /// <summary>
        /// A delegate used to retrieve chunk data descriptors for a given file hash, starting from a specified index, up to a specified batch size.
        /// </summary>
        public Func<string, int, int, IEnumerable<BlobChunkAssociationData>> ChunkDataDescriptorRetriever;

        /// <summary>
        /// Gets the file system directory path where chunks are stored.
        /// </summary>
        public string ChunkDirectory { get; internal set; }

        /// <summary>
        /// Gets the number of chunk data descriptors to retrieve per batch.
        /// </summary>
        public int ChunkDataBatchSize { get; internal set; }

        /// <summary>
        /// Gets the length, in bytes, used when chunking files.
        /// </summary>
        public int ChunkLength { get; internal set; }
        protected CompositeChunkStorage ChunkStorage { get; set; }
        protected FileSystemChunkStorage FileSystemChunkStorage { get; set; }
        protected RepositoryChunkStorage RepositoryChunkStorage { get; set; }
        /// <summary>
        /// Saves a chunk data descriptor, creating it if it does not already exist.
        /// </summary>
        /// <param name="xref">The blob-chunk association data to save.</param>
        /// <returns>The existing or newly saved <see cref="BlobChunkAssociationData"/>.</returns>
        public virtual BlobChunkAssociationData SaveChunkDataDescriptor(BlobChunkAssociationData xref)
        {
            BlobChunkAssociationData? existingXref = Repository
                               .Query<BlobChunkAssociationData>(
                                       Filter.Where(nameof(BlobChunkAssociationData.BlobHash)) == xref.BlobHash &&
                                       Filter.Where(nameof(BlobChunkAssociationData.ChunkHash)) == xref.ChunkHash &&
                                       Filter.Where(nameof(BlobChunkAssociationData.ChunkIndex)) == xref.ChunkIndex)
                                   .FirstOrDefault();
            if (existingXref == null)
            {
                existingXref = Repository.Save(xref);
            }
            return existingXref;
        }

        /// <summary>
        /// Save the ChunkData
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public virtual void SaveChunkData(ChunkData chunk)
        {
            Args.ThrowIf(!chunk.ChunkHash.Equals(chunk.Data.FromBase64().Sha256()), "Hash validation failed");
            ChunkStorage.SetChunk(chunk.ToChunk());
        }

        /// <summary>
        /// Retrieves the specified chunkdata from the 
        /// file system if its there otherwise from the repository
        /// </summary>
        /// <param name="chunkHash"></param>
        /// <returns></returns>
        public virtual ChunkData GetChunkData(string chunkHash)
        {
            return ChunkData.FromChunk(ChunkStorage.GetChunk(chunkHash));
        }

        /// <summary>
        /// Gets a file descriptor by file hash or file name.
        /// </summary>
        /// <param name="fileHashOrName">The SHA-256 hash or file name to look up.</param>
        /// <returns>The matching <see cref="ChunkedFileDescriptor"/>, or null if not found.</returns>
        public virtual ChunkedFileDescriptor GetFileDescriptor(string fileHashOrName)
        {
            ChunkedFileDescriptor descriptor = GetFileDescriptorByFileHash(fileHashOrName);
            if (descriptor == null)
            {
                descriptor = GetFileDescriptorsByFileName(fileHashOrName).SingleOrDefault();
            }
            return descriptor;
        }

        /// <summary>
        /// Gets a file descriptor by its SHA-256 file hash.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <returns>The matching <see cref="ChunkedFileDescriptor"/>, or null if not found.</returns>
        public virtual ChunkedFileDescriptor GetFileDescriptorByFileHash(string fileHash)
        {
            return Repository.Query<ChunkedFileDescriptor>(Filter.Where(nameof(ChunkedFileDescriptor.FileHash)) == fileHash).SingleOrDefault();
        }

        /// <summary>
        /// Gets file descriptors matching the specified file name and optionally a directory.
        /// </summary>
        /// <param name="fileName">The file name to search for.</param>
        /// <param name="originalDirectory">An optional directory to filter by.</param>
        /// <returns>An enumerable of matching <see cref="ChunkedFileDescriptor"/> instances.</returns>
        public virtual IEnumerable<ChunkedFileDescriptor> GetFileDescriptorsByFileName(string fileName, string originalDirectory = null)
        {
            QueryFilter filter = Filter.Where(nameof(ChunkedFileDescriptor.FileName)) == fileName;
            if (!string.IsNullOrEmpty(originalDirectory))
            {
                filter = filter && Filter.Where(nameof(ChunkedFileDescriptor.OriginalDirectory)) == originalDirectory;
            }
            return Repository.Query<ChunkedFileDescriptor>(filter);
        }

        /// <summary>
        /// Get a set of FileChunks for the specified fileHash
        /// starting from the specified fromIndex returning
        /// the specified batchSize number of FileChunks
        /// </summary>
        /// <param name="fileHash">The hash of the file to get chunks for</param>
        /// <param name="fromIndex">The exclusive chunk index to start from</param>
        /// <param name="batchSize">The number of chunks to return</param>
        /// <returns></returns>
        public virtual List<FileChunk> GetFileChunks(string fileHash, int fromIndex, int batchSize)
        {
            List<FileChunk> chunks = YieldFileChunks(fileHash, fromIndex, batchSize).ToList();
            chunks.Sort((x, y) => x.ChunkIndex.CompareTo(y.ChunkIndex));
            return chunks;
        }

        [Local]
        public IEnumerable<FileChunk> YieldFileChunks(string fileHash, int fromIndex, int batchSize)
        {
            IEnumerable<ChunkDataDescriptor> xrefs = ChunkDataDescriptorRetriever(fileHash, fromIndex, batchSize);
            foreach (ChunkDataDescriptor xref in xrefs)
            {
                ChunkData data = GetChunkData(xref.ChunkHash);
                yield return new FileChunk
                {
                    FileHash = xref.FileHash,
                    ChunkHash = xref.ChunkHash,
                    ChunkIndex = xref.ChunkIndex,
                    StreamIndex = xref.StreamIndex,
                    ChunkLength = data.ChunkLength,
                    Data = data.Data
                };
            }
        }

        /// <summary>
        /// Lists all file descriptors stored in the specified directory.
        /// </summary>
        /// <param name="originalDirectory">The original directory path to list files from.</param>
        /// <returns>An enumerable of <see cref="ChunkedFileDescriptor"/> instances in the specified directory.</returns>
        public IEnumerable<ChunkedFileDescriptor> ListFiles(string originalDirectory)
        {
            return Repository.Query<ChunkedFileDescriptor>(Filter.Where(nameof(ChunkedFileDescriptor.OriginalDirectory)) == originalDirectory);
        }

        [Exclude]
        public ChunkedFileWriter GetFileWriter(string fileHash)
        {
            return ChunkedFileWriter.FromFileHash(this, fileHash, Logger);
        }

        /// <summary>
        /// Retrieves chunk data from the repository by hash and asynchronously caches it to the file system.
        /// </summary>
        /// <param name="chunkHash">The SHA-256 hash of the chunk to retrieve.</param>
        /// <returns>The matching <see cref="ChunkData"/>.</returns>
        public virtual ChunkData GetChunkDataFromRepository(string chunkHash)
        {
            ChunkData result = Repository.Query<ChunkData>(Filter.Where(nameof(ChunkData.ChunkHash)) == chunkHash).FirstOrDefault();
            Args.ThrowIf(result == null, "Chunk not found with hash of ({0})", chunkHash);         
            Task.Run(() => Path.Combine(ChunkDirectory, chunkHash).SafeWriteFile(result.Data, true));
            return result;
        }

        [Local]
        /// <summary>
        /// Retrieves chunk data directly from the file system chunk storage.
        /// </summary>
        /// <param name="chunkHash">The SHA-256 hash of the chunk to retrieve.</param>
        /// <returns>The matching <see cref="ChunkData"/>.</returns>
        public ChunkData GetChunkDataFromFileSystem(string chunkHash)
        {
            return ChunkData.FromChunk(FileSystemChunkStorage.GetChunk(chunkHash));
        }

        /// <summary>
        /// Save the specified file into chunk storage
        /// </summary>
        /// <param name="file"></param>
        /// <param name="chunkLength"></param>
        /// <returns></returns>
        [Local]
        public ChunkedFileDescriptor StoreFileChunks(FileInfo file, string description = null)
        {
            ChunkedFileReader chunked = new ChunkedFileReader(file, ChunkLength);
            ChunkedFileDescriptor chunkedFileDescriptor = chunked.ToChunkedFileDescriptor(description);
            SaveFileDescriptor(chunkedFileDescriptor);

            foreach (ChunkedFileDataDescriptor chunkFileDataDescriptor in chunked.ToChunkedFileDataDescriptor())
            {
                ChunkData chunk = chunkFileDataDescriptor.ChunkData;
                SaveChunkData(chunk);
                ChunkDataDescriptor xref = chunkFileDataDescriptor.ChunkDataDescriptor;
                SaveChunkDataDescriptor(xref);
            }
            return chunkedFileDescriptor;
        }

        [Local]
        /// <summary>
        /// Restores a file from chunk storage to the local file system using its file descriptor.
        /// </summary>
        /// <param name="fileDescriptor">The file descriptor containing metadata about the file to restore.</param>
        /// <param name="localPath">The local path to write the file to. Defaults to the original path from the descriptor.</param>
        /// <returns>A <see cref="FileInfo"/> for the restored file.</returns>
        public FileInfo RestoreFile(ChunkedFileDescriptor fileDescriptor, string localPath = null)
        {
            return RestoreFile(fileDescriptor.FileHash, localPath ?? Path.Combine(fileDescriptor.OriginalDirectory, fileDescriptor.FileName));
        }

        [Local]
        /// <summary>
        /// Restores a file from chunk storage to the specified local path.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash identifying the file in chunk storage.</param>
        /// <param name="localPath">The local file path to write the restored file to.</param>
        /// <param name="overwrite">Whether to overwrite an existing file at the local path. Defaults to true.</param>
        /// <returns>A <see cref="FileInfo"/> for the restored file.</returns>
        public FileInfo RestoreFile(string fileHash, string localPath, bool overwrite = true)
        {
            HandleExistingFile(localPath, overwrite);
            FileInfo file = new FileInfo(localPath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            using (FileStream fs = new FileStream(localPath, FileMode.Create))
            {
                WriteFileHashToStream(fileHash, fs);
            }
            file.Refresh();
            return file;
        }

        [Local]
        /// <summary>
        /// Writes file data to the specified directory, using the file's original name. Skips writing if the file already exists and its hash matches.
        /// </summary>
        /// <param name="fileNameOrHash">The file name or SHA-256 hash identifying the file.</param>
        /// <param name="directoryPath">The directory to write the file to.</param>
        /// <returns>A <see cref="FileInfo"/> for the written file.</returns>
        public FileInfo WriteFileDataToDirectory(string fileNameOrHash, string directoryPath)
        {
            ChunkedFileDescriptor fileDescriptor = GetFileDescriptor(fileNameOrHash);
            Args.ThrowIfNull(fileDescriptor, "fileDescriptor");
            string localPath = Path.Combine(directoryPath, fileDescriptor.FileName);
            FileInfo file = new FileInfo(localPath);
            if (File.Exists(localPath) && !fileDescriptor.FileHash.Equals(file.Sha256()))
            {
                File.Delete(localPath);
            }
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            if (!File.Exists(localPath))
            {
                using(FileStream fs = new FileStream(localPath, FileMode.Create))
                {
                    WriteFileHashToStream(fileDescriptor.FileHash, fs);
                }
            }
            file.Refresh();
            return file;
        }

        [Local]
        /// <summary>
        /// Writes file data to the specified stream by looking up the file by name or hash.
        /// </summary>
        /// <param name="fileNameOrHash">The file name or SHA-256 hash identifying the file.</param>
        /// <param name="stream">The stream to write the file data to.</param>
        public void WriteFileToStream(string fileNameOrHash, Stream stream)
        {
            ChunkedFileDescriptor fileDescriptor = GetFileDescriptor(fileNameOrHash);
            Args.ThrowIfNull(fileDescriptor, "fileDescriptor");
            WriteFileHashToStream(fileDescriptor.FileHash, stream);
        }

        [Local]
        /// <summary>
        /// Writes all chunks for the specified file hash to a stream in order.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file whose chunks to write.</param>
        /// <param name="fs">The stream to write the chunk data to.</param>
        public void WriteFileHashToStream(string fileHash, Stream fs)
        {
            List<FileChunk> chunks = GetFileChunks(fileHash, -1);
            while (chunks.Count > 0)
            {
                foreach (FileChunk chunk in chunks)
                {
                    fs.Write(chunk.ByteData, 0, chunk.ByteData.Length);
                }
                chunks = GetFileChunks(fileHash, (int)(chunks[chunks.Count - 1].ChunkIndex));
            }
        }

        /// <summary>
        /// Gets file chunks for the specified file hash starting after the given index, using the default batch size.
        /// </summary>
        /// <param name="fileHash">The SHA-256 hash of the file.</param>
        /// <param name="fromIndex">The exclusive chunk index to start from.</param>
        /// <returns>A sorted list of <see cref="FileChunk"/> instances.</returns>
        public virtual List<FileChunk> GetFileChunks(string fileHash, int fromIndex)
        {
            return GetFileChunks(fileHash, fromIndex, ChunkDataBatchSize);
        }

        protected IDataDirectoryProvider DataProvider { get; }

        private static void HandleExistingFile(string localPath, bool overwrite)
        {
            if (File.Exists(localPath))
            {
                if (overwrite)
                {
                    File.Delete(localPath);
                }
                else
                {
                    throw new InvalidOperationException($"File already exists: {localPath}");
                }
            }
        }

        private void SetChunkDataDescriptorRetriever()
        {
            if (Repository is DaoRepository daoRepo)
            {
                ChunkDataDescriptorRetriever = (fileHash, fromIndex, chunkDataBatchSize) => daoRepo.Top<ChunkDataDescriptor>(chunkDataBatchSize,
                    Filter.Where(nameof(ChunkDataDescriptor.FileHash)) == fileHash &&
                    Filter.Where(nameof(ChunkDataDescriptor.ChunkIndex)) > fromIndex);
            }
            else
            {
                Logger.Warning("{0}::FileService.Repository is not a DaoRepository but is a ({1}), good luck with that!", nameof(SetChunkDataDescriptorRetriever), Repository.GetType().Name);
                ChunkDataDescriptorRetriever = (fileHash, fromIndex, chunkDataBatchSize) => Repository.Query<ChunkDataDescriptor>(
                        Filter.Where(nameof(ChunkDataDescriptor.FileHash)) == fileHash &&
                        Filter.Where(nameof(ChunkDataDescriptor.ChunkIndex)) > fromIndex)
                    .Take(chunkDataBatchSize);
            }
        }
    }
}
