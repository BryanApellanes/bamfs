using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using Bam.Data.Repositories;
using Newtonsoft.Json;
using Bam.Blobs.Data.Distributed;
using Bam.Blobs.Data.Distributed.Dao;

namespace Bam.Blobs.Data.Distributed.Wrappers
{
	// generated
	[Serializable]
	public class OpaqueBlobHandleDataWrapper: Bam.Blobs.Data.Distributed.OpaqueBlobHandleData, IHasUpdatedXrefCollectionProperties
	{
		public OpaqueBlobHandleDataWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public OpaqueBlobHandleDataWrapper(DaoRepository repository) : this()
		{
			this.DaoRepository = repository;
		}

		[JsonIgnore]
		public DaoRepository DaoRepository { get; set; }

		[JsonIgnore]
		public Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }

		protected void SetUpdatedXrefCollectionProperty(string propertyName, PropertyInfo correspondingProperty)
		{
			if(UpdatedXrefCollectionProperties != null && !UpdatedXrefCollectionProperties.ContainsKey(propertyName))
			{
				UpdatedXrefCollectionProperties.Add(propertyName, correspondingProperty);				
			}
			else if(UpdatedXrefCollectionProperties != null)
			{
				UpdatedXrefCollectionProperties[propertyName] = correspondingProperty;				
			}
		}





	}
	// -- generated
}																								
