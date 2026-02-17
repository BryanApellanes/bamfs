namespace Bam.Chunking
{
    /// <summary>
    /// ChunkStorage composed of a single Primary IChunkStorage
    /// and zero or more Secondary IChunkStorage providers
    /// </summary>
    public class CompositeChunkStorage : IChunkStorage
    {
        public CompositeChunkStorage()
        {
            Primary = new FileSystemChunkStorage();
            Secondary = new HashSet<IChunkStorage>();
        }

        /// <summary>
        /// Gets or sets the primary chunk storage, checked first when retrieving chunks.
        /// </summary>
        public IChunkStorage Primary { get; set; }

        /// <summary>
        /// Gets the set of secondary chunk storage providers.
        /// </summary>
        public HashSet<IChunkStorage> Secondary { get; }

        /// <summary>
        /// Adds a secondary chunk storage backend.
        /// </summary>
        /// <param name="storage">The chunk storage to add.</param>
        public void AddStorage(IChunkStorage storage)
        {
            Secondary.Add(storage);
        }

        /// <summary>
        /// Removes all secondary chunk storage backends.
        /// </summary>
        public void ClearStorage()
        {
            Secondary.Clear();
        }

        /// <summary>
        /// Retrieves a chunk by hash, checking the primary storage first, then each secondary storage.
        /// If found in secondary storage, the chunk is asynchronously cached to the primary storage.
        /// </summary>
        /// <param name="hash">The hash of the chunk to retrieve.</param>
        /// <returns>The chunk if found; otherwise, null.</returns>
        public IChunk GetChunk(string hash)
        {
            IChunk chunk = Primary.GetChunk(hash);
            if(chunk != null)
            {
                return chunk;
            }
            foreach (IChunkStorage storage in Secondary)
            {
                chunk = storage.GetChunk(hash);
                if (chunk != null)
                {
                    Task.Run(() => Primary.SetChunk(chunk));
                    return chunk;
                }
            }
            return null!;
        }

        /// <summary>
        /// Stores a chunk in the primary storage and asynchronously replicates it to all secondary storage backends.
        /// </summary>
        /// <param name="chunk">The chunk to store.</param>
        public void SetChunk(IChunk chunk)
        {
            Primary.SetChunk(chunk);
            foreach(IChunkStorage storage in Secondary)
            {
                Task.Run(() => storage.SetChunk(chunk));
            }
        }
    }
}
