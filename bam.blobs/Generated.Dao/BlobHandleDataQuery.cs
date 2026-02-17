/*
	This file was generated and should not be modified directly
*/

using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobHandleDataQuery: Query<BlobHandleDataColumns, BlobHandleData>
    { 
		public BlobHandleDataQuery(){}
		public BlobHandleDataQuery(WhereDelegate<BlobHandleDataColumns> where, OrderBy<BlobHandleDataColumns> orderBy = null!, Database db = null!) : base(where, orderBy, db) { }
		public BlobHandleDataQuery(Func<BlobHandleDataColumns, QueryFilter<BlobHandleDataColumns>> where, OrderBy<BlobHandleDataColumns> orderBy = null!, Database db = null!) : base(where, orderBy, db) { }
		public BlobHandleDataQuery(Delegate where, Database db = null!) : base(where, db) { }

        public static BlobHandleDataQuery Where(WhereDelegate<BlobHandleDataColumns> where)
        {
            return Where(where, null!, null!);
        }

        public static BlobHandleDataQuery Where(WhereDelegate<BlobHandleDataColumns> where, OrderBy<BlobHandleDataColumns> orderBy = null!, Database db = null!)
        {
            return new BlobHandleDataQuery(where, orderBy, db);
        }

		public BlobHandleDataCollection Execute()
		{
			return new BlobHandleDataCollection(this, true);
		}
    }
}