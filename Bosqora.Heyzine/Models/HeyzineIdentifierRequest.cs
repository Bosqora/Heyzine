using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineIdentifierRequest
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}