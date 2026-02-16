using Bam.Data.Repositories;

namespace Bam.Blobs.Data.Distributed;

/// <summary>
/// Represents an opaque (HMAC-protected) blob handle stored in distributed storage, associating an author with a blob.
/// </summary>
public class OpaqueBlobHandleData : RepoData
{
    /// <summary>
    /// Gets or sets the HMAC of the author handle, used for association without revealing the plaintext identity.
    /// </summary>
    public string AuthorHandleHmac { get; set; }

    /// <summary>
    /// Gets or sets the HMAC of the blob hash, used for lookup without revealing the plaintext hash.
    /// </summary>
    public string BlobHashHmac { get; set; }
}