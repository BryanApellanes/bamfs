using System.Reflection;
using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobPropertyDataColumns: QueryFilter<BlobPropertyDataColumns>, IFilterToken
    {
        public BlobPropertyDataColumns() { }
        public BlobPropertyDataColumns(string columnName, bool isForeignKey = false)
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
        
		public BlobPropertyDataColumns KeyColumn => new BlobPropertyDataColumns("Id");

        public BlobPropertyDataColumns Id => new BlobPropertyDataColumns("Id");
        public BlobPropertyDataColumns Uuid => new BlobPropertyDataColumns("Uuid");
        public BlobPropertyDataColumns Cuid => new BlobPropertyDataColumns("Cuid");
        public BlobPropertyDataColumns BlobHash => new BlobPropertyDataColumns("BlobHash");
        public BlobPropertyDataColumns Name => new BlobPropertyDataColumns("Name");
        public BlobPropertyDataColumns Value => new BlobPropertyDataColumns("Value");
        public BlobPropertyDataColumns Created => new BlobPropertyDataColumns("Created");


		public Type DaoType => typeof(BlobPropertyData);

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}