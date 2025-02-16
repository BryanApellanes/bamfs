using Bam.Chunking;

namespace Bam.Net.Services.Chunking
{
    public class Chunk : IChunk
    {
        public string Hash { get; set; }
        public byte[] Data { get; set; }

        public IChunk ToChunk()
        {
            return this;
        }
    }
}
