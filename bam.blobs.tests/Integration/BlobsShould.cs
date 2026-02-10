using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("Blobs should")]
public class BlobsShould : UnitTestMenuContainer
{

    [UnitTest]
    public void SaveFileProperties()
    {
        When.A<object>("placeholder test passes",
            () => new object(),
            (o) => o)
        .TheTest
        .ShouldPass(because =>
        {
            because.TheResult.IsNotNull();
        })
        .SoBeHappy()
        .UnlessItFailed();
    }
}
