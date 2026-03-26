using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineOEmbedResponse
{
    public int Height { get; set; }

    public string? Html { get; set; }

    [JsonPropertyName("provider_name")]
    public string? ProviderName { get; set; }

    [JsonPropertyName("provider_url")]
    public Uri? ProviderUrl { get; set; }

    [JsonPropertyName("thumbnail_height")]
    public int ThumbnailHeight { get; set; }

    [JsonPropertyName("thumbnail_url")]
    public Uri? ThumbnailUrl { get; set; }

    [JsonPropertyName("thumbnail_width")]
    public int ThumbnailWidth { get; set; }

    public string? Title { get; set; }

    public string? Type { get; set; }

    public string? Version { get; set; }

    public int Width { get; set; }
}