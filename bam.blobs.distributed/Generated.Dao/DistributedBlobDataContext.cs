/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Data;
using Bam.Data.Qi;

namespace Bam.Blobs.Data.Distributed.Dao
{
	// schema = DistributedBlobData
    public static class DistributedBlobDataContext
    {
		public static string ConnectionName
		{
			get
			{
				return "DistributedBlobData";
			}
		}

		public static IDatabase Db
		{
			get
			{
				return Bam.Data.Db.For(ConnectionName);
			}
		}


	public class OpaqueBlobChunkDataQueryContext
	{
			public OpaqueBlobChunkDataCollection Where(WhereDelegate<OpaqueBlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Where(where, db);
			}
		   
			public OpaqueBlobChunkDataCollection Where(WhereDelegate<OpaqueBlobChunkDataColumns> where, OrderBy<OpaqueBlobChunkDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Where(where, orderBy, db);
			}

			public OpaqueBlobChunkData OneWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.OneWhere(where, db);
			}

			public static OpaqueBlobChunkData GetOneWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.GetOneWhere(where, db);
			}
		
			public OpaqueBlobChunkData FirstOneWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.FirstOneWhere(where, db);
			}

			public OpaqueBlobChunkDataCollection Top(int count, WhereDelegate<OpaqueBlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Top(count, where, db);
			}

			public OpaqueBlobChunkDataCollection Top(int count, WhereDelegate<OpaqueBlobChunkDataColumns> where, OrderBy<OpaqueBlobChunkDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OpaqueBlobChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Count(where, db);
			}
	}

	static OpaqueBlobChunkDataQueryContext _opaqueBlobChunkDatas;
	static object _opaqueBlobChunkDatasLock = new object();
	public static OpaqueBlobChunkDataQueryContext OpaqueBlobChunkDatas
	{
		get
		{
			return _opaqueBlobChunkDatasLock.DoubleCheckLock<OpaqueBlobChunkDataQueryContext>(ref _opaqueBlobChunkDatas, () => new OpaqueBlobChunkDataQueryContext());
		}
	}
	public class OpaqueBlobHandleDataQueryContext
	{
			public OpaqueBlobHandleDataCollection Where(WhereDelegate<OpaqueBlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Where(where, db);
			}
		   
			public OpaqueBlobHandleDataCollection Where(WhereDelegate<OpaqueBlobHandleDataColumns> where, OrderBy<OpaqueBlobHandleDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Where(where, orderBy, db);
			}

			public OpaqueBlobHandleData OneWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.OneWhere(where, db);
			}

			public static OpaqueBlobHandleData GetOneWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.GetOneWhere(where, db);
			}
		
			public OpaqueBlobHandleData FirstOneWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.FirstOneWhere(where, db);
			}

			public OpaqueBlobHandleDataCollection Top(int count, WhereDelegate<OpaqueBlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Top(count, where, db);
			}

			public OpaqueBlobHandleDataCollection Top(int count, WhereDelegate<OpaqueBlobHandleDataColumns> where, OrderBy<OpaqueBlobHandleDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OpaqueBlobHandleDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Count(where, db);
			}
	}

	static OpaqueBlobHandleDataQueryContext _opaqueBlobHandleDatas;
	static object _opaqueBlobHandleDatasLock = new object();
	public static OpaqueBlobHandleDataQueryContext OpaqueBlobHandleDatas
	{
		get
		{
			return _opaqueBlobHandleDatasLock.DoubleCheckLock<OpaqueBlobHandleDataQueryContext>(ref _opaqueBlobHandleDatas, () => new OpaqueBlobHandleDataQueryContext());
		}
	}
	public class OpaqueBlobPropertyDataQueryContext
	{
			public OpaqueBlobPropertyDataCollection Where(WhereDelegate<OpaqueBlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Where(where, db);
			}
		   
			public OpaqueBlobPropertyDataCollection Where(WhereDelegate<OpaqueBlobPropertyDataColumns> where, OrderBy<OpaqueBlobPropertyDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Where(where, orderBy, db);
			}

			public OpaqueBlobPropertyData OneWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.OneWhere(where, db);
			}

			public static OpaqueBlobPropertyData GetOneWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.GetOneWhere(where, db);
			}
		
			public OpaqueBlobPropertyData FirstOneWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.FirstOneWhere(where, db);
			}

			public OpaqueBlobPropertyDataCollection Top(int count, WhereDelegate<OpaqueBlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Top(count, where, db);
			}

			public OpaqueBlobPropertyDataCollection Top(int count, WhereDelegate<OpaqueBlobPropertyDataColumns> where, OrderBy<OpaqueBlobPropertyDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OpaqueBlobPropertyDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Count(where, db);
			}
	}

	static OpaqueBlobPropertyDataQueryContext _opaqueBlobPropertyDatas;
	static object _opaqueBlobPropertyDatasLock = new object();
	public static OpaqueBlobPropertyDataQueryContext OpaqueBlobPropertyDatas
	{
		get
		{
			return _opaqueBlobPropertyDatasLock.DoubleCheckLock<OpaqueBlobPropertyDataQueryContext>(ref _opaqueBlobPropertyDatas, () => new OpaqueBlobPropertyDataQueryContext());
		}
	}
	public class OpaqueChunkDataQueryContext
	{
			public OpaqueChunkDataCollection Where(WhereDelegate<OpaqueChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Where(where, db);
			}
		   
			public OpaqueChunkDataCollection Where(WhereDelegate<OpaqueChunkDataColumns> where, OrderBy<OpaqueChunkDataColumns> orderBy = null, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Where(where, orderBy, db);
			}

			public OpaqueChunkData OneWhere(WhereDelegate<OpaqueChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.OneWhere(where, db);
			}

			public static OpaqueChunkData GetOneWhere(WhereDelegate<OpaqueChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.GetOneWhere(where, db);
			}
		
			public OpaqueChunkData FirstOneWhere(WhereDelegate<OpaqueChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.FirstOneWhere(where, db);
			}

			public OpaqueChunkDataCollection Top(int count, WhereDelegate<OpaqueChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Top(count, where, db);
			}

			public OpaqueChunkDataCollection Top(int count, WhereDelegate<OpaqueChunkDataColumns> where, OrderBy<OpaqueChunkDataColumns> orderBy, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<OpaqueChunkDataColumns> where, Database db = null)
			{
				return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Count(where, db);
			}
	}

	static OpaqueChunkDataQueryContext _opaqueChunkDatas;
	static object _opaqueChunkDatasLock = new object();
	public static OpaqueChunkDataQueryContext OpaqueChunkDatas
	{
		get
		{
			return _opaqueChunkDatasLock.DoubleCheckLock<OpaqueChunkDataQueryContext>(ref _opaqueChunkDatas, () => new OpaqueChunkDataQueryContext());
		}
	}
    }
}																								
