using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a request to override the social metadata of a flipbook or bookshelf.
/// </summary>
public class HeyzineSocialMetadataRequest
{
    /// <summary>
    /// Gets or sets the description used for social sharing.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the flipbook or bookshelf identifier whose social metadata should be updated.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the absolute URL of the social sharing thumbnail image.
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public Uri? Thumbnail { get; set; }

    /// <summary>
    /// Gets or sets the title used for social sharing.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}