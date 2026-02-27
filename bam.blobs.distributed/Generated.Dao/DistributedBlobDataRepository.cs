/*
This file was generated and should not be modified directly
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Data;
using Bam.Data.Repositories;
using Bam.Blobs.Data.Distributed;

namespace Bam.Blobs.Data.Distributed.Dao.Repository
{
	[Serializable]
	public partial class DistributedBlobDataRepository: AsyncDaoRepository
	{
		public DistributedBlobDataRepository()
		{
			SchemaName = "DistributedBlobData";
			BaseNamespace = "Bam.Blobs.Data.Distributed";

			
			AddType<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>();
			
			
			AddType<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>();
			
			
			AddType<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>();
			
			
			AddType<Bam.Blobs.Data.Distributed.OpaqueChunkData>();
			

			DaoAssembly = typeof(DistributedBlobDataRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(DistributedBlobDataRepository).Assembly;
            }
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueBlobChunkDataWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueBlobChunkDataWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where, out Bam.Blobs.Data.Distributed.OpaqueBlobChunkData result)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.SetOneWhere(where, out Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Distributed.OpaqueBlobChunkData GetOneOpaqueBlobChunkDataWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>();
			return (Bam.Blobs.Data.Distributed.OpaqueBlobChunkData)Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single OpaqueBlobChunkData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobChunkDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Distributed.OpaqueBlobChunkData OneOpaqueBlobChunkDataWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>();
            return (Bam.Blobs.Data.Distributed.OpaqueBlobChunkData)Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Distributed.OpaqueBlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Distributed.OpaqueBlobChunkDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData> OpaqueBlobChunkDatasWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where, OrderBy<OpaqueBlobChunkDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method issues a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobChunkDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData> TopOpaqueBlobChunkDatasWhere(int count, WhereDelegate<OpaqueBlobChunkDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData> TopOpaqueBlobChunkDatasWhere(int count, WhereDelegate<OpaqueBlobChunkDataColumns> where, OrderBy<OpaqueBlobChunkDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of OpaqueBlobChunkDatas
		/// </summary>
		public long CountOpaqueBlobChunkDatas()
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobChunkDataColumns and other values
		/// </param>
        public long CountOpaqueBlobChunkDatasWhere(WhereDelegate<OpaqueBlobChunkDataColumns> where)
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.Count(where, Database);
        }
        
        /*public async Task BatchQueryOpaqueBlobChunkDatas(int batchSize, WhereDelegate<OpaqueBlobChunkDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllOpaqueBlobChunkDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueBlobChunkData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobChunkData>(batch));
            }, Database);
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueBlobHandleDataWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueBlobHandleDataWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where, out Bam.Blobs.Data.Distributed.OpaqueBlobHandleData result)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.SetOneWhere(where, out Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Distributed.OpaqueBlobHandleData GetOneOpaqueBlobHandleDataWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>();
			return (Bam.Blobs.Data.Distributed.OpaqueBlobHandleData)Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single OpaqueBlobHandleData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobHandleDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Distributed.OpaqueBlobHandleData OneOpaqueBlobHandleDataWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>();
            return (Bam.Blobs.Data.Distributed.OpaqueBlobHandleData)Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Distributed.OpaqueBlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Distributed.OpaqueBlobHandleDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData> OpaqueBlobHandleDatasWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where, OrderBy<OpaqueBlobHandleDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method issues a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobHandleDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData> TopOpaqueBlobHandleDatasWhere(int count, WhereDelegate<OpaqueBlobHandleDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData> TopOpaqueBlobHandleDatasWhere(int count, WhereDelegate<OpaqueBlobHandleDataColumns> where, OrderBy<OpaqueBlobHandleDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of OpaqueBlobHandleDatas
		/// </summary>
		public long CountOpaqueBlobHandleDatas()
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobHandleDataColumns and other values
		/// </param>
        public long CountOpaqueBlobHandleDatasWhere(WhereDelegate<OpaqueBlobHandleDataColumns> where)
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.Count(where, Database);
        }
        
        /*public async Task BatchQueryOpaqueBlobHandleDatas(int batchSize, WhereDelegate<OpaqueBlobHandleDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllOpaqueBlobHandleDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueBlobHandleData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobHandleData>(batch));
            }, Database);
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueBlobPropertyDataWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueBlobPropertyDataWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where, out Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData result)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.SetOneWhere(where, out Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData GetOneOpaqueBlobPropertyDataWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>();
			return (Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData)Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single OpaqueBlobPropertyData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobPropertyDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData OneOpaqueBlobPropertyDataWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>();
            return (Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData)Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Distributed.OpaqueBlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Distributed.OpaqueBlobPropertyDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData> OpaqueBlobPropertyDatasWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where, OrderBy<OpaqueBlobPropertyDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method issues a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobPropertyDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData> TopOpaqueBlobPropertyDatasWhere(int count, WhereDelegate<OpaqueBlobPropertyDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData> TopOpaqueBlobPropertyDatasWhere(int count, WhereDelegate<OpaqueBlobPropertyDataColumns> where, OrderBy<OpaqueBlobPropertyDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>(Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of OpaqueBlobPropertyDatas
		/// </summary>
		public long CountOpaqueBlobPropertyDatas()
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueBlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueBlobPropertyDataColumns and other values
		/// </param>
        public long CountOpaqueBlobPropertyDatasWhere(WhereDelegate<OpaqueBlobPropertyDataColumns> where)
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.Count(where, Database);
        }
        
        /*public async Task BatchQueryOpaqueBlobPropertyDatas(int batchSize, WhereDelegate<OpaqueBlobPropertyDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllOpaqueBlobPropertyDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueBlobPropertyData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueBlobPropertyData>(batch));
            }, Database);
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueChunkDataWhere(WhereDelegate<OpaqueChunkDataColumns> where)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneOpaqueChunkDataWhere(WhereDelegate<OpaqueChunkDataColumns> where, out Bam.Blobs.Data.Distributed.OpaqueChunkData result)
		{
			Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.SetOneWhere(where, out Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Distributed.OpaqueChunkData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Distributed.OpaqueChunkData GetOneOpaqueChunkDataWhere(WhereDelegate<OpaqueChunkDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueChunkData>();
			return (Bam.Blobs.Data.Distributed.OpaqueChunkData)Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single OpaqueChunkData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueChunkDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Distributed.OpaqueChunkData OneOpaqueChunkDataWhere(WhereDelegate<OpaqueChunkDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Distributed.OpaqueChunkData>();
            return (Bam.Blobs.Data.Distributed.OpaqueChunkData)Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Distributed.OpaqueChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Distributed.OpaqueChunkDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueChunkData> OpaqueChunkDatasWhere(WhereDelegate<OpaqueChunkDataColumns> where, OrderBy<OpaqueChunkDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueChunkData>(Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Where(where, orderBy, Database));
        }
		
		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method issues a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that receives a OpaqueChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueChunkDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueChunkData> TopOpaqueChunkDatasWhere(int count, WhereDelegate<OpaqueChunkDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueChunkData>(Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Distributed.OpaqueChunkData> TopOpaqueChunkDatasWhere(int count, WhereDelegate<OpaqueChunkDataColumns> where, OrderBy<OpaqueChunkDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Distributed.OpaqueChunkData>(Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of OpaqueChunkDatas
		/// </summary>
		public long CountOpaqueChunkDatas()
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a OpaqueChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between OpaqueChunkDataColumns and other values
		/// </param>
        public long CountOpaqueChunkDatasWhere(WhereDelegate<OpaqueChunkDataColumns> where)
        {
            return Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.Count(where, Database);
        }
        
        /*public async Task BatchQueryOpaqueChunkDatas(int batchSize, WhereDelegate<OpaqueChunkDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueChunkData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueChunkData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllOpaqueChunkDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Distributed.OpaqueChunkData>> batchProcessor)
        {
            await Bam.Blobs.Data.Distributed.Dao.OpaqueChunkData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Distributed.OpaqueChunkData>(batch));
            }, Database);
        }


	}
}																								
