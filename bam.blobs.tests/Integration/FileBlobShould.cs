using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("FileBlobs should")]
public class FileBlobShould : UnitTestMenuContainer
{
    [UnitTest]
    public void FileBlobShouldHaveNoTail()
    {
        long expectedTailSize = 0;
        long expectedChunkCount = 1;

        When.A<TestFileBlobHandle>("has no tail when file equals chunk size",
            () => new TestFileBlobHandle("./TestFiles/256000"),
            (blobHandle) => new object[] { blobHandle.TailLengthAccessor, blobHandle.ChunkCount })
        .TheTest
        .ShouldPass(because =>
        {
            object[] results = (object[])because.Result;
            long tailSize = (long)results[0];
            long chunkCount = (long)results[1];
            because.ItsTrue("tail size equals expected", expectedTailSize == tailSize);
            because.ItsTrue("chunk count equals expected", expectedChunkCount == chunkCount);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void FileBlobShouldHaveTail()
    {
        long expectedTailSize = 19000;
        long expectedChunkCount = 2;

        When.A<TestFileBlobHandle>("has a tail when file exceeds chunk size",
            () => new TestFileBlobHandle("./TestFiles/275000"),
            (blobHandle) => new object[] { blobHandle.TailLengthAccessor, blobHandle.ChunkCount })
        .TheTest
        .ShouldPass(because =>
        {
            object[] results = (object[])because.Result;
            long tailSize = (long)results[0];
            long chunkCount = (long)results[1];
            because.ItsTrue("tail size equals expected", expectedTailSize == tailSize);
            because.ItsTrue("chunk count equals expected", expectedChunkCount == chunkCount);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }
}
