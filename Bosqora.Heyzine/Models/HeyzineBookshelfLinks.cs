namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents the public links returned for a bookshelf.
/// </summary>
public class HeyzineBookshelfLinks
{
    /// <summary>
    /// Gets or sets the bookshelf thumbnail URL.
    /// </summary>
    public Uri? Thumbnail { get; set; }

    /// <summary>
    /// Gets or sets the public bookshelf URL.
    /// </summary>
    public Uri? Url { get; set; }
}