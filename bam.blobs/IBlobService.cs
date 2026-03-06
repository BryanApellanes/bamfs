using Bam.Blobs.Data.Local;

namespace Bam.Blobs;

/// <summary>
/// Defines the contract for saving and loading blobs.
/// </summary>
public interface IBlobService
{
    /// <summary>
    /// Saves a blob by persisting its handle, chunks, and properties.
    /// </summary>
    /// <param name="blob">The blob to save.</param>
    /// <returns>The saved <see cref="BlobHandleData"/> containing the blob metadata.</returns>
    Task<BlobHandleData> SaveBlobAsync(Blob blob);

    /// <summary>
    /// Loads a blob by its hash.
    /// </summary>
    /// <param name="blobHash">The SHA-256 hash of the blob to load.</param>
    /// <returns>The loaded <see cref="Blob"/>, or null if not found.</returns>
    Task<Blob?> LoadBlobAsync(string blobHash);
}
