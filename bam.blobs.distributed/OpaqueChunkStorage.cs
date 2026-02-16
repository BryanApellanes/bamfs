using System.Security.Cryptography;
using System.Text;
using Bam.Blobs;
using Bam.Blobs.Data;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Encryption;
using Bam.Net.CoreServices.Files;
using Bam.Storage;

namespace Bam.Files;

/// <summary>
/// An <see cref="IChunkStorage"/> implementation that stores and retrieves chunks using HMAC-based key derivation for opaque (privacy-preserving) storage.
/// </summary>
public class OpaqueChunkStorage : IChunkStorage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpaqueChunkStorage"/> class.
    /// </summary>
    /// <param name="hmacKeyProvider">The provider of HMAC keys used for hash obfuscation.</param>
    /// <param name="blobRepository">An optional local blob data repository. Uses a default instance if not provided.</param>
    public OpaqueChunkStorage(IHmacKeyProvider hmacKeyProvider, LocalBlobDataRepository? blobRepository = null)
    {
        this.HmacKeyProvider = hmacKeyProvider;
        this.HMAC = new HMACSHA256(hmacKeyProvider.GetNewHmacKey());
        this.BlobDataRepository = blobRepository ?? new LocalBlobDataRepository();
    }
    private HMAC HMAC { get; set; }
    private IHmacKeyProvider HmacKeyProvider { get; set; }
    
    private LocalBlobDataRepository BlobDataRepository { get; set; }
    
    /// <summary>
    /// Retrieves a chunk by computing the HMAC of its hash and looking it up in storage. Not yet implemented.
    /// </summary>
    /// <param name="hash">The SHA-256 hash of the chunk to retrieve.</param>
    /// <returns>The chunk if found; otherwise, null.</returns>
    /// <exception cref="NotImplementedException">Always thrown; this method is not yet implemented.</exception>
    public IChunk? GetChunk(string hash)
    {
        byte[] hashBytes = Encoding.ASCII.GetBytes(hash);
        string hex = HMAC.ComputeHash(hashBytes).ToHexString();
        throw new NotImplementedException();
    }

    /// <summary>
    /// Stores a chunk using HMAC-based key derivation for opaque storage. Not yet implemented.
    /// </summary>
    /// <param name="chunk">The chunk to store.</param>
    public void SetChunk(IChunk chunk)
    {
        /*OpaqueKeyValueData data = new OpaqueKeyValueData();
        byte[] hashBytes = Encoding.ASCII.GetBytes(chunk.ChunkHash);
        string hmacKey = HMAC.ComputeHash(hashBytes).ToHexString();
        
        data.Key = hmacKey;*/
        
    }
}