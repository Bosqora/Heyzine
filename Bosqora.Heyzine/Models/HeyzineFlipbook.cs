using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents flipbook details returned by the Heyzine management API.
/// </summary>
public class HeyzineFlipbook
{
    /// <summary>
    /// Gets or sets the UTC date returned for the flipbook.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Gets or sets the flipbook description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the flipbook identifier.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the public links associated with the flipbook.
    /// </summary>
    public HeyzineFlipbookLinks Links { get; set; } = new();

    /// <summary>
    /// Gets or sets the raw oEmbed object returned by the flipbook-details endpoint.
    /// </summary>
    [JsonPropertyName("oembed")]
    public JsonElement? OEmbed { get; set; }

    /// <summary>
    /// Gets or sets the number of pages in the flipbook.
    /// </summary>
    public int Pages { get; set; }

    /// <summary>
    /// Gets or sets the zero-based bookshelf position when the flipbook is returned from a bookshelf listing.
    /// </summary>
    [JsonPropertyName("position")]
    public int? Position { get; set; }

    /// <summary>
    /// Gets or sets the internal management note returned in the "private" field.
    /// </summary>
    [JsonPropertyName("private")]
    public string? PrivateNote { get; set; }

    /// <summary>
    /// Gets or sets the source file size in bytes when Heyzine includes it in list responses.
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    /// Gets or sets the flipbook subtitle.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets the comma-separated tags associated with the flipbook.
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the flipbook title.
    /// </summary>
    public string? Title { get; set; }
}