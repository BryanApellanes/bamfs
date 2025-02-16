/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Blobs.Data;
using Bam.Blobs.Data.Local;

namespace Bam.Blobs
{
    /// <summary>
    /// A chunk or segment of a file
    /// </summary>
	[Serializable]
	public class BlobChunk: Chunk
	{
		public BlobChunk()
		{
		}
        
        public string BlobHash { get; set; }
        
        
        /// <summary>
        /// The index of this chunk relative to
        /// all chunks.
        /// </summary>
		public long ChunkIndex { get; set; }

        /// <summary>
        /// The index in the blob stream
        /// where this chunk begins
        /// </summary>
        public long BlobIndex { get; set; }

        string _data;
        /// <summary>
        /// The base 64 encoded data of this 
        /// chunk
        /// </summary>
        public string DataBase64
        {
            get => _data;
            set
            {
                _data = value;
                _byteData = _data.FromBase64();
                SetChunkHash();
            }
        }

        byte[] _byteData;
        public byte[] Data
        {
            get => _byteData;
            set
            {
                _byteData = value;
                _data = _byteData.ToBase64();
                SetChunkHash();
            }
        }

        public BlobChunkData ToChunkDataDescriptor()
        {
            return new BlobChunkData
            {
                BlobHash = BlobHash,
                ChunkHash = ChunkHash,
                ChunkIndex = ChunkIndex,
                BlobIndex = BlobIndex
            };
        }

        public ChunkData ToChunkData()
        {
            return new ChunkData
            {
                ChunkHash = ChunkHash,
                Data = DataBase64
            };
        }

        private void SetChunkHash()
        {
            ChunkHash = _byteData.Sha256();
        }
    }
}
