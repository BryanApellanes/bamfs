/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Net.Services.Chunking;

namespace Bam.Chunking
{
    /// <summary>
    /// A chunk or segment of a file
    /// </summary>
	[Serializable]
	public class FileChunk: IChunkable
	{
		public FileChunk()
		{
		}
        
        public string FileHash { get; set; }

        /// <summary>
        /// Hash of this chunks ByteData
        /// </summary>
        public string ChunkHash { get; set; }
        
        /// <summary>
        /// The index of this chunk relative to
        /// all file chunks
        /// </summary>
		public long ChunkIndex { get; set; }

        /// <summary>
        /// The index in the file stream
        /// where this chunk begins
        /// </summary>
        public long StreamIndex { get; set; }

        /// <summary>
        /// The length of the byte[] data, Data
        /// base 64 decoded
        /// </summary>
		public int ChunkLength
		{
			get;
			set;
		}

        string _data;
        /// <summary>
        /// The base 64 encoded data of this 
        /// chunk
        /// </summary>
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                _byteData = _data.FromBase64();
                SetChunkHash();
            }
        }

        byte[] _byteData;
        protected internal byte[] ByteData
        {
            get
            {
                return _byteData;
            }
            set
            {
                _byteData = value;
                _data = _byteData.ToBase64();
                SetChunkHash();
            }
        }

        public ChunkedDataDataRelationship ToChunkDataDescriptor()
        {
            return new ChunkedDataDataRelationship
            {
                DataHash = FileHash,
                ChunkHash = ChunkHash,
                ChunkIndex = ChunkIndex,
                StreamIndex = StreamIndex
            };
        }

        public ChunkData ToChunkData()
        {
            return new ChunkData
            {
                ChunkHash = ChunkHash,
                ChunkLength = ChunkLength,
                Data = Data
            };
        }

        private void SetChunkHash()
        {
            ChunkHash = _byteData.Sha256();
        }

        public IChunk ToChunk()
        {
            return new Chunk { Hash = ChunkHash, Data = Data.FromBase64() };
        }
    }
}
