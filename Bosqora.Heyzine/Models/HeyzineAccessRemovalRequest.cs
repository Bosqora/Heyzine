using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a request to revoke access to a password-protected flipbook.
/// </summary>
public class HeyzineAccessRemovalRequest
{
    /// <summary>
    /// Gets or sets the flipbook identifier whose access entry should be removed.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the password entry to remove. Required for access entries created with the "pass_only" or "otp" access types.
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the user name or email entry to remove. Required for "user_pass", "google", "email_link", and "send_code" access types.
    /// </summary>
    [JsonPropertyName("user")]
    public string? User { get; set; }
}