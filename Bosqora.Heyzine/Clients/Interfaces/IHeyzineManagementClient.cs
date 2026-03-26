using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients.Interfaces;

/// <summary>
/// Defines the authenticated management operations exposed by the Heyzine API.
/// </summary>
/// <remarks>
/// These operations require the <c>HeyzineApiKey</c> environment variable to be configured on the concrete client.
/// </remarks>
public interface IHeyzineManagementClient
{
    /// <summary>
    /// Adds a flipbook to a bookshelf, optionally at a specific zero-based position.
    /// </summary>
    /// <param name="request">The bookshelf and flipbook identifiers to send to the Heyzine bookshelf-add endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> AddFlipbookToBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Grants a user or password-based access entry to a protected flipbook.
    /// </summary>
    /// <param name="request">The access configuration to send to the access-add endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> AddUserAccessAsync(HeyzineAccessEntryRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Configures the password-protection mode and login prompts for a flipbook.
    /// </summary>
    /// <param name="request">The password-protection settings to send to the access-setup endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> ConfigurePasswordProtectionAsync(HeyzineAccessSetupRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a flipbook from the authenticated Heyzine account.
    /// </summary>
    /// <param name="flipbookId">The Heyzine flipbook identifier.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> DeleteFlipbookAsync(string flipbookId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the full metadata payload for a single flipbook.
    /// </summary>
    /// <param name="flipbookId">The Heyzine flipbook identifier.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The flipbook details returned by Heyzine, or <see langword="null"/> when the payload is empty.</returns>
    Task<HeyzineFlipbook?> GetFlipbookDetailsAsync(string flipbookId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all bookshelves in the authenticated Heyzine account.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The bookshelves returned by Heyzine, or <see langword="null"/> when the payload is empty.</returns>
    Task<IReadOnlyList<HeyzineBookshelf>?> ListBookshelvesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists the flipbooks contained in a specific bookshelf, ordered by position.
    /// </summary>
    /// <param name="bookshelfId">The Heyzine bookshelf identifier.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The flipbooks returned by Heyzine, or <see langword="null"/> when the payload is empty.</returns>
    Task<IReadOnlyList<HeyzineFlipbook>?> ListBookshelfFlipbooksAsync(string bookshelfId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all flipbooks in the authenticated Heyzine account.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The flipbooks returned by Heyzine, or <see langword="null"/> when the payload is empty.</returns>
    Task<IReadOnlyList<HeyzineFlipbook>?> ListFlipbooksAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a flipbook from a bookshelf.
    /// </summary>
    /// <param name="request">The bookshelf and flipbook identifiers to send to the bookshelf-remove endpoint.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> RemoveFlipbookFromBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a user or password-based access entry from a protected flipbook.
    /// </summary>
    /// <param name="request">The access entry to remove.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> RemoveUserAccessAsync(HeyzineAccessRemovalRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the social sharing metadata of a bookshelf.
    /// </summary>
    /// <param name="request">The social metadata payload for the target bookshelf.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> SetBookshelfSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the social sharing metadata of a flipbook.
    /// </summary>
    /// <param name="request">The social metadata payload for the target flipbook.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the request.</param>
    /// <returns>The API result returned by Heyzine.</returns>
    Task<HeyzineApiResult?> SetFlipbookSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default);
}