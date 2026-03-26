using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineResponse
{
    public string? Id { get; set; }

    [JsonPropertyName("meta")]
    public HeyzineMetadata Metadata { get; set; } = new();

    public Uri? Pdf { get; set; }

    public string? State { get; set; }

    public Uri? Thumbnail { get; set; }

    public Uri? Url { get; set; }
}