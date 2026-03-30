using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Enumerations;

/// <summary>
/// Defines the access entry types supported by Heyzine.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<HeyzineAccessType>))]
public enum HeyzineAccessType
{
    /// <summary>
    /// Uses a user name or email together with a password.
    /// </summary>
    [JsonStringEnumMemberName("user_pass")]
    UserPassword,

    /// <summary>
    /// Uses Google Sign-In.
    /// </summary>
    [JsonStringEnumMemberName("google")]
    Google,

    /// <summary>
    /// Uses a shared password without a user name.
    /// </summary>
    [JsonStringEnumMemberName("pass_only")]
    PasswordOnly,

    /// <summary>
    /// Uses a one-time password.
    /// </summary>
    [JsonStringEnumMemberName("otp")]
    OneTimePassword,

    /// <summary>
    /// Emails a one-time access link to the user.
    /// </summary>
    [JsonStringEnumMemberName("email_link")]
    EmailLink,

    /// <summary>
    /// Emails a code to the user.
    /// </summary>
    [JsonStringEnumMemberName("send_code")]
    SendCode,
}