using System.Reflection;
using Bam.Data;

namespace Bam.Blobs.Data.Local.Dao
{
    public class BlobChunkDataColumns: QueryFilter<BlobChunkDataColumns>, IFilterToken
    {
        public BlobChunkDataColumns() { }
        public BlobChunkDataColumns(string columnName, bool isForeignKey = false)
            : base(columnName)
        { 
            _isForeignKey = isForeignKey;
        }
        
        public bool IsKey()
        {
            return (bool)ColumnName!.Equals(KeyColumn.ColumnName);
        }

        private bool? _isForeignKey;
        public bool IsForeignKey
        {
            get
            {
                if (_isForeignKey == null)
                {
                    PropertyInfo? prop = DaoType
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
        
		public BlobChunkDataColumns KeyColumn => new BlobChunkDataColumns("Id");

        public BlobChunkDataColumns Id => new BlobChunkDataColumns("Id");
        public BlobChunkDataColumns Uuid => new BlobChunkDataColumns("Uuid");
        public BlobChunkDataColumns Cuid => new BlobChunkDataColumns("Cuid");
        public BlobChunkDataColumns BlobHash => new BlobChunkDataColumns("BlobHash");
        public BlobChunkDataColumns ChunkHash => new BlobChunkDataColumns("ChunkHash");
        public BlobChunkDataColumns ChunkIndex => new BlobChunkDataColumns("ChunkIndex");
        public BlobChunkDataColumns BlobIndex => new BlobChunkDataColumns("BlobIndex");
        public BlobChunkDataColumns Created => new BlobChunkDataColumns("Created");


		public Type DaoType => typeof(BlobChunkData);

		public string Operator { get; set; } = null!;

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}