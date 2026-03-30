using System.Text.Json.Serialization;

namespace Bosqora.Heyzine.Enumerations;

/// <summary>
/// Defines the asynchronous conversion states returned by Heyzine.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<HeyzineConversionState>))]
public enum HeyzineConversionState
{
    /// <summary>
    /// The conversion has started but is not finished yet.
    /// </summary>
    [JsonStringEnumMemberName("started")]
    Started,

    /// <summary>
    /// The conversion completed successfully.
    /// </summary>
    [JsonStringEnumMemberName("processed")]
    Processed,

    /// <summary>
    /// The conversion failed.
    /// </summary>
    [JsonStringEnumMemberName("failed")]
    Failed,
}