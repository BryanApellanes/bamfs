using Bam.Data.Repositories;

namespace Bam.Chunking
{
    [Serializable]
    public class ChunkedDataDescriptor: RepoData
    {
        public string DataHash { get; set; }
        public string? OriginalFileName { get; set; }
        public string? Description { get; set; }
        public string? OriginalDirectory { get; set; }
        public long FileLength { get; set; }
        public long ChunkCount { get; set; }
        /// <summary>
        /// The specified ChunkLength at the time
        /// of chunking
        /// </summary>
        public int ChunkLength { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is ChunkedDataDescriptor o)
            {
                return o.DataHash.Equals(DataHash);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return DataHash.GetHashCode();
        }
    }
}
