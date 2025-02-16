using Bam.Net.CoreServices.Files;

namespace Bam.Blobs
{
    public class Chunk : IChunk
    {
        private string? _chunkHash;
        public string ChunkHash
        {
            get
            {
                if (string.IsNullOrEmpty(_chunkHash))
                {
                    _chunkHash = Data?.Sha256();
                }
                return _chunkHash ?? string.Empty;
            }
            set => _chunkHash = value;
        }

        public virtual byte[] Data { get; set; }
    }

}
