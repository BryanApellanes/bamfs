/*
	This file was generated and should not be modified directly
*/

using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobPropertyDataQuery: Query<BlobPropertyDataColumns, BlobPropertyData>
    { 
		public BlobPropertyDataQuery(){}
		public BlobPropertyDataQuery(WhereDelegate<BlobPropertyDataColumns> where, OrderBy<BlobPropertyDataColumns> orderBy = null!, Database db = null!) : base(where, orderBy, db) { }
		public BlobPropertyDataQuery(Func<BlobPropertyDataColumns, QueryFilter<BlobPropertyDataColumns>> where, OrderBy<BlobPropertyDataColumns> orderBy = null!, Database db = null!) : base(where, orderBy, db) { }
		public BlobPropertyDataQuery(Delegate where, Database db = null!) : base(where, db) { }

        public static BlobPropertyDataQuery Where(WhereDelegate<BlobPropertyDataColumns> where)
        {
            return Where(where, null!, null!);
        }

        public static BlobPropertyDataQuery Where(WhereDelegate<BlobPropertyDataColumns> where, OrderBy<BlobPropertyDataColumns> orderBy = null!, Database db = null!)
        {
            return new BlobPropertyDataQuery(where, orderBy, db);
        }

		public BlobPropertyDataCollection Execute()
		{
			return new BlobPropertyDataCollection(this, true);
		}
    }
}