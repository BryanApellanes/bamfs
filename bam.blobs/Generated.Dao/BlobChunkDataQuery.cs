/*
	This file was generated and should not be modified directly
*/

using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobChunkDataQuery: Query<BlobChunkDataColumns, BlobChunkData>
    { 
		public BlobChunkDataQuery(){}
		public BlobChunkDataQuery(WhereDelegate<BlobChunkDataColumns> where, OrderBy<BlobChunkDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public BlobChunkDataQuery(Func<BlobChunkDataColumns, QueryFilter<BlobChunkDataColumns>> where, OrderBy<BlobChunkDataColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public BlobChunkDataQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static BlobChunkDataQuery Where(WhereDelegate<BlobChunkDataColumns> where)
        {
            return Where(where, null, null);
        }

        public static BlobChunkDataQuery Where(WhereDelegate<BlobChunkDataColumns> where, OrderBy<BlobChunkDataColumns> orderBy = null, Database db = null)
        {
            return new BlobChunkDataQuery(where, orderBy, db);
        }

		public BlobChunkDataCollection Execute()
		{
			return new BlobChunkDataCollection(this, true);
		}
    }
}