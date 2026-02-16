namespace Bam.Blobs
{
    /// <summary>
    /// Extends <see cref="IBlobHandle"/> with file-specific metadata such as file name and directory.
    /// </summary>
    public interface IFileBlobHandle : IBlobHandle
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        string? FileName { get; }

        /// <summary>
        /// Gets the directory path where the file is located.
        /// </summary>
        string? Directory { get; }
    }
}