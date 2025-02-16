using Bam.Blobs.Data.Local;
using Bam.Blobs.Data.Local.Dao.Repository;
using Bam.Test;

namespace Bam.Application.Unit;

[UnitTestMenu("BlobDaoRepositoryShould")]
public class BlobDaoRepositoryShould: UnitTestMenuContainer
{
    [UnitTest]
    public void SaveDataInstances()
    {
        LocalBlobDataRepository repo = new LocalBlobDataRepository();
        if (repo == null)
        {
            Args.Throw<ArgumentNullException>("default repo was null");
        }

        string testHash = 32.RandomLetters();
        BlobHandleData data = repo.Save(new BlobHandleData()
        {
            BlobHash = testHash
        });
        
        data.ShouldNotBeNull();
        data.BlobHash.ShouldBeEqualTo(testHash);
        data.Id.ShouldBeGreaterThan(0);
    }
    
    [UnitTest]
    public void FindBlobHandleByHash()
    {
        LocalBlobDataRepository repo = new LocalBlobDataRepository();

        string testHash = 32.RandomLetters();
        BlobHandleData data = repo.Save(new BlobHandleData()
        {
            BlobHash = testHash
        });

        BlobHandleData retrieved = repo.OneBlobHandleDataWhere(c => c.BlobHash == testHash);
        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldEqual(data.Id);
        retrieved.BlobHash.ShouldEqual(testHash);
    }
}