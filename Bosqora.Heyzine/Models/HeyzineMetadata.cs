using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents conversion metadata returned by Heyzine.
/// </summary>
public class HeyzineMetadata
{
    /// <summary>
    /// Gets or sets the number of pages detected in the source document.
    /// </summary>
    [JsonPropertyName("num_pages")]
    public int NumberOfPages { get; set; }

    /// <summary>
    /// Gets or sets the page aspect ratio reported for the converted document.
    /// </summary>
    [JsonPropertyName("aspect_ratio")]
    public float AspectRatio { get; set; }
}