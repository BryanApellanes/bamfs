using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bam.Data;

namespace Bam.Blobs.Data.Distributed.Dao
{
    public class OpaqueChunkDataColumns: QueryFilter<OpaqueChunkDataColumns>, IFilterToken
    {
        public OpaqueChunkDataColumns() { }
        public OpaqueChunkDataColumns(string columnName, bool isForeignKey = false)
            : base(columnName)
        { 
            _isForeignKey = isForeignKey;
        }
        
        public bool IsKey()
        {
            return (bool)ColumnName?.Equals(KeyColumn.ColumnName);
        }

        private bool? _isForeignKey;
        public bool IsForeignKey
        {
            get
            {
                if (_isForeignKey == null)
                {
                    PropertyInfo prop = DaoType
                        .GetProperties()
                        .FirstOrDefault(pi => ((MemberInfo) pi)
                            .HasCustomAttributeOfType<ForeignKeyAttribute>(out ForeignKeyAttribute foreignKeyAttribute)
                                && foreignKeyAttribute.Name.Equals(ColumnName));
                        _isForeignKey = prop != null;
                }

                return _isForeignKey.Value;
            }
            set => _isForeignKey = value;
        }
        
		public OpaqueChunkDataColumns KeyColumn => new OpaqueChunkDataColumns("Id");

        public OpaqueChunkDataColumns Id => new OpaqueChunkDataColumns("Id");
        public OpaqueChunkDataColumns Uuid => new OpaqueChunkDataColumns("Uuid");
        public OpaqueChunkDataColumns Cuid => new OpaqueChunkDataColumns("Cuid");
        public OpaqueChunkDataColumns ChunkHashHmac => new OpaqueChunkDataColumns("ChunkHashHmac");
        public OpaqueChunkDataColumns DataCipher => new OpaqueChunkDataColumns("DataCipher");
        public OpaqueChunkDataColumns Created => new OpaqueChunkDataColumns("Created");


		public Type DaoType => typeof(OpaqueChunkData);

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}