using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("FileBlobs should")]
public class FileBlobShould : UnitTestMenuContainer
{
    [UnitTest]
    public void FileBlobShouldHaveNoTail()
    {
        TestFileBlobHandle blobHandle = new TestFileBlobHandle("./TestFiles/256000"); // default chunklength is 256000
        long expectedTailSize = 0;
        long expectedChunkCount = 1;
        blobHandle.TailLengthAccessor.ShouldEqual(expectedTailSize);
        blobHandle.ChunkCount.ShouldBeEqualTo(expectedChunkCount);
    }
    
    [UnitTest]
    public void FileBlobShouldHaveTail()
    {
        TestFileBlobHandle blobHandle = new TestFileBlobHandle("./TestFiles/275000"); // default chunklength is 256000
        long expectedTailSize = 19000;
        long expectedChunkCount = 2;
        blobHandle.TailLengthAccessor.ShouldEqual(expectedTailSize);
        blobHandle.ChunkCount.ShouldBeEqualTo(expectedChunkCount);
    }
}