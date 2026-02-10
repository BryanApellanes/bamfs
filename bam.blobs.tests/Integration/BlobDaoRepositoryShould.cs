using Bam.Blobs.Data.Local;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("BlobDaoRepositoryShould")]
public class BlobDaoRepositoryShould : UnitTestMenuContainer
{
    [UnitTest]
    public void SaveDataInstances()
    {
        string testHash = 32.RandomLetters();

        When.A<LocalBlobDataRepository>("saves blob handle data",
            (repo) =>
            {
                BlobHandleData data = repo.Save(new BlobHandleData()
                {
                    BlobHash = testHash
                });
                return data;
            })
        .TheTest
        .ShouldPass(because =>
        {
            because.TheResult.IsNotNull()
                .As<BlobHandleData>("BlobHash equals expected", d => testHash.Equals(d?.BlobHash))
                .As<BlobHandleData>("Id is greater than 0", d => d?.Id > 0);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void FindBlobHandleByHash()
    {
        string testHash = 32.RandomLetters();

        When.A<LocalBlobDataRepository>("finds blob handle by hash",
            (repo) =>
            {
                BlobHandleData data = repo.Save(new BlobHandleData()
                {
                    BlobHash = testHash
                });
                BlobHandleData retrieved = repo.OneBlobHandleDataWhere(c => c.BlobHash == testHash);
                return new object[] { data, retrieved };
            })
        .TheTest
        .ShouldPass(because =>
        {
            object[] results = (object[])because.Result;
            BlobHandleData saved = (BlobHandleData)results[0];
            BlobHandleData retrieved = (BlobHandleData)results[1];
            because.ItsTrue("retrieved is not null", retrieved != null);
            because.ItsTrue("Id matches saved", saved.Id == retrieved?.Id);
            because.ItsTrue("BlobHash equals expected", testHash.Equals(retrieved?.BlobHash));
        })
        .SoBeHappy()
        .UnlessItFailed();
    }
}
