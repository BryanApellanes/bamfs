using Bam.Data.Repositories;

namespace Bam.Blobs.Data.Distributed;

/// <summary>
/// Represents an opaque (encrypted) blob property stored in distributed storage. The blob hash and property name are HMAC-protected, and the value is encrypted.
/// </summary>
public class OpaqueBlobPropertyData : RepoData
{
    /// <summary>
    /// Gets or sets the HMAC of the blob hash this property belongs to.
    /// </summary>
    public string BlobHashHmac { get; set; }

    /// <summary>
    /// Gets or sets the HMAC of the property name.
    /// </summary>
    public string NameHmac { get; set; }

    /// <summary>
    /// Gets or sets the encrypted (ciphertext) property value, or null if not set.
    /// </summary>
    public string? ValueCipher { get; set; }
}