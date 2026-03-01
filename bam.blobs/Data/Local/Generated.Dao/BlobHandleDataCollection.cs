using System.Data;
using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobHandleDataCollection: DaoCollection<BlobHandleDataColumns, BlobHandleData>
    { 
		public BlobHandleDataCollection(){}
		public BlobHandleDataCollection(IDatabase db, DataTable table, IDao dao = null!, string rc = null!) : base(db, table, dao, rc) { }
		public BlobHandleDataCollection(DataTable table, IDao dao = null!, string rc = null!) : base(table, dao, rc) { }
		public BlobHandleDataCollection(IQuery<BlobHandleDataColumns, BlobHandleData> q, Bam.Data.Dao dao = null!, string rc = null!) : base(q, dao, rc) { }
		public BlobHandleDataCollection(IDatabase db, IQuery<BlobHandleDataColumns, BlobHandleData> q, bool load) : base(db, q, load) { }
		public BlobHandleDataCollection(IQuery<BlobHandleDataColumns, BlobHandleData> q, bool load) : base(q, load) { }
    }
}