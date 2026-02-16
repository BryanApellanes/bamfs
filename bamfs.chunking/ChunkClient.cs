using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.CoreServices.Files;

namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// A streaming client that communicates with a <see cref="ChunkServer"/> to get and set chunks remotely, implementing <see cref="IChunkStorage"/>.
    /// </summary>
    public class ChunkClient : StreamingClient<ChunkRequest, ChunkResponse>, IChunkStorage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkClient"/> class that connects to the specified host and port.
        /// </summary>
        /// <param name="hostName">The host name of the chunk server.</param>
        /// <param name="port">The port number of the chunk server.</param>
        public ChunkClient(string hostName, int port) : base(hostName, port)
        {
            ExceptionMode = ChunkExceptionMode.EmitEvents;
        }
        /// <summary>
        /// Raised when an exception occurs during a GetChunk operation (when <see cref="ExceptionMode"/> is <see cref="ChunkExceptionMode.EmitEvents"/>).
        /// </summary>
        public event EventHandler GetChunkException;

        /// <summary>
        /// Raised when an exception occurs during a SetChunk operation (when <see cref="ExceptionMode"/> is <see cref="ChunkExceptionMode.EmitEvents"/>).
        /// </summary>
        public event EventHandler SetChunkException;

        /// <summary>
        /// Gets or sets the error handling mode for chunk operations.
        /// </summary>
        public ChunkExceptionMode ExceptionMode { get; set; }

        /// <summary>
        /// Retrieves a chunk from the remote server by its hash.
        /// </summary>
        /// <param name="hash">The SHA-256 hash of the chunk to retrieve.</param>
        /// <returns>The chunk returned by the server.</returns>
        public IChunk GetChunk(string hash)
        {
            StreamingResponse<ChunkResponse> response = SendRequest(new ChunkRequest
            {
                Operation = ChunkOperation.Get,
                Hash = hash
            });

            if (!response.Body.Success)
            {
                HandleException(GetChunkException, hash, response);
            }

            return response.Body.Chunk;            
        }

        /// <summary>
        /// Sends a chunk to the remote server for storage.
        /// </summary>
        /// <param name="chunkData">The chunk to store remotely.</param>
        public void SetChunk(IChunk chunkData)
        {
            StreamingResponse<ChunkResponse> response = SendRequest(new ChunkRequest
            {
                Operation = ChunkOperation.Set,
                Chunk = new Chunk
                {
                    Hash = chunkData.Hash,
                    Data = chunkData.Data
                }
            });

            if (!response.Body.Success)
            {
                HandleException(SetChunkException, chunkData.Hash, response);
            }
        }

        private void HandleException(EventHandler toFire, string hash, StreamingResponse<ChunkResponse> response)
        {
            switch (ExceptionMode)
            {
                case ChunkExceptionMode.Throw:
                    throw new ChunkException(response.Body);
                case ChunkExceptionMode.EmitEvents:
                    FireEvent(toFire, new ChunkExceptionEventArgs { Hash = hash });
                    break;
            }
        }
    }
}
