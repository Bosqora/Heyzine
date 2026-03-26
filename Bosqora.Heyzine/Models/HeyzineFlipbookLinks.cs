using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineFlipbookLinks
{
    [JsonPropertyName("base")]
    public Uri? Base { get; set; }

    public Uri? Custom { get; set; }

    public Uri? Pdf { get; set; }

    public Uri? Thumbnail { get; set; }
}