using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a bookshelf returned by the Heyzine management API.
/// </summary>
public class HeyzineBookshelf
{
    /// <summary>
    /// Gets or sets the UTC date returned for the bookshelf.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Gets or sets the bookshelf description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the number of flipbooks currently contained in the bookshelf.
    /// </summary>
    [JsonPropertyName("flipbook_count")]
    public int FlipbookCount { get; set; }

    /// <summary>
    /// Gets or sets the bookshelf identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the public links associated with the bookshelf.
    /// </summary>
    public HeyzineBookshelfLinks Links { get; set; } = new();

    /// <summary>
    /// Gets or sets the bookshelf subtitle.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets the bookshelf title.
    /// </summary>
    public string? Title { get; set; }
}