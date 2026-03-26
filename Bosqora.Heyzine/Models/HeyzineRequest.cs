using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineRequest(Uri pdfUrl, string clientId)
{
    [JsonPropertyName("pdf")]
    public Uri PdfUrl { get; set; } = pdfUrl;

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = clientId;
}
