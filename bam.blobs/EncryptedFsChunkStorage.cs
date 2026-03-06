using Bam.Data.Dynamic.Objects;
using Bam.Encryption;
using Bam.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Blobs
{
    public class EncryptedFsChunkStorage : ChunkStorage
    {
        public EncryptedFsChunkStorage(IEncryptor encryptor, IDecryptor decryptor) : base(new EncryptedFsSlottedStorage(Path.Combine(Environment.CurrentDirectory, "storage"), encryptor, decryptor))
        {
        }
    }
}
