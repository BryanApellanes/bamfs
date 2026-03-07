# Refactor Plan: BlobService + LoadBlobAsync

## Context

`LocalBlobService` currently only supports saving blobs (`SaveBlobAsync`). It lacks a load/retrieval counterpart, has no interface for dependency injection or testability, and uses a name (`LocalBlobService`) that couples the abstraction to a specific storage locality. This plan adds `LoadBlobAsync`, extracts an `IBlobService` interface, and renames the class to `BlobService`.

**No changes to generated DAO code or Handlebars templates are required.** The existing `LocalBlobDataRepository` query methods (`OneBlobHandleDataWhere`, `BlobChunkDatasWhere`, `BlobPropertyDatasWhere`) provide everything needed for load operations.

---

## Bug Fix (Prerequisite)

**`SaveBlobChunk` does not persist `ChunkHash`** — the current code creates `BlobChunkData` without setting `ChunkHash`:

```csharp
// Current (broken for load):
BlobRepository.Save(new BlobChunkData()
{
    BlobHash = blobChunk.BlobHash,
    ChunkIndex = blobChunk.ChunkIndex,
    BlobIndex = blobChunk.BlobIndex,
    // ChunkHash is missing!
});
```

Without `ChunkHash`, there is no way to retrieve the actual chunk bytes from `DataDirectoryChunkStorage` during load. This must be fixed as part of this change.

---

## Changes

### 1. Create `IBlobService.cs` (NEW)
- **Path**: `bam.blobs/IBlobService.cs`
- **Namespace**: `Bam.Blobs`
- **Methods**:
  - `Task<BlobHandleData> SaveBlobAsync(Blob blob)` — existing contract
  - `Task<Blob?> LoadBlobAsync(string blobHash)` — new: loads blob by hash, returns null if not found

### 2. Create `StoredBlob.cs` (NEW)
- **Path**: `bam.blobs/StoredBlob.cs`
- **Namespace**: `Bam.Blobs`
- **Purpose**: Concrete `Blob` subclass representing a blob loaded from storage. Lazy-loads chunk data from `IChunkStorage` on access via the indexer.
- **Design**:
  - Constructor accepts: `blobHash`, `chunkSize`, `length`, `List<BlobChunkData> chunkDescriptors`, `List<BlobPropertyData> properties`, `IChunkStorage chunkStorage`
  - `ChunkCount` → `_chunkDescriptors.Count`
  - Indexer `this[long chunkIndex]` → looks up `ChunkHash` from descriptor, calls `_chunkStorage.GetChunk(chunkHash)`, returns populated `BlobChunk`
  - `GetBlobProperties()` → converts `_properties` to `BlobProperty` instances

### 3. Rename `LocalBlobService.cs` → `BlobService.cs` (MODIFY + RENAME)
- **Path**: `bam.blobs/BlobService.cs`
- **Namespace**: Move from `Bam.Files` → `Bam.Blobs` (consistent with all other blob types)
- **Implements**: `IBlobService`
- **Changes to existing code**:
  - Fix `SaveBlobChunk`: add `ChunkHash = blobChunk.ChunkHash`
  - Add saving `ChunkSize` and `Length` as `BlobPropertyData` entries in `SaveBlobAsync` (needed for reconstruction during load; reuses existing `BlobPropertyData` infrastructure — no schema changes required)
- **New method**: `LoadBlobAsync(string blobHash)`:
  1. Query `BlobHandleData` by hash via `OneBlobHandleDataWhere` — return null if not found
  2. Query all `BlobChunkData` records for the blob hash via `BlobChunkDatasWhere`, ordered by `ChunkIndex`
  3. Query all `BlobPropertyData` records for the blob hash via `BlobPropertyDatasWhere`
  4. Extract `ChunkSize` and `Length` from properties
  5. Return `new StoredBlob(...)` wrapping the loaded data

### 4. Update `README.md` (MODIFY)
- **Path**: `bam.blobs/README.md`
- Remove `IBlobService` from "Known Gaps" section (it will now be implemented)
- Update `LocalBlobService` references to `BlobService`
- Add `IBlobService`, `BlobService`, and `StoredBlob` to Key Classes table
- Add usage example for `LoadBlobAsync`
- Update namespace in examples from `Bam.Files` to `Bam.Blobs`

### 5. Delete `LocalBlobService.cs`
- Remove the old file after `BlobService.cs` is created (content migrated, not duplicated)

---

## Files Summary

| File | Action | Key Change |
|------|--------|------------|
| `bam.blobs/IBlobService.cs` | Create | Interface: `SaveBlobAsync`, `LoadBlobAsync` |
| `bam.blobs/StoredBlob.cs` | Create | Concrete `Blob` for storage-loaded blobs |
| `bam.blobs/BlobService.cs` | Create (replaces LocalBlobService) | Implements `IBlobService`, adds `LoadBlobAsync`, fixes `ChunkHash` bug |
| `bam.blobs/LocalBlobService.cs` | Delete | Replaced by `BlobService.cs` |
| `bam.blobs/README.md` | Modify | Reflect all changes |

**No generated DAO files modified. No template files modified.**

---

## Verification

1. **Build**: `dotnet build bamfs.sln` from `C:/src/repos/bamtk/submodules/bamfs/`
2. **Confirm no references to `LocalBlobService`** remain in source (only in git history)
3. **Confirm `IBlobService` is properly implemented** by `BlobService` (compiler will enforce)
4. **Review round-trip**: `SaveBlobAsync` persists `ChunkHash`, `ChunkSize`, and `Length`; `LoadBlobAsync` reads them back and constructs a `StoredBlob` with working chunk access

---

## Status

**Completed** — Implemented and pushed to `origin/bamfs` on 2026-03-07.
