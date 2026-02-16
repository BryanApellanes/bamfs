# bam.blobs.distributed

Distributed, HMAC-opaque blob storage with encrypted chunk and metadata persistence.

## Overview

bam.blobs.distributed extends the bam.blobs content-addressable storage system with data types and storage designed for distributed, privacy-preserving scenarios. Instead of storing plaintext hashes and data, this project uses HMAC-derived keys and ciphertext columns so that the storage server cannot inspect blob contents or correlate blob identities without the client's HMAC key.

The project defines "opaque" counterparts to every local data type: `OpaqueBlobHandleData` stores an HMAC of the author handle and blob hash; `OpaqueBlobChunkData` and `OpaqueChunkData` store HMAC-derived chunk hashes and encrypted data; and `OpaqueBlobPropertyData` stores HMAC-keyed property names with encrypted values. A generated `DistributedBlobDataRepository` provides typed DAO query methods for these opaque types, following the same generated-repository pattern used in bam.blobs.

The `OpaqueChunkStorage` class implements `IChunkStorage` using HMAC-SHA256 to derive opaque keys for chunk lookup and storage. This is intended to allow chunk data to be stored on untrusted distributed nodes where the server sees only opaque identifiers and ciphertext.

## Key Classes

| Class | Description |
|---|---|
| `OpaqueChunkStorage` | `IChunkStorage` implementation that uses HMAC-SHA256 to derive opaque chunk identifiers and is intended to store/retrieve encrypted chunk data. |
| `OpaqueBlobHandleData` | POCO for a distributed blob handle with `AuthorHandleHmac` and `BlobHashHmac` fields. |
| `OpaqueBlobChunkData` | POCO for a distributed blob-chunk relationship with `ChunkHashHmac` and `DataCipher` fields. |
| `OpaqueBlobPropertyData` | POCO for blob metadata with `BlobHashHmac`, `NameHmac`, and `ValueCipher` fields. |
| `OpaqueChunkData` | POCO for standalone chunk data with `ChunkHashHmac` and `DataCipher` fields. |
| `DistributedBlobDataRepository` | Generated DAO repository with typed query, count, set-one, batch, and top methods for all four opaque data types. Has a partial class extension point. |

## Dependencies

### Project References

- bam.base
- bam.data
- bam.blobs

### Target Framework

- net10.0

## Usage Examples

### Creating opaque data entries

```csharp
using Bam.Blobs.Data.Distributed;
using Bam.Blobs.Data.Distributed.Dao.Repository;

var repo = new DistributedBlobDataRepository();

// Save an opaque blob handle
repo.Save(new OpaqueBlobHandleData
{
    AuthorHandleHmac = hmacOfAuthorHandle,
    BlobHashHmac = hmacOfBlobHash
});

// Query opaque chunk data
var chunk = repo.OneOpaqueChunkDataWhere(c => c.ChunkHashHmac == hmacOfChunkHash);
```

### Using OpaqueChunkStorage (partial implementation)

```csharp
using Bam.Files;

// Requires an IHmacKeyProvider to generate HMAC keys
OpaqueChunkStorage storage = new OpaqueChunkStorage(hmacKeyProvider);

// GetChunk computes HMAC of the hash to derive the opaque key
// (currently throws NotImplementedException after computing the HMAC)
IChunk chunk = storage.GetChunk(chunkHash);
```

## Known Gaps / Not Yet Implemented

- **`OpaqueChunkStorage.GetChunk`** -- Throws `NotImplementedException`. The method computes the HMAC of the provided hash but does not yet perform the actual lookup or decryption of the chunk data.
- **`OpaqueChunkStorage.SetChunk`** -- The method body is commented out and effectively a no-op. The commented code shows the intended approach of computing an HMAC key and storing it, but the implementation is incomplete.
- **`DistributedBlobDataRepository` partial class** -- The hand-written partial class extension (`DistributedBlobDataRepository.cs` in the project root) is empty, containing no additional behavior beyond the generated code.
- **No encryption/decryption logic** -- While the data types have `DataCipher` and `ValueCipher` columns, there is no implemented code that performs the actual AES/RSA encryption or decryption of chunk data before storage.
