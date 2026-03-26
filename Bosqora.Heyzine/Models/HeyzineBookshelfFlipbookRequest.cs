using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a request to add or remove a flipbook from a bookshelf.
/// </summary>
public class HeyzineBookshelfFlipbookRequest
{
    /// <summary>
    /// Gets or sets the flipbook identifier to add or remove.
    /// </summary>
    [JsonPropertyName("flipbook_id")]
    public string? FlipbookId { get; set; }

    /// <summary>
    /// Gets or sets the bookshelf identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the zero-based position for the flipbook inside the bookshelf. When omitted, Heyzine appends the flipbook to the end.
    /// </summary>
    [JsonPropertyName("position")]
    public int? Position { get; set; }
}