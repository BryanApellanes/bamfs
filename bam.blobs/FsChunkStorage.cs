using Bam.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Bam.Blobs
{
    public class FsChunkStorage : ChunkStorage
    {
        public FsChunkStorage() : base(new FsSlottedStorage())
        {
        }

        public FsChunkStorage(string storageRootPath) : base(new FsSlottedStorage(storageRootPath))
        { 
        }
    }
}
