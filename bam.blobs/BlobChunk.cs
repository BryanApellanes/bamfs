/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Blobs.Data;
using Bam.Blobs.Data.Local;

namespace Bam.Blobs
{
    /// <summary>
    /// A chunk or segment of a blob
    /// </summary>
	[Serializable]
	public class BlobChunk: Chunk
	{
		public BlobChunk()
		{
		}

        /// <summary>
        /// Gets or sets the SHA-256 hash of the blob this chunk belongs to.
        /// </summary>
        public string BlobHash { get; set; } = null!;
        
        
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

        string _data = null!;
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

        byte[] _byteData = null!;
        /// <summary>
        /// Gets or sets the raw byte data of this chunk. Setting this value
        /// also updates <see cref="DataBase64"/> and recomputes the chunk hash.
        /// </summary>
        public new byte[] Data
        {
            get => _byteData;
            set
            {
                _byteData = value;
                _data = _byteData.ToBase64();
                SetChunkHash();
            }
        }

        /// <summary>
        /// Converts this blob chunk into a <see cref="BlobChunkData"/> descriptor containing hash, index, and position metadata.
        /// </summary>
        /// <returns>A <see cref="BlobChunkData"/> representing the chunk metadata.</returns>
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

        /// <summary>
        /// Converts this blob chunk into a <see cref="ChunkData"/> instance containing the hash and base64-encoded data.
        /// </summary>
        /// <returns>A <see cref="ChunkData"/> representing the chunk content.</returns>
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
