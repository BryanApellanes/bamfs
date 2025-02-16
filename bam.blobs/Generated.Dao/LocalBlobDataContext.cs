/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition

using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
	// schema = LocalBlobData
    public static class LocalBlobDataContext
    {
		public static string ConnectionName
		{
			get
			{
				return "LocalBlobData";
			}
		}

		public static IDatabase Db
		{
			get
			{
				return Bam.Data.Db.For(ConnectionName);
			}
		}


	public class BlobChunkDataQueryContext
	{
			public BlobChunkDataCollection Where(WhereDelegate<BlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.Where(where, db);
			}
		   
			public BlobChunkDataCollection Where(WhereDelegate<BlobChunkDataColumns> where, OrderBy<BlobChunkDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.Where(where, orderBy, db);
			}

			public BlobChunkData OneWhere(WhereDelegate<BlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.OneWhere(where, db);
			}

			public static BlobChunkData GetOneWhere(WhereDelegate<BlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.GetOneWhere(where, db);
			}
		
			public BlobChunkData FirstOneWhere(WhereDelegate<BlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.FirstOneWhere(where, db);
			}

			public BlobChunkDataCollection Top(int count, WhereDelegate<BlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.Top(count, where, db);
			}

			public BlobChunkDataCollection Top(int count, WhereDelegate<BlobChunkDataColumns> where, OrderBy<BlobChunkDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<BlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobChunkData.Count(where, db);
			}
	}

	static BlobChunkDataQueryContext _blobChunkDatas;
	static object _blobChunkDatasLock = new object();
	public static BlobChunkDataQueryContext BlobChunkDatas
	{
		get
		{
			return _blobChunkDatasLock.DoubleCheckLock<BlobChunkDataQueryContext>(ref _blobChunkDatas, () => new BlobChunkDataQueryContext());
		}
	}
	public class BlobHandleDataQueryContext
	{
			public BlobHandleDataCollection Where(WhereDelegate<BlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.Where(where, db);
			}
		   
			public BlobHandleDataCollection Where(WhereDelegate<BlobHandleDataColumns> where, OrderBy<BlobHandleDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.Where(where, orderBy, db);
			}

			public BlobHandleData OneWhere(WhereDelegate<BlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.OneWhere(where, db);
			}

			public static BlobHandleData GetOneWhere(WhereDelegate<BlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.GetOneWhere(where, db);
			}
		
			public BlobHandleData FirstOneWhere(WhereDelegate<BlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.FirstOneWhere(where, db);
			}

			public BlobHandleDataCollection Top(int count, WhereDelegate<BlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.Top(count, where, db);
			}

			public BlobHandleDataCollection Top(int count, WhereDelegate<BlobHandleDataColumns> where, OrderBy<BlobHandleDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<BlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobHandleData.Count(where, db);
			}
	}

	static BlobHandleDataQueryContext _blobHandleDatas;
	static object _blobHandleDatasLock = new object();
	public static BlobHandleDataQueryContext BlobHandleDatas
	{
		get
		{
			return _blobHandleDatasLock.DoubleCheckLock<BlobHandleDataQueryContext>(ref _blobHandleDatas, () => new BlobHandleDataQueryContext());
		}
	}
	public class BlobPropertyDataQueryContext
	{
			public BlobPropertyDataCollection Where(WhereDelegate<BlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Where(where, db);
			}
		   
			public BlobPropertyDataCollection Where(WhereDelegate<BlobPropertyDataColumns> where, OrderBy<BlobPropertyDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Where(where, orderBy, db);
			}

			public BlobPropertyData OneWhere(WhereDelegate<BlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.OneWhere(where, db);
			}

			public static BlobPropertyData GetOneWhere(WhereDelegate<BlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.GetOneWhere(where, db);
			}
		
			public BlobPropertyData FirstOneWhere(WhereDelegate<BlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.FirstOneWhere(where, db);
			}

			public BlobPropertyDataCollection Top(int count, WhereDelegate<BlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Top(count, where, db);
			}

			public BlobPropertyDataCollection Top(int count, WhereDelegate<BlobPropertyDataColumns> where, OrderBy<BlobPropertyDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<BlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Count(where, db);
			}
	}

	static BlobPropertyDataQueryContext _blobPropertyDatas;
	static object _blobPropertyDatasLock = new object();
	public static BlobPropertyDataQueryContext BlobPropertyDatas
	{
		get
		{
			return _blobPropertyDatasLock.DoubleCheckLock<BlobPropertyDataQueryContext>(ref _blobPropertyDatas, () => new BlobPropertyDataQueryContext());
		}
	}
    }
}																								
