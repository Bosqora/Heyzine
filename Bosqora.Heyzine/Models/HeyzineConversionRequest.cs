using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineConversionRequest
{
    [JsonPropertyName("background_color")]
    public string? BackgroundColor { get; set; }

    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("download")]
    public bool? Download { get; set; }

    [JsonPropertyName("full_screen")]
    public bool? FullScreen { get; set; }

    [JsonPropertyName("logo")]
    public Uri? Logo { get; set; }

    [JsonPropertyName("pdf")]
    public Uri? Pdf { get; set; }

    [JsonPropertyName("prev_next")]
    public bool? PreviousNext { get; set; }

    [JsonPropertyName("private_note")]
    public string? PrivateNote { get; set; }

    [JsonPropertyName("share")]
    public bool? Share { get; set; }

    [JsonPropertyName("show_info")]
    public bool? ShowInfo { get; set; }

    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    [JsonPropertyName("tags")]
    public string? Tags { get; set; }

    [JsonPropertyName("template")]
    public string? Template { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}