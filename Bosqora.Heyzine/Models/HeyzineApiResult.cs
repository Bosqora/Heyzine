namespace Bosqora.Heyzine.Models;

/// <summary>
/// Represents a standard Heyzine API operation result.
/// </summary>
public class HeyzineApiResult
{
    /// <summary>
    /// Gets or sets the numeric status code returned by Heyzine.
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// Gets or sets the human-readable result message returned by Heyzine.
    /// </summary>
    public string? Msg { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the operation completed successfully.
    /// </summary>
    public bool Success { get; set; }
}