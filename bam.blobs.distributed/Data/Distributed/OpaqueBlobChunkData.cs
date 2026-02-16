
using Bam.Data.Repositories;

namespace Bam.Blobs.Data.Distributed;

/// <summary>
/// Represents an opaque (encrypted) blob chunk stored in distributed storage. The chunk hash is HMAC-protected and the data is encrypted.
/// </summary>
public class OpaqueBlobChunkData: RepoData
{
    /// <summary>
    /// Gets or sets the HMAC of the original chunk hash, used for lookup without revealing the plaintext hash.
    /// </summary>
    public string ChunkHashHmac { get; set; }

    /// <summary>
    /// Gets or sets the encrypted (ciphertext) chunk data.
    /// </summary>
    public string DataCipher { get; set; }
}