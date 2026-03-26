using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a request to grant access to a password-protected flipbook.
/// </summary>
public class HeyzineAccessEntryRequest
{
    /// <summary>
    /// Gets or sets the access method. Supported values are "user_pass", "google", "pass_only", "otp", "email_link", and "send_code".
    /// </summary>
    [JsonPropertyName("access_type")]
    public string? AccessType { get; set; }

    /// <summary>
    /// Gets or sets the flipbook identifier that will receive the access entry.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the password used on the flipbook login page. Required for "user_pass", "pass_only", and "otp" access types.
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the user name or email shown on the login page. Required for "user_pass", "google", "email_link", and "send_code" access types.
    /// </summary>
    [JsonPropertyName("user")]
    public string? User { get; set; }
}