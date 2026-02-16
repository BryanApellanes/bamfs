# bam.blobs.tests

Integration and unit tests for the bam.blobs blob storage library.

## Overview

bam.blobs.tests is a console application that uses the bam.test menu-driven test runner (via `BamConsoleContext.StaticMain`) to exercise the bam.blobs library. Tests are organized as `[UnitTestMenu]`-attributed classes with `[UnitTest]` methods that use the `When.A<T>()` fluent assertion API.

The test suite covers three areas: basic blob placeholder assertions (`BlobsShould`), DAO repository save and query operations (`BlobDaoRepositoryShould`), and file-blob chunking correctness (`FileBlobShould`). The project includes a set of test fixture files of various sizes (50 bytes through 5 MB) that are copied to the output directory for use in chunking tests.

The `TestFileBlobHandle` helper class extends `FileBlob` to expose the `protected internal` `TailSize` property for test assertions, enabling verification that files evenly divisible by the chunk size have no tail and that files exceeding the chunk size produce the correct tail length and chunk count.

## Key Classes

| Class | Description |
|---|---|
| `BlobsShould` | Unit test menu with a placeholder test (`SaveFileProperties`) that verifies the test framework is functional. |
| `BlobDaoRepositoryShould` | Unit test menu that exercises `LocalBlobDataRepository` -- saves `BlobHandleData` instances and retrieves them by hash using `OneBlobHandleDataWhere`. |
| `FileBlobShould` | Unit test menu that verifies `FileBlob` chunking: tests that a file exactly equal to the chunk size has no tail (1 chunk), and a file exceeding the chunk size has the expected tail size (2 chunks). |
| `TestFileBlobHandle` | Test helper extending `FileBlob` that exposes `TailSize` as `TailLengthAccessor` for assertion access. |

## Dependencies

### Project References

- bam.base
- bam.configuration
- bam.console
- bam.data.repositories
- bam.data
- bam.test
- bam.blobs

### Target Framework

- net10.0

### Test Fixture Files

| File | Size | Copy Behavior |
|---|---|---|
| `TestFiles/50` | 50 bytes | Always |
| `TestFiles/256` | 256 bytes | Always |
| `TestFiles/276` | 276 bytes | Always |
| `TestFiles/128K` | 128 KB | Always |
| `TestFiles/256000` | 256,000 bytes | Always |
| `TestFiles/275000` | 275,000 bytes | Always |
| `TestFiles/1Meg` | ~1 MB | PreserveNewest |
| `TestFiles/5MegFile` | ~5 MB | PreserveNewest |
| `TestFiles/1GigFile` | ~1 GB | PreserveNewest |

## Usage Examples

### Running the tests

```bash
# Run all unit tests (use --ut, not /ut in Git Bash)
dotnet run --project submodules/bamfs/bam.blobs.tests/bam.blobs.tests.csproj -- --ut
```

### Test structure example

```csharp
[UnitTestMenu("FileBlobs should")]
public class FileBlobShould : UnitTestMenuContainer
{
    [UnitTest]
    public void FileBlobShouldHaveNoTail()
    {
        When.A<TestFileBlobHandle>("has no tail when file equals chunk size",
            () => new TestFileBlobHandle(TestFilePath("256000")),
            (blobHandle) => new object[] { blobHandle.TailLengthAccessor, blobHandle.ChunkCount })
        .TheTest
        .ShouldPass(because =>
        {
            object[] results = (object[])because.Result;
            long tailSize = (long)results[0];
            long chunkCount = (long)results[1];
            because.ItsTrue("tail size equals expected", 0 == tailSize);
            because.ItsTrue("chunk count equals expected", 1 == chunkCount);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }
}
```

## Known Gaps / Not Yet Implemented

- **`BlobsShould.SaveFileProperties`** -- Currently a placeholder test that only asserts a non-null object. Does not actually test saving file properties to a blob.
- **No chunk storage tests** -- There are no tests for `DataDirectoryChunkStorage`, `FileSystemChunkStorage`, `RepositoryChunkStorage`, or `CompositeChunkStorage`.
- **No `LocalBlobService` tests** -- The `SaveBlobAsync` workflow is not covered.
- **No distributed/opaque storage tests** -- The bam.blobs.distributed types have no test coverage in this project.
