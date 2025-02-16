namespace Bam.Chunking
{
    public interface IChunk
    {
        string Hash { get; set; }
        byte[] Data { get; set; }
    }
}
