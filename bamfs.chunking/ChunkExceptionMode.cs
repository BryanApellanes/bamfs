namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Specifies how chunk operation errors are handled.
    /// </summary>
    public enum ChunkExceptionMode
    {
        /// <summary>
        /// Throw a <see cref="ChunkException"/> when an error occurs.
        /// </summary>
        Throw,

        /// <summary>
        /// Raise events instead of throwing exceptions when an error occurs.
        /// </summary>
        EmitEvents
    }
}
