using System.Data;
using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobChunkDataCollection: DaoCollection<BlobChunkDataColumns, BlobChunkData>
    { 
		public BlobChunkDataCollection(){}
		public BlobChunkDataCollection(IDatabase db, DataTable table, IDao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public BlobChunkDataCollection(DataTable table, IDao dao = null, string rc = null) : base(table, dao, rc) { }
		public BlobChunkDataCollection(IQuery<BlobChunkDataColumns, BlobChunkData> q, Bam.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public BlobChunkDataCollection(IDatabase db, IQuery<BlobChunkDataColumns, BlobChunkData> q, bool load) : base(db, q, load) { }
		public BlobChunkDataCollection(IQuery<BlobChunkDataColumns, BlobChunkData> q, bool load) : base(q, load) { }
    }
}