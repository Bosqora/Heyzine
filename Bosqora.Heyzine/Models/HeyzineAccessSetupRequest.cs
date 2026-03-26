using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

public class HeyzineAccessSetupRequest
{
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("text_password")]
    public string? TextPassword { get; set; }

    [JsonPropertyName("text_user")]
    public string? TextUser { get; set; }
}