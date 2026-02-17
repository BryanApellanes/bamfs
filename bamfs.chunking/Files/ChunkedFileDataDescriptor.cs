namespace Bam.Chunking
{
    /// <summary>
    /// An intermediate class used to describe the relationships
    /// between ChunkDataDescriptor and ChunkData
    /// </summary>
    public class ChunkedFileDataDescriptor
    {
        /// <summary>
        /// Gets or sets the relationship descriptor linking the chunked data to its parent file.
        /// </summary>
        public ChunkedDataDataRelationship ChunkedDataDataRelationship { get; set; } = null!;

        /// <summary>
        /// Gets or sets the chunk data containing the hash and base64-encoded content.
        /// </summary>
        public ChunkData ChunkData { get; set; } = null!;
    }
}
