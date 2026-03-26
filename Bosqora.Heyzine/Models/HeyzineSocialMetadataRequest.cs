using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineSocialMetadataRequest
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("thumbnail")]
    public Uri? Thumbnail { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}