using Bam.Data.Repositories;

namespace Bam.Blobs.Profiles.Data.Local;

/// <summary>
/// Represents the association between a blob profile handle and a delegated key set, linking a profile to its encryption keys.
/// </summary>
public class BlobProfileHandleDelegatedKeySetAssociationData : KeyedAuditRepoData
{
    /// <summary>
    /// Gets or sets the handle of the blob profile.
    /// </summary>
    public string ProfileHandle { get; set; }

    /// <summary>
    /// Gets or sets the handle of the delegated key set associated with the profile.
    /// </summary>
    public string KeySetHandle { get; set; }
}