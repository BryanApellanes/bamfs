using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bam.Data;

namespace Bam.Blobs.Data.Distributed.Dao
{
    public class OpaqueBlobChunkDataColumns: QueryFilter<OpaqueBlobChunkDataColumns>, IFilterToken
    {
        public OpaqueBlobChunkDataColumns() { }
        public OpaqueBlobChunkDataColumns(string columnName, bool isForeignKey = false)
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
        
		public OpaqueBlobChunkDataColumns KeyColumn => new OpaqueBlobChunkDataColumns("Id");

        public OpaqueBlobChunkDataColumns Id => new OpaqueBlobChunkDataColumns("Id");
        public OpaqueBlobChunkDataColumns Uuid => new OpaqueBlobChunkDataColumns("Uuid");
        public OpaqueBlobChunkDataColumns Cuid => new OpaqueBlobChunkDataColumns("Cuid");
        public OpaqueBlobChunkDataColumns ChunkHashHmac => new OpaqueBlobChunkDataColumns("ChunkHashHmac");
        public OpaqueBlobChunkDataColumns DataCipher => new OpaqueBlobChunkDataColumns("DataCipher");
        public OpaqueBlobChunkDataColumns Created => new OpaqueBlobChunkDataColumns("Created");


		public Type DaoType => typeof(OpaqueBlobChunkData);

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}