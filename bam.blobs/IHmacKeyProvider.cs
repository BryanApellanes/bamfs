namespace Bam.Blobs;

/// <summary>
/// Defines the contract for providing HMAC keys used in blob data authentication.
/// </summary>
public interface IHmacKeyProvider
{
    /// <summary>
    /// Gets the HMAC key bytes.
    /// </summary>
    /// <returns>A byte array containing the HMAC key.</returns>
    byte[] GetHmacKey();
}