/*
This file was generated and should not be modified directly
*/

using Bam.Data;
using Bam.Data.Repositories;

namespace Bam.Blobs.Data.Local.Dao.Repository
{
	[Serializable]
	public class LocalBlobDataRepository: DaoRepository
	{
		public LocalBlobDataRepository()
		{
			SchemaName = "LocalBlobData";
			BaseNamespace = "Bam.Blobs.Data.Local";

			
			AddType<Bam.Blobs.Data.Local.BlobChunkData>();
			
			
			AddType<Bam.Blobs.Data.Local.BlobHandleData>();
			
			
			AddType<Bam.Blobs.Data.Local.BlobPropertyData>();
			

			DaoAssembly = typeof(LocalBlobDataRepository).Assembly;
		}

		object _addLock = new object();
        public override void AddType(Type type)
        {
            lock (_addLock)
            {
                base.AddType(type);
                DaoAssembly = typeof(LocalBlobDataRepository).Assembly;
            }
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneBlobChunkDataWhere(WhereDelegate<BlobChunkDataColumns> where)
		{
			Bam.Blobs.Data.Local.Dao.BlobChunkData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneBlobChunkDataWhere(WhereDelegate<BlobChunkDataColumns> where, out Bam.Blobs.Data.Local.BlobChunkData result)
		{
			Bam.Blobs.Data.Local.Dao.BlobChunkData.SetOneWhere(where, out Bam.Blobs.Data.Local.Dao.BlobChunkData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Local.BlobChunkData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Local.BlobChunkData GetOneBlobChunkDataWhere(WhereDelegate<BlobChunkDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Local.BlobChunkData>();
			return (Bam.Blobs.Data.Local.BlobChunkData)Bam.Blobs.Data.Local.Dao.BlobChunkData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single BlobChunkData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a BlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobChunkDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Local.BlobChunkData OneBlobChunkDataWhere(WhereDelegate<BlobChunkDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Local.BlobChunkData>();
            return (Bam.Blobs.Data.Local.BlobChunkData)Bam.Blobs.Data.Local.Dao.BlobChunkData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Local.BlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Local.BlobChunkDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Local.BlobChunkData> BlobChunkDatasWhere(WhereDelegate<BlobChunkDataColumns> where, OrderBy<BlobChunkDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobChunkData>(Bam.Blobs.Data.Local.Dao.BlobChunkData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that receives a BlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobChunkDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Local.BlobChunkData> TopBlobChunkDatasWhere(int count, WhereDelegate<BlobChunkDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobChunkData>(Bam.Blobs.Data.Local.Dao.BlobChunkData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Local.BlobChunkData> TopBlobChunkDatasWhere(int count, WhereDelegate<BlobChunkDataColumns> where, OrderBy<BlobChunkDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobChunkData>(Bam.Blobs.Data.Local.Dao.BlobChunkData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of BlobChunkDatas
		/// </summary>
		public long CountBlobChunkDatas()
        {
            return Bam.Blobs.Data.Local.Dao.BlobChunkData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a BlobChunkDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobChunkDataColumns and other values
		/// </param>
        public long CountBlobChunkDatasWhere(WhereDelegate<BlobChunkDataColumns> where)
        {
            return Bam.Blobs.Data.Local.Dao.BlobChunkData.Count(where, Database);
        }
        
        /*public async Task BatchQueryBlobChunkDatas(int batchSize, WhereDelegate<BlobChunkDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Local.BlobChunkData>> batchProcessor)
        {
            await Bam.Blobs.Data.Local.Dao.BlobChunkData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Local.BlobChunkData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllBlobChunkDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Local.BlobChunkData>> batchProcessor)
        {
            await Bam.Blobs.Data.Local.Dao.BlobChunkData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Local.BlobChunkData>(batch));
            }, Database);
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneBlobHandleDataWhere(WhereDelegate<BlobHandleDataColumns> where)
		{
			Bam.Blobs.Data.Local.Dao.BlobHandleData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneBlobHandleDataWhere(WhereDelegate<BlobHandleDataColumns> where, out Bam.Blobs.Data.Local.BlobHandleData result)
		{
			Bam.Blobs.Data.Local.Dao.BlobHandleData.SetOneWhere(where, out Bam.Blobs.Data.Local.Dao.BlobHandleData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Local.BlobHandleData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Local.BlobHandleData GetOneBlobHandleDataWhere(WhereDelegate<BlobHandleDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Local.BlobHandleData>();
			return (Bam.Blobs.Data.Local.BlobHandleData)Bam.Blobs.Data.Local.Dao.BlobHandleData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single BlobHandleData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a BlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobHandleDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Local.BlobHandleData OneBlobHandleDataWhere(WhereDelegate<BlobHandleDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Local.BlobHandleData>();
            return (Bam.Blobs.Data.Local.BlobHandleData)Bam.Blobs.Data.Local.Dao.BlobHandleData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Local.BlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Local.BlobHandleDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Local.BlobHandleData> BlobHandleDatasWhere(WhereDelegate<BlobHandleDataColumns> where, OrderBy<BlobHandleDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobHandleData>(Bam.Blobs.Data.Local.Dao.BlobHandleData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that receives a BlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobHandleDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Local.BlobHandleData> TopBlobHandleDatasWhere(int count, WhereDelegate<BlobHandleDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobHandleData>(Bam.Blobs.Data.Local.Dao.BlobHandleData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Local.BlobHandleData> TopBlobHandleDatasWhere(int count, WhereDelegate<BlobHandleDataColumns> where, OrderBy<BlobHandleDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobHandleData>(Bam.Blobs.Data.Local.Dao.BlobHandleData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of BlobHandleDatas
		/// </summary>
		public long CountBlobHandleDatas()
        {
            return Bam.Blobs.Data.Local.Dao.BlobHandleData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a BlobHandleDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobHandleDataColumns and other values
		/// </param>
        public long CountBlobHandleDatasWhere(WhereDelegate<BlobHandleDataColumns> where)
        {
            return Bam.Blobs.Data.Local.Dao.BlobHandleData.Count(where, Database);
        }
        
        /*public async Task BatchQueryBlobHandleDatas(int batchSize, WhereDelegate<BlobHandleDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Local.BlobHandleData>> batchProcessor)
        {
            await Bam.Blobs.Data.Local.Dao.BlobHandleData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Local.BlobHandleData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllBlobHandleDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Local.BlobHandleData>> batchProcessor)
        {
            await Bam.Blobs.Data.Local.Dao.BlobHandleData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Local.BlobHandleData>(batch));
            }, Database);
        }

		
		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneBlobPropertyDataWhere(WhereDelegate<BlobPropertyDataColumns> where)
		{
			Bam.Blobs.Data.Local.Dao.BlobPropertyData.SetOneWhere(where, Database);
		}

		/// <summary>
		/// Set one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		public void SetOneBlobPropertyDataWhere(WhereDelegate<BlobPropertyDataColumns> where, out Bam.Blobs.Data.Local.BlobPropertyData result)
		{
			Bam.Blobs.Data.Local.Dao.BlobPropertyData.SetOneWhere(where, out Bam.Blobs.Data.Local.Dao.BlobPropertyData daoResult, Database);
			result = daoResult.CopyAs<Bam.Blobs.Data.Local.BlobPropertyData>();
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one is created; success depends on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		public Bam.Blobs.Data.Local.BlobPropertyData GetOneBlobPropertyDataWhere(WhereDelegate<BlobPropertyDataColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Blobs.Data.Local.BlobPropertyData>();
			return (Bam.Blobs.Data.Local.BlobPropertyData)Bam.Blobs.Data.Local.Dao.BlobPropertyData.GetOneWhere(where, Database)?.CopyAs(wrapperType, this);
		}

		/// <summary>
		/// Execute a query that should return only one result.  If no result is found null is returned.  If more
		/// than one result is returned a MultipleEntriesFoundException is thrown.  This method is most commonly used to retrieve a
		/// single BlobPropertyData instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a BlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobPropertyDataColumns and other values
		/// </param>
		public Bam.Blobs.Data.Local.BlobPropertyData OneBlobPropertyDataWhere(WhereDelegate<BlobPropertyDataColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Blobs.Data.Local.BlobPropertyData>();
            return (Bam.Blobs.Data.Local.BlobPropertyData)Bam.Blobs.Data.Local.Dao.BlobPropertyData.OneWhere(where, Database)?.CopyAs(wrapperType, this);
        }

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a Bam.Blobs.Data.Local.BlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between Bam.Blobs.Data.Local.BlobPropertyDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Local.BlobPropertyData> BlobPropertyDatasWhere(WhereDelegate<BlobPropertyDataColumns> where, OrderBy<BlobPropertyDataColumns> orderBy = null)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobPropertyData>(Bam.Blobs.Data.Local.Dao.BlobPropertyData.Where(where, orderBy, Database));
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
		/// <param name="where">A WhereDelegate that receives a BlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobPropertyDataColumns and other values
		/// </param>
		public IEnumerable<Bam.Blobs.Data.Local.BlobPropertyData> TopBlobPropertyDatasWhere(int count, WhereDelegate<BlobPropertyDataColumns> where)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobPropertyData>(Bam.Blobs.Data.Local.Dao.BlobPropertyData.Top(count, where, Database));
        }

        public IEnumerable<Bam.Blobs.Data.Local.BlobPropertyData> TopBlobPropertyDatasWhere(int count, WhereDelegate<BlobPropertyDataColumns> where, OrderBy<BlobPropertyDataColumns> orderBy)
        {
            return Wrap<Bam.Blobs.Data.Local.BlobPropertyData>(Bam.Blobs.Data.Local.Dao.BlobPropertyData.Top(count, where, orderBy, Database));
        }
                                
		/// <summary>
		/// Return the count of BlobPropertyDatas
		/// </summary>
		public long CountBlobPropertyDatas()
        {
            return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Count(Database);
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that receives a BlobPropertyDataColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between BlobPropertyDataColumns and other values
		/// </param>
        public long CountBlobPropertyDatasWhere(WhereDelegate<BlobPropertyDataColumns> where)
        {
            return Bam.Blobs.Data.Local.Dao.BlobPropertyData.Count(where, Database);
        }
        
        /*public async Task BatchQueryBlobPropertyDatas(int batchSize, WhereDelegate<BlobPropertyDataColumns> where, Action<IEnumerable<Bam.Blobs.Data.Local.BlobPropertyData>> batchProcessor)
        {
            await Bam.Blobs.Data.Local.Dao.BlobPropertyData.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Local.BlobPropertyData>(batch));
            }, Database);
        }*/
		
        public async Task BatchAllBlobPropertyDatas(int batchSize, Action<IEnumerable<Bam.Blobs.Data.Local.BlobPropertyData>> batchProcessor)
        {
            await Bam.Blobs.Data.Local.Dao.BlobPropertyData.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Blobs.Data.Local.BlobPropertyData>(batch));
            }, Database);
        }


	}
}																								
