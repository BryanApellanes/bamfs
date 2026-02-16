using Bam.Data.Repositories;

namespace Bam.Blobs.Profiles.Data.Local;

/// <summary>
/// Represents a blob profile handle, identified by a unique handle string used as a composite key.
/// </summary>
public class BlobProfileHandleData : KeyedAuditRepoData
{
    /// <summary>
    /// Gets or sets the unique handle that identifies this blob profile.
    /// </summary>
    [CompositeKey]
    public string Handle { get; set; }
}