using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Enumerations;

/// <summary>
/// Defines the password-protection modes supported by Heyzine.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<HeyzineAccessMode>))]
public enum HeyzineAccessMode
{
    /// <summary>
    /// Uses separate credentials for each allowed user.
    /// </summary>
    [JsonStringEnumMemberName("users")]
    Users,

    /// <summary>
    /// Uses a single shared password for all visitors.
    /// </summary>
    [JsonStringEnumMemberName("everyone")]
    Everyone,

    /// <summary>
    /// Disables password protection.
    /// </summary>
    [JsonStringEnumMemberName("disabled")]
    Disabled,
}