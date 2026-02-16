# bam.blobs.profiles

Profile-based key management data types for encrypted blob storage.

## Overview

bam.blobs.profiles defines the data model for associating blob storage profiles with cryptographic key sets. A "profile" is an identity anchor (identified by a handle) that owns one or more delegated key sets. Each key set contains RSA and AES cryptographic material used for encrypting and decrypting blob data in the distributed storage layer.

The project provides three local POCO data types and one distributed (opaque) data type. `BlobProfileHandleData` represents a named profile identity. `BlobProfileDelegatedKeySetData` encapsulates a complete cryptographic key set (RSA key pair + AES key/IV) that can be initialized on construction. `BlobProfileHandleDelegatedKeySetAssociationData` is a join entity that maps profiles to their delegated key sets. On the distributed side, `OpaqueBlobProfileHandleData` is a placeholder for the opaque (HMAC-protected) form of a profile handle.

This project is in an early stage. The data types define the schema and key generation logic, but there are no service classes, repository generation, or workflows that consume these types yet.

## Key Classes

| Class | Description |
|---|---|
| `BlobProfileHandleData` | Local POCO representing a blob storage profile. Extends `KeyedAuditRepoData` with a `[CompositeKey]` `Handle` property. |
| `BlobProfileDelegatedKeySetData` | Local POCO for a complete cryptographic key set (RSA private/public keys, AES key/IV). Extends `KeyedAuditRepoData`. Constructor can auto-initialize all keys from a specified `RsaKeyLength`. The handle is derived as the SHA-256 of the RSA public key PEM. |
| `BlobProfileHandleDelegatedKeySetAssociationData` | Local POCO join entity linking a `ProfileHandle` to a `KeySetHandle`. Extends `KeyedAuditRepoData`. |
| `OpaqueBlobProfileHandleData` | Distributed POCO placeholder for an opaque profile handle. Currently an empty class. |

## Dependencies

### Project References

- bam.data.repositories
- bam.data
- bam.encryption
- bam.protocol.data
- bam.protocol

### Target Framework

- net10.0

## Usage Examples

### Creating a profile with a delegated key set

```csharp
using Bam.Blobs.Profiles.Data.Local;
using Bam.Encryption;

// Create a new key set with RSA-2048, auto-initialized
var keySet = new BlobProfileDelegatedKeySetData(RsaKeyLength._2048, initialize: true);
// keySet.Handle is the SHA-256 of the RSA public key PEM
// keySet.RsaPrivateKey, keySet.RsaPublicKey, keySet.AesKey, keySet.AesIv are populated

// Create a profile handle
var profile = new BlobProfileHandleData
{
    Handle = "my-storage-profile"
};

// Associate the profile with the key set
var association = new BlobProfileHandleDelegatedKeySetAssociationData
{
    ProfileHandle = profile.Handle,
    KeySetHandle = keySet.Handle
};
```

## Known Gaps / Not Yet Implemented

- **`OpaqueBlobProfileHandleData`** -- Empty class with no properties or behavior.
- **No generated DAO repository** -- Unlike bam.blobs and bam.blobs.distributed, this project has no `Generated.Dao` directory and no generated repository class. The POCO types are defined but there is no data access layer to persist or query them.
- **No service layer** -- There are no service classes or workflows that create profiles, manage key sets, or use the key material for blob encryption/decryption.
- **Excluded source file** -- `Data\Local\BlobProfileDelegatedKeySetData.cs` is explicitly excluded from compilation in the .csproj, meaning the key set data type is not currently built. Only `BlobProfileHandleData`, `BlobProfileHandleDelegatedKeySetAssociationData`, and `OpaqueBlobProfileHandleData` are compiled.
