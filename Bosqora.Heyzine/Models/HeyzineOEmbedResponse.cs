using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents the oEmbed response returned for a flipbook URL.
/// </summary>
public class HeyzineOEmbedResponse
{
    /// <summary>
    /// Gets or sets the embed height returned by the oEmbed endpoint.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the iframe HTML snippet that can be rendered to embed the flipbook.
    /// </summary>
    public string? Html { get; set; }

    /// <summary>
    /// Gets or sets the oEmbed provider name, which is typically "Heyzine".
    /// </summary>
    [JsonPropertyName("provider_name")]
    public string? ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the provider URL for the oEmbed response.
    /// </summary>
    [JsonPropertyName("provider_url")]
    public Uri? ProviderUrl { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail height in pixels.
    /// </summary>
    [JsonPropertyName("thumbnail_height")]
    public int ThumbnailHeight { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail image URL.
    /// </summary>
    [JsonPropertyName("thumbnail_url")]
    public Uri? ThumbnailUrl { get; set; }

    /// <summary>
    /// Gets or sets the thumbnail width in pixels.
    /// </summary>
    [JsonPropertyName("thumbnail_width")]
    public int ThumbnailWidth { get; set; }

    /// <summary>
    /// Gets or sets the title reported by the oEmbed endpoint.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the oEmbed resource type, which Heyzine returns as "rich".
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the oEmbed specification version, typically "1.0".
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Gets or sets the embed width returned by the oEmbed endpoint.
    /// </summary>
    public int Width { get; set; }
}