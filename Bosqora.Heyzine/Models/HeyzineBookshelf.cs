using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineBookshelf
{
    public DateTimeOffset Date { get; set; }

    public string? Description { get; set; }

    [JsonPropertyName("flipbook_count")]
    public int FlipbookCount { get; set; }

    public string? Id { get; set; }

    public HeyzineBookshelfLinks Links { get; set; } = new();

    public string? Subtitle { get; set; }

    public string? Title { get; set; }
}