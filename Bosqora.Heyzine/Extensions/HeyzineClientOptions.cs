namespace Bosqora.Heyzine.Extensions;

/// <summary>
/// Configures the shared HTTP client used by the Heyzine wrappers.
/// </summary>
public sealed class HeyzineClientOptions
{
    /// <summary>
    /// Gets or sets the HTTP timeout applied to Heyzine API calls.
    /// </summary>
    /// <remarks>
    /// Increase this value when using the sync conversion endpoint with large documents, because Heyzine can take longer than the default <see cref="HttpClient.Timeout"/>.
    /// When <see langword="null"/>, the platform default timeout is used.
    /// </remarks>
    public TimeSpan? Timeout { get; set; }
}