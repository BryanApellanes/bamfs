using System.Reflection;
using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobHandleDataColumns: QueryFilter<BlobHandleDataColumns>, IFilterToken
    {
        public BlobHandleDataColumns() { }
        public BlobHandleDataColumns(string columnName, bool isForeignKey = false)
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
        
		public BlobHandleDataColumns KeyColumn => new BlobHandleDataColumns("Id");

        public BlobHandleDataColumns Id => new BlobHandleDataColumns("Id");
        public BlobHandleDataColumns Uuid => new BlobHandleDataColumns("Uuid");
        public BlobHandleDataColumns Cuid => new BlobHandleDataColumns("Cuid");
        public BlobHandleDataColumns BlobHash => new BlobHandleDataColumns("BlobHash");
        public BlobHandleDataColumns Created => new BlobHandleDataColumns("Created");


		public Type DaoType => typeof(BlobHandleData);

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}