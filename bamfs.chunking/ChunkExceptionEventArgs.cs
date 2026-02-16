namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// Event arguments for chunk exception events, containing the hash of the chunk involved.
    /// </summary>
    public class ChunkExceptionEventArgs: EventArgs
    {
        /// <summary>
        /// Gets or sets the hash of the chunk that caused the exception.
        /// </summary>
        public string Hash { get; set; }
    }
}
