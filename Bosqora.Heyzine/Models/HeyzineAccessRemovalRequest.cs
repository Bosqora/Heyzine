using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineAccessRemovalRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("user")]
    public string? User { get; set; }
}