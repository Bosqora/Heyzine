using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a PDF-to-flipbook conversion request.
/// </summary>
public class HeyzineConversionRequest
{
    /// <summary>
    /// Gets or sets a solid background color in hex format without the leading '#', replacing the template background image.
    /// </summary>
    [JsonPropertyName("background_color")]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the Heyzine client identifier used to authorize conversion requests.
    /// </summary>
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Gets or sets the description shown on the flipbook and in social shares.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether visitors can download the original PDF.
    /// </summary>
    [JsonPropertyName("download")]
    public bool? Download { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the fullscreen button is shown.
    /// </summary>
    [JsonPropertyName("full_screen")]
    public bool? FullScreen { get; set; }

    /// <summary>
    /// Gets or sets the logo URL to display in the flipbook. This is available on Standard plans and above.
    /// </summary>
    [JsonPropertyName("logo")]
    public Uri? Logo { get; set; }

    /// <summary>
    /// Gets or sets the direct URL of the PDF, DOCX, or PPTX file to convert. Heyzine requires a URL-encoded direct link with no redirections.
    /// </summary>
    [JsonPropertyName("pdf")]
    public Uri? Pdf { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether previous and next navigation buttons are shown.
    /// </summary>
    [JsonPropertyName("prev_next")]
    public bool? PreviousNext { get; set; }

    /// <summary>
    /// Gets or sets the internal note stored with the flipbook. This value is never shown to visitors.
    /// </summary>
    [JsonPropertyName("private_note")]
    public string? PrivateNote { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the share button is shown.
    /// </summary>
    [JsonPropertyName("share")]
    public bool? Share { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the title, subtitle, and description are shown on the flipbook background.
    /// </summary>
    [JsonPropertyName("show_info")]
    public bool? ShowInfo { get; set; }

    /// <summary>
    /// Gets or sets the subtitle shown on the flipbook and in social shares.
    /// </summary>
    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets a comma-separated tag list used to organize and filter the flipbook in the administration panel.
    /// </summary>
    [JsonPropertyName("tags")]
    public string? Tags { get; set; }

    /// <summary>
    /// Gets or sets the source flipbook identifier used as a template. It copies styles and controls, but not access lists or interactions.
    /// </summary>
    [JsonPropertyName("template")]
    public string? Template { get; set; }

    /// <summary>
    /// Gets or sets the title shown on the flipbook and in social shares.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }
}