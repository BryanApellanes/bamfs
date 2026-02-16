

namespace Bam.Net.Services.Chunking
{
    /// <summary>
    /// A streaming server that processes chunk get and set requests using an <see cref="IChunkStorage"/> backend.
    /// </summary>
    public class ChunkServer : StreamingServer<ChunkRequest, ChunkResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChunkServer"/> class with the specified chunk storage and optional logger.
        /// </summary>
        /// <param name="chunkStorage">The chunk storage backend to use for get and set operations.</param>
        /// <param name="logger">An optional logger instance.</param>
        public ChunkServer(IChunkStorage chunkStorage, ILogger logger = null)
        {
            ChunkStorage = chunkStorage;
            Logger = logger ?? Log.Default;
        }
        /// <summary>
        /// Gets or sets the chunk storage backend used for processing requests.
        /// </summary>
        public IChunkStorage ChunkStorage { get; set; }

        /// <summary>
        /// Processes an incoming chunk request by performing the specified get or set operation.
        /// </summary>
        /// <param name="context">The streaming context containing the chunk request.</param>
        /// <returns>A <see cref="ChunkResponse"/> indicating success or failure.</returns>
        public override ChunkResponse ProcessRequest(StreamingContext<ChunkRequest> context)
        {
            try
            {
                Args.ThrowIfNull(context?.Request.Body, "context.Request.Message");
                ChunkRequest msg = context.Request.Body;
                IChunk chunk = null;
                switch (msg.Operation)
                {
                    case ChunkOperation.Invalid:
                        throw new InvalidOperationException("Invalid ChunkOperation specified");
                    case ChunkOperation.Get:
                        chunk = ChunkStorage.GetChunk(msg.Hash);                        
                        break;
                    case ChunkOperation.Set:
                        ChunkStorage.SetChunk(msg.Chunk);
                        break;
                    default:
                        break;
                }

                return new ChunkResponse { Success = true, Chunk = chunk };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception processing request: {0}", ex, ex.Message);
                return new ChunkResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
