# bamfs.chunking

File chunking library with client-server streaming, composite storage, and file service orchestration.

## Overview

bamfs.chunking provides the foundational file chunking infrastructure for splitting files into content-addressable chunks, storing them across multiple backends, and reassembling them on demand. It is the predecessor/companion to bam.blobs, operating in the `Bam.Chunking` and `Bam.Net.Services.Chunking` namespaces.

The library defines a complete chunking pipeline: `ChunkedFileReader` splits a local file into `FileChunk` instances (default 256 KB), each identified by its SHA-256 hash. `FileService` orchestrates the full lifecycle -- storing file descriptors, chunk data, and chunk-to-file relationship records via `IRepository`, with chunk bytes persisted through `CompositeChunkStorage` (a layered strategy that combines `FileSystemChunkStorage` and `RepositoryChunkStorage`). Files can be restored from storage to local disk or written to streams. `ChunkedFileWriter` provides the inverse operation, reconstructing a file from its hash using the file service.

The project also includes a client-server streaming layer: `ChunkServer` accepts `ChunkRequest` messages (Get/Set operations) and delegates to an `IChunkStorage` backend, while `ChunkClient` sends requests over a streaming connection and handles errors via configurable exception modes (throw or emit events).

## Key Classes

| Class | Description |
|---|---|
| `ChunkedFileReader` | Reads a local file and provides indexed access to `FileChunk` instances. Computes file hash, chunk count, and handles partial tail chunks. |
| `ChunkedFileWriter` | Restores a file from chunk storage given its hash. Created via `FromFileHash` factory method using an `IFileService`. |
| `FileChunk` | A chunk of a file with `FileHash`, `ChunkHash`, `ChunkIndex`, `StreamIndex`, base64 data, and byte data. Auto-computes SHA-256 hash. Implements `IChunkable`. |
| `Chunk` | Simple `IChunk` implementation with `Hash` and `Data` properties. |
| `FileService` | Full-lifecycle file chunking service. Stores file descriptors, chunk data, and chunk-to-file cross-references. Supports restore to file, stream writing, and batch chunk retrieval. Requires HMAC key authorization. |
| `FileServiceSettings` | Configuration POCO for `FileService`: chunk directory, batch size, chunk length, async timeout. |
| `FileSystemChunkStorage` | `IChunkStorage` that persists chunks as files in a hash-derived directory tree under the data directory. |
| `CompositeChunkStorage` | Layers a primary `IChunkStorage` (file system) with secondary providers. Reads check primary first; cache-promotes from secondary on miss. Writes fan out to all providers. |
| `ChunkServer` | Streaming server that processes `ChunkRequest` messages, dispatching Get/Set operations to an `IChunkStorage` backend. |
| `ChunkClient` | Streaming client implementing `IChunkStorage`. Sends `ChunkRequest` messages to a `ChunkServer`. Supports throw or event-emit error handling modes. |
| `ChunkRequest` | Request message containing a `Chunk`, a `Hash` string, and a `ChunkOperation` (Get/Set/Invalid). |
| `ChunkResponse` | Response message with `Success` flag, optional `Message`, and optional `IChunk`. Includes `Validate()` for hash verification. |
| `ChunkOperation` | Enum: `Invalid`, `Get`, `Set`. |
| `ChunkException` | Exception wrapping a failed `ChunkResponse`. |
| `ChunkExceptionMode` | Enum: `Throw`, `EmitEvents`. Controls `ChunkClient` error handling behavior. |
| `ChunkedDataDescriptor` | Persisted metadata for a chunked file: data hash, original filename, directory, file length, chunk count, chunk length. |
| `ChunkedDataDataRelationship` | Persisted cross-reference between a file hash and a chunk hash, with chunk index and stream index. |
| `ChunkData` | Persisted chunk data: SHA-256 hash, chunk length, base64-encoded data. Implements `IChunkable`. |
| `ChunkedFileDataDescriptor` | Intermediate object pairing a `ChunkedDataDataRelationship` with its `ChunkData` during file storage operations. |

### Interfaces

| Interface | Description |
|---|---|
| `IChunk` | Contract for a chunk: `Hash` and `Data`. |
| `IChunkable` | Marks types that can produce an `IChunk` via `ToChunk()`. |
| `IChunkStorage` | Get/Set chunk storage contract. |
| `IChunkedFileDescriptor` | File descriptor contract: chunk count, chunk length, file hash, file length, original name/directory. |
| `IFileService` | Complete file service contract: store, retrieve, restore, list, and stream chunked files. |

## Dependencies

### Project References

- bam.base
- bam.data.repositories
- bam.data

### Target Framework

- net10.0

## Usage Examples

### Chunking and storing a file

```csharp
using Bam.Chunking;
using Bam.Net.CoreServices;

// Create a file service with a repository
FileService fileService = new FileService(repository);

// Store a file -- splits into chunks, saves descriptors and data
FileInfo file = new FileInfo(@"C:\data\document.pdf");
ChunkedDataDescriptor descriptor = fileService.StoreFileChunks(file, "My PDF document");

Console.WriteLine($"File hash: {descriptor.DataHash}");
Console.WriteLine($"Chunks: {descriptor.ChunkCount}");
```

### Restoring a file from chunk storage

```csharp
// Restore by hash to a local path
FileInfo restored = fileService.RestoreFile(descriptor.DataHash, @"C:\restored\document.pdf");

// Or use ChunkedFileWriter
ChunkedFileWriter writer = fileService.GetFileWriter(descriptor.DataHash);
await writer.Write(@"C:\restored\document.pdf");
```

### Reading file chunks directly

```csharp
using Bam.Chunking;

ChunkedFileReader reader = new ChunkedFileReader(new FileInfo(@"C:\data\largefile.bin"));

Console.WriteLine($"File: {reader.OriginalFileName}");
Console.WriteLine($"Hash: {reader.FileHash}");
Console.WriteLine($"Chunks: {reader.ChunkCount}");

for (int i = 0; i < reader.ChunkCount; i++)
{
    FileChunk chunk = reader[i];
    Console.WriteLine($"  Chunk {i}: hash={chunk.ChunkHash}, length={chunk.ChunkLength}");
}
```

### Using the chunk client/server

```csharp
using Bam.Net.Services.Chunking;

// Server side
var server = new ChunkServer(new FileSystemChunkStorage());
// server.Start(...) -- start listening

// Client side
var client = new ChunkClient("localhost", 8080);
client.ExceptionMode = ChunkExceptionMode.EmitEvents;
client.GetChunkException += (s, e) => Console.WriteLine("Get failed");

// Store a chunk
client.SetChunk(someChunk);

// Retrieve a chunk
IChunk retrieved = client.GetChunk(someHash);
```

## Known Gaps / Not Yet Implemented

- **Excluded source files** -- Several files are excluded from compilation: `Files/FileService.cs`, `Files/FileServiceSettings.cs`, `ChunkClient.cs`, `ChunkRequest.cs`, `ChunkResponse.cs`, `ChunkServer.cs`, `ChunkException.cs`. These contain the client-server streaming infrastructure and the `FileService` orchestrator, but are not part of the current build. This means only the core chunking types (`ChunkedFileReader`, `FileChunk`, `ChunkData`, storage implementations) are currently compiled.
- **`RepositoryChunkStorage` not in this project** -- The `FileService` references `RepositoryChunkStorage` but this class lives in bam.blobs, not bamfs.chunking. The excluded `FileService.cs` would need this dependency resolved if re-enabled.
- **No async file I/O** -- `ChunkedFileReader` uses synchronous `FileStream.Read` for chunk reading. Large file operations could benefit from async I/O.
