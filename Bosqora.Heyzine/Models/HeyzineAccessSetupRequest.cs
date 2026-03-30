using System.Text.Json.Serialization;

using Bosqora.Heyzine.Enumerations;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a request to configure password protection for a flipbook.
/// </summary>
public class HeyzineAccessSetupRequest
{
    /// <summary>
    /// Gets or sets the protection mode.
    /// </summary>
    [JsonPropertyName("mode")]
    public HeyzineAccessMode? Mode { get; set; }

    /// <summary>
    /// Gets or sets the flipbook identifier to configure.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the shared password shown on the login page when the mode is "everyone".
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the prompt text used for the password field on the login screen.
    /// </summary>
    [JsonPropertyName("text_password")]
    public string? TextPassword { get; set; }

    /// <summary>
    /// Gets or sets the prompt text used for the username field on the login screen.
    /// </summary>
    [JsonPropertyName("text_user")]
    public string? TextUser { get; set; }
}