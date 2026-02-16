# bam.blobs

Content-addressable blob storage with SHA-256-based chunking and multiple storage backends.

## Overview

bam.blobs provides a content-addressable storage system for binary large objects (blobs). Files and arbitrary byte data are split into fixed-size chunks (default 256 KB), each identified by its SHA-256 hash. This deduplication-friendly design means identical chunks are stored only once regardless of how many blobs reference them.

The project defines the core abstractions (`IChunk`, `IChunkStorage`, `IBlobHandle`) and provides several concrete storage implementations: file-system-based (`DataDirectoryChunkStorage`, `FileSystemChunkStorage`, `FsChunkStorage`), database-backed (`RepositoryChunkStorage`), and a composite strategy (`CompositeChunkStorage`) that layers primary and secondary storage providers with automatic cache promotion. A `LocalBlobService` orchestrates saving blobs by persisting both the chunk data and the blob-to-chunk relationship metadata via a generated DAO repository.

The data layer consists of POCO types (`BlobHandleData`, `BlobChunkData`, `BlobPropertyData`, `ChunkData`) and their corresponding generated DAO classes under the `Generated.Dao` directory. The generated `LocalBlobDataRepository` provides typed query methods such as `OneBlobHandleDataWhere`, `BlobChunkDatasWhere`, and batch iteration.

## Key Classes

| Class | Description |
|---|---|
| `Blob` | Abstract base class representing a blob with chunk count, chunk size, blob hash, and length. Provides an indexer to access individual `BlobChunk` instances. |
| `FileBlob` | Concrete `Blob` that reads chunks directly from a local file. Handles tail chunks when file size is not evenly divisible by chunk size. |
| `BlobChunk` | A chunk of a blob. Holds base64-encoded and raw byte data, auto-computes SHA-256 hash on set. Converts to DAO-friendly `BlobChunkData` and `ChunkData`. |
| `Chunk` | Simple `IChunk` implementation holding byte data and a lazily computed SHA-256 hash. |
| `BlobProperty` | Key-value metadata pair associated with a blob hash (e.g., FileName, Directory). |
| `LocalBlobService` | Service that saves a `Blob` by persisting its handle, all chunks (to `DataDirectoryChunkStorage`), blob-chunk relationships, and properties to `LocalBlobDataRepository`. |
| `DataDirectoryChunkStorage` | `IChunkStorage` that stores chunk bytes on the file system under a hash-derived directory tree (hash split into 2-character path segments). |
| `FileSystemChunkStorage` | Functionally identical to `DataDirectoryChunkStorage`; file-system chunk storage using `IDataDirectoryProvider`. |
| `FsChunkStorage` | `IChunkStorage` backed by `FsSlottedStorage` from bam.storage. |
| `RepositoryChunkStorage` | `IChunkStorage` that persists chunks as `ChunkData` in an `IRepository` (database-backed). Validates SHA-256 hash before storing. |
| `CompositeChunkStorage` | Composite pattern over `IChunkStorage`. Reads from primary first, falls back to secondary providers, and promotes found chunks to primary. Writes fan out to all providers. |
| `BlobWriter` | Placeholder class intended to write blobs back to files from repository storage. |
| `Handle` | Empty subclass of `DataHandle`; reserved for future use. |
| `LocalBlobDataRepository` | Generated DAO repository with typed query/save methods for `BlobHandleData`, `BlobChunkData`, and `BlobPropertyData`. |

## Dependencies

### Project References

- bam.application
- bam.base
- bam.configuration
- bam.data.repositories
- bam.data.schema
- bam.data.config
- bam.data.firebird
- bam.data.mssql
- bam.data.mysql
- bam.data.oracle
- bam.data.postgres
- bam.data
- bam.encryption
- bam.generators
- bam.logging
- bam.storage

### Target Framework

- net10.0

## Usage Examples

### Saving a file as a blob

```csharp
using Bam.Blobs;
using Bam.Files;

// Create a FileBlob from a local file (default 256 KB chunks)
FileBlob fileBlob = new FileBlob(@"C:\data\myfile.bin");

// Use LocalBlobService to persist the blob
LocalBlobService service = new LocalBlobService();
BlobHandleData handle = await service.SaveBlobAsync(fileBlob);

// The handle contains the SHA-256 hash of the entire file
Console.WriteLine($"Blob hash: {handle.BlobHash}");
```

### Reading chunks from a FileBlob

```csharp
FileBlob blob = new FileBlob(new FileInfo(@"C:\data\myfile.bin"), chunkSize: 128000);

Console.WriteLine($"Total chunks: {blob.ChunkCount}");
Console.WriteLine($"File hash: {blob.BlobHash}");

for (long i = 0; i < blob.ChunkCount; i++)
{
    BlobChunk chunk = blob[i];
    Console.WriteLine($"Chunk {i}: hash={chunk.ChunkHash}, size={chunk.Data.Length}");
}
```

### Using CompositeChunkStorage

```csharp
using Bam.Blobs;

var composite = new CompositeChunkStorage();
// Primary is DataDirectoryChunkStorage by default
// Add a database-backed secondary
composite.AddStorage(new RepositoryChunkStorage(dataProvider));

// GetChunk checks primary first, then secondary (promoting to primary on hit)
IChunk chunk = composite.GetChunk(someHash);
```

### Querying blob metadata via the generated repository

```csharp
using Bam.Blobs.Data.Local.Dao.Repository;

var repo = new LocalBlobDataRepository();

// Find a blob handle by hash
BlobHandleData handle = repo.OneBlobHandleDataWhere(c => c.BlobHash == "abc123...");

// Get all chunk descriptors for a blob
var chunks = repo.BlobChunkDatasWhere(c => c.BlobHash == "abc123...");
```

## Known Gaps / Not Yet Implemented

- **`BlobWriter.WriteBlobToFile`** -- The method body is commented out. The class is a placeholder that does not yet support writing blobs back to the file system from stored chunks.
- **`IBlobService`** -- The interface is declared but has no methods defined; it is an empty contract.
- **`Handle`** -- Empty subclass of `DataHandle` with no additional behavior.
- **Excluded source files** -- Several files are explicitly excluded from compilation in the .csproj: `FileService.cs`, `FileServiceSettings.cs`, `IFileService.cs`, `FileWriter.cs`, `OpaqueChunkStorage.cs`, `IHmacKeyProvider.cs`. These appear to be legacy or superseded types.
