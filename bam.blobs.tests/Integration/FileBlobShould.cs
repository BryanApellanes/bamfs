using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("FileBlobs should")]
public class FileBlobShould : UnitTestMenuContainer
{
    private static string TestFilePath(string fileName) =>
        Path.Combine(AppContext.BaseDirectory, "TestFiles", fileName);

    [UnitTest]
    public void FileBlobShouldHaveNoTail()
    {
        long expectedTailSize = 0;
        long expectedChunkCount = 1;

        When.A<TestFileBlobHandle>("has no tail when file equals chunk size",
            () => new TestFileBlobHandle(TestFilePath("256000")),
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
            () => new TestFileBlobHandle(TestFilePath("275000")),
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
    public void ReadWholeAndTailChunksExactly()
    {
        string path = Path.Combine(Path.GetTempPath(), $"fileblob-{Guid.NewGuid():N}.bin");
        try
        {
            When.A<Bam.Blobs.FileBlob>("reads a whole chunk and a partial tail chunk",
                () =>
                {
                    byte[] content = new byte[25];
                    for (int i = 0; i < content.Length; i++)
                    {
                        content[i] = (byte)(i + 1);
                    }
                    File.WriteAllBytes(path, content);
                    return new Bam.Blobs.FileBlob(new FileInfo(path), 10);
                },
                (blob) =>
                {
                    byte[] whole = blob.ReadChunk(1, out long wholeIndex);
                    byte[] tail = blob.ReadChunk(2, out long tailIndex);
                    return new ChunkReadOutcome(whole, wholeIndex, tail, tailIndex);
                })
            .TheTest
            .ShouldPass<ChunkReadOutcome>((because, _, outcome) =>
            {
                because.ItsTrue("the whole chunk starts at byte 10", outcome.WholeIndex == 10);
                because.ItsTrue("the whole chunk holds bytes 11 to 20", outcome.Whole.SequenceEqual(Enumerable.Range(11, 10).Select(i => (byte)i)));
                because.ItsTrue("the tail chunk starts at byte 20", outcome.TailIndex == 20);
                because.ItsTrue("the tail chunk holds bytes 21 to 25", outcome.Tail.SequenceEqual(Enumerable.Range(21, 5).Select(i => (byte)i)));
            })
            .SoBeHappy()
            .UnlessItFailed();
        }
        finally
        {
            File.Delete(path);
        }
    }

    [UnitTest]
    public void ThrowRatherThanPadWhenTheFileShrinks()
    {
        string path = Path.Combine(Path.GetTempPath(), $"fileblob-{Guid.NewGuid():N}.bin");
        try
        {
            When.A<Bam.Blobs.FileBlob>("reads chunks from a file truncated after the blob was sized",
                () =>
                {
                    File.WriteAllBytes(path, new byte[25]);
                    Bam.Blobs.FileBlob blob = new Bam.Blobs.FileBlob(new FileInfo(path), 10);
                    File.WriteAllBytes(path, new byte[15]);
                    return blob;
                },
                (blob) => new TruncationOutcome(ThrowsEndOfStream(() => blob.ReadChunk(1, out long _)), ThrowsEndOfStream(() => blob.ReadChunk(2, out long _))))
            .TheTest
            .ShouldPass<TruncationOutcome>((because, _, outcome) =>
            {
                because.ItsTrue("a short whole-chunk read throws EndOfStreamException instead of returning zero padding", outcome.WholeChunkThrew);
                because.ItsTrue("a short tail-chunk read throws EndOfStreamException instead of returning zero padding", outcome.TailChunkThrew);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }
        finally
        {
            File.Delete(path);
        }
    }

    private static bool ThrowsEndOfStream(Action read)
    {
        try
        {
            read();
            return false;
        }
        catch (EndOfStreamException)
        {
            return true;
        }
    }

    private sealed record ChunkReadOutcome(byte[] Whole, long WholeIndex, byte[] Tail, long TailIndex);

    private sealed record TruncationOutcome(bool WholeChunkThrew, bool TailChunkThrew);
}
