using Bam.Chunking;
using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("ChunkedFileReader should")]
public class ChunkedFileReaderShould : UnitTestMenuContainer
{
    [UnitTest]
    public void ReadWholeAndTailChunksExactly()
    {
        string path = Path.Combine(Path.GetTempPath(), $"chunkedfilereader-{Guid.NewGuid():N}.bin");
        try
        {
            When.A<ChunkedFileReader>("reads a whole chunk and a partial tail chunk",
                () =>
                {
                    byte[] content = new byte[25];
                    for (int i = 0; i < content.Length; i++)
                    {
                        content[i] = (byte)(i + 1);
                    }
                    File.WriteAllBytes(path, content);
                    return new ChunkedFileReader(new FileInfo(path), 10);
                },
                (reader) =>
                {
                    byte[] whole = reader.ReadChunk(1, out long wholeIndex);
                    byte[] tail = reader.ReadChunk(2, out long tailIndex);
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
        string path = Path.Combine(Path.GetTempPath(), $"chunkedfilereader-{Guid.NewGuid():N}.bin");
        try
        {
            When.A<ChunkedFileReader>("reads chunks from a file truncated after the reader was sized",
                () =>
                {
                    File.WriteAllBytes(path, new byte[25]);
                    ChunkedFileReader reader = new ChunkedFileReader(new FileInfo(path), 10);
                    File.WriteAllBytes(path, new byte[15]);
                    return reader;
                },
                (reader) => new TruncationOutcome(ThrowsEndOfStream(() => reader.ReadChunk(1, out long _)), ThrowsEndOfStream(() => reader.ReadChunk(2, out long _))))
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
