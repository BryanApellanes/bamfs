namespace Bam.Chunking
{
    /// <summary>
    /// An intermediate class used to describe the relationships
    /// between ChunkDataDescriptor and ChunkData
    /// </summary>
    public class ChunkedFileDataDescriptor
    {
        public ChunkedDataDataRelationship ChunkedDataDataRelationship { get; set; }
        public ChunkData ChunkData { get; set; }
    }
}
