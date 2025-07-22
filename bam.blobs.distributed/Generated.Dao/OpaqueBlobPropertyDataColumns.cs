using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Bam.Data;

namespace Bam.Blobs.Data.Distributed.Dao
{
    public class OpaqueBlobPropertyDataColumns: QueryFilter<OpaqueBlobPropertyDataColumns>, IFilterToken
    {
        public OpaqueBlobPropertyDataColumns() { }
        public OpaqueBlobPropertyDataColumns(string columnName, bool isForeignKey = false)
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
        
		public OpaqueBlobPropertyDataColumns KeyColumn => new OpaqueBlobPropertyDataColumns("Id");

        public OpaqueBlobPropertyDataColumns Id => new OpaqueBlobPropertyDataColumns("Id");
        public OpaqueBlobPropertyDataColumns Uuid => new OpaqueBlobPropertyDataColumns("Uuid");
        public OpaqueBlobPropertyDataColumns Cuid => new OpaqueBlobPropertyDataColumns("Cuid");
        public OpaqueBlobPropertyDataColumns BlobHashHmac => new OpaqueBlobPropertyDataColumns("BlobHashHmac");
        public OpaqueBlobPropertyDataColumns NameHmac => new OpaqueBlobPropertyDataColumns("NameHmac");
        public OpaqueBlobPropertyDataColumns ValueCipher => new OpaqueBlobPropertyDataColumns("ValueCipher");
        public OpaqueBlobPropertyDataColumns Created => new OpaqueBlobPropertyDataColumns("Created");


		public Type DaoType => typeof(OpaqueBlobPropertyData);

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}