namespace Bam.Net.CoreServices.Files
{
    public interface IChunk
    {
        string ChunkHash { get; }
        byte[] Data { get; set; }
    }
}
