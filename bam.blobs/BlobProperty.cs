namespace Bam.Blobs;

/// <summary>
/// Represents a named metadata property associated with a blob.
/// </summary>
public class BlobProperty
{
    /// <summary>
    /// Gets or sets the SHA-256 hash of the blob this property belongs to.
    /// </summary>
    public string BlobHash { get; set; }

    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the value of the property, or null if not set.
    /// </summary>
    public string? Value { get; set; }
}