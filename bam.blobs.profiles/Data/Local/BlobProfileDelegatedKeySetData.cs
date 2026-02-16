using Bam.Data.Repositories;
using Bam.Encryption;
using Bam.Encryption.Data;

namespace Bam.Blobs.Profiles.Data.Local;

/// <summary>
/// Represents a delegated key set for a blob profile, containing RSA and AES keys used for encryption and signing operations.
/// </summary>
public class BlobProfileDelegatedKeySetData : KeyedAuditRepoData
{
    /// <summary>
    /// Initializes a new instance with the specified RSA key length without generating keys.
    /// </summary>
    /// <param name="keyLength">The RSA key length to use. Defaults to 2048 bits.</param>
    public BlobProfileDelegatedKeySetData(RsaKeyLength keyLength = RsaKeyLength._2048): this(keyLength, false)
    {
    }

    /// <summary>
    /// Initializes a new instance with the specified RSA key length, optionally generating keys immediately.
    /// </summary>
    /// <param name="keyLength">The RSA key length to use.</param>
    /// <param name="initialize">If true, generates RSA and AES keys immediately.</param>
    public BlobProfileDelegatedKeySetData(RsaKeyLength keyLength, bool initialize)
    {
        this.RsaKeyLength = keyLength;
        
        if (initialize)
        {
            Initialize();
        }
    }

    protected void Initialize()
    {
        KeySet keySet = new KeySet(this.RsaKeyLength, true, true);
        RsaPrivateKey = keySet.RsaKey;
        RsaPublicKey = RsaPrivateKey.ToKeyPair().PublicKeyToPem();
        Handle = RsaPublicKey.Sha256();
        AesKey = keySet.AesKey;
        AesIv = keySet.AesIV;
    }
    
    /// <summary>
    /// Gets or sets the handle, derived from the SHA-256 hash of the RSA public key.
    /// </summary>
    [CompositeKey]
    public string Handle { get; set; }

    /// <summary>
    /// Gets or sets the RSA key length used for this key set.
    /// </summary>
    public RsaKeyLength RsaKeyLength { get; set; }

    /// <summary>
    /// Gets or sets the RSA private key in PEM format.
    /// </summary>
    public string RsaPrivateKey { get; set; }

    /// <summary>
    /// Gets or sets the RSA public key in PEM format.
    /// </summary>
    public string RsaPublicKey { get; set; }

    /// <summary>
    /// Gets or sets the AES symmetric encryption key.
    /// </summary>
    public string AesKey { get; set; }

    /// <summary>
    /// Gets or sets the AES initialization vector.
    /// </summary>
    public string AesIv { get; set; }
}