namespace Bam.Chunking
{
    public interface IChunkStorage
    {
        IChunk GetChunk(string hash);
        void SetChunk(IChunk chunk);
    }
}
