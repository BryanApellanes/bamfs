using Bam.Data.Repositories;

namespace Bam.Blobs.Data.Local;

/// <summary>
/// Represents persistent data for a blob metadata property, storing a name-value pair associated with a blob hash.
/// </summary>
public class BlobPropertyData :RepoData
{
    /// <summary>
    /// Gets or sets the SHA-256 hash of the blob this property belongs to.
    /// </summary>
    public string BlobHash { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the property, or null if not set.
    /// </summary>
    public string? Value { get; set; }
}