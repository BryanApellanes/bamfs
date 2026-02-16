namespace Bam.Net.Services.Chunking;

/// <summary>
/// Provides string extension methods for chunking operations.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Splits the string into substrings of the specified maximum length.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="maxLength">The maximum length of each substring.</param>
    /// <returns>An enumerable of substrings, each up to <paramref name="maxLength"/> characters long.</returns>
    public static IEnumerable<string> SplitByLength(this string value, int maxLength)
    {
        for (int index = 0; index < value.Length; index += maxLength)
        {
            yield return value.Substring(index, Math.Min(maxLength, value.Length - index));
        }
    }
}