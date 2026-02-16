using Bam.Data.Repositories;

namespace Bam.Chunking
{
    /// <summary>
    /// Describes a chunked data set, storing metadata about the original file and its chunking parameters.
    /// </summary>
    [Serializable]
    public class ChunkedDataDescriptor: RepoData
    {
        /// <summary>
        /// Gets or sets the SHA-256 hash of the original data.
        /// </summary>
        public string DataHash { get; set; }

        /// <summary>
        /// Gets or sets the original file name, or null if not applicable.
        /// </summary>
        public string? OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets a description of the chunked data, or null if not set.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the original directory path, or null if not applicable.
        /// </summary>
        public string? OriginalDirectory { get; set; }

        /// <summary>
        /// Gets or sets the total length, in bytes, of the original file.
        /// </summary>
        public long FileLength { get; set; }

        /// <summary>
        /// Gets or sets the total number of chunks.
        /// </summary>
        public long ChunkCount { get; set; }
        /// <summary>
        /// The specified ChunkLength at the time
        /// of chunking
        /// </summary>
        public int ChunkLength { get; set; }
        /// <summary>
        /// Determines equality based on the <see cref="DataHash"/> value.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the other object is a <see cref="ChunkedDataDescriptor"/> with the same DataHash; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is ChunkedDataDescriptor o)
            {
                return o.DataHash.Equals(DataHash);
            }
            return false;
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return DataHash.GetHashCode();
        }
    }
}
