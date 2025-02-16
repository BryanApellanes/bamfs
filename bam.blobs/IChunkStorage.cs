namespace Bam.Net.CoreServices.Files
{
    public interface IChunkStorage
    {
        IChunk? GetChunk(string hash);
        void SetChunk(IChunk chunk);
    }
}
