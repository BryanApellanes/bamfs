namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Specifies the type of chunk operation in a client-server request.
    /// </summary>
    public enum ChunkOperation
    {
        /// <summary>
        /// An invalid or unspecified operation.
        /// </summary>
        Invalid,

        /// <summary>
        /// Retrieve a chunk from storage.
        /// </summary>
        Get,

        /// <summary>
        /// Store a chunk in storage.
        /// </summary>
        Set
    }
}
