using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineBookshelfFlipbookRequest
{
    [JsonPropertyName("flipbook_id")]
    public string? FlipbookId { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }
}