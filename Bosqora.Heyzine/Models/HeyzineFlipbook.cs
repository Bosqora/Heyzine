using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineFlipbook
{
    public DateTimeOffset Date { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public HeyzineFlipbookLinks Links { get; set; } = new();

    [JsonPropertyName("oembed")]
    public JsonElement? OEmbed { get; set; }

    public int Pages { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }

    [JsonPropertyName("private")]
    public string? PrivateNote { get; set; }

    public int? Size { get; set; }

    public string? Subtitle { get; set; }

    public string? Tags { get; set; }

    public string? Title { get; set; }
}