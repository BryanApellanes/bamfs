using System.Data;
using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobPropertyDataCollection: DaoCollection<BlobPropertyDataColumns, BlobPropertyData>
    { 
		public BlobPropertyDataCollection(){}
		public BlobPropertyDataCollection(IDatabase db, DataTable table, IDao dao = null!, string rc = null!) : base(db, table, dao, rc) { }
		public BlobPropertyDataCollection(DataTable table, IDao dao = null!, string rc = null!) : base(table, dao, rc) { }
		public BlobPropertyDataCollection(IQuery<BlobPropertyDataColumns, BlobPropertyData> q, Bam.Data.Dao dao = null!, string rc = null!) : base(q, dao, rc) { }
		public BlobPropertyDataCollection(IDatabase db, IQuery<BlobPropertyDataColumns, BlobPropertyData> q, bool load) : base(db, q, load) { }
		public BlobPropertyDataCollection(IQuery<BlobPropertyDataColumns, BlobPropertyData> q, bool load) : base(q, load) { }
    }
}