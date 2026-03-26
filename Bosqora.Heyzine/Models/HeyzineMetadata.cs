using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineMetadata
{
    [JsonPropertyName("num_pages")]
    public int NumberOfPages { get; set; }

    [JsonPropertyName("aspect_ratio")]
    public float AspectRatio { get; set; }
}