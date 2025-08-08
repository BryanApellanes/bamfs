using System.Security.Cryptography;
using System.Text;
using Bam.Blobs;
using Bam.Blobs.Data;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Encryption;
using Bam.Net.CoreServices.Files;
using Bam.Storage;

namespace Bam.Files;

public class OpaqueChunkStorage : IChunkStorage
{
    public OpaqueChunkStorage(IHmacKeyProvider hmacKeyProvider, LocalBlobDataRepository? blobRepository = null)
    {
        this.HmacKeyProvider = hmacKeyProvider;
        this.HMAC = new HMACSHA256(hmacKeyProvider.GetNewHmacKey());
        this.BlobDataRepository = blobRepository ?? new LocalBlobDataRepository();
    }
    private HMAC HMAC { get; set; }
    private IHmacKeyProvider HmacKeyProvider { get; set; }
    
    private LocalBlobDataRepository BlobDataRepository { get; set; }
    
    public IChunk? GetChunk(string hash)
    {
        byte[] hashBytes = Encoding.ASCII.GetBytes(hash);
        string hex = HMAC.ComputeHash(hashBytes).ToHexString();
        throw new NotImplementedException();
    }

    public void SetChunk(IChunk chunk)
    {
        /*OpaqueKeyValueData data = new OpaqueKeyValueData();
        byte[] hashBytes = Encoding.ASCII.GetBytes(chunk.ChunkHash);
        string hmacKey = HMAC.ComputeHash(hashBytes).ToHexString();
        
        data.Key = hmacKey;*/
        
    }
}