using Bam.Data.Repositories;

namespace Bam.Blobs.Data.Local
{
    /// <summary>
    /// Represents the persistent data for a blob handle, identified by its SHA-256 hash.
    /// </summary>
    [Serializable]
    public class BlobHandleData: RepoData
    {
        /// <summary>
        /// Gets or sets the SHA-256 hash that uniquely identifies this blob.
        /// </summary>
        public string BlobHash { get; set; }

        /// <summary>
        /// Determines equality based on the <see cref="BlobHash"/> value.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the other object is a <see cref="BlobHandleData"/> with the same BlobHash; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BlobHandleData o)
            {
                return o.BlobHash.Equals(BlobHash);
            }
            return false;
        }
    }
}
