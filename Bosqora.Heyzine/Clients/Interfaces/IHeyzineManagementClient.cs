using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients.Interfaces;

public interface IHeyzineManagementClient
{
    Task<HeyzineApiResult?> AddFlipbookToBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> AddUserAccessAsync(HeyzineAccessEntryRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> ConfigurePasswordProtectionAsync(HeyzineAccessSetupRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> DeleteFlipbookAsync(string flipbookId, CancellationToken cancellationToken = default);

    Task<HeyzineFlipbook?> GetFlipbookDetailsAsync(string flipbookId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<HeyzineBookshelf>?> ListBookshelvesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<HeyzineFlipbook>?> ListBookshelfFlipbooksAsync(string bookshelfId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<HeyzineFlipbook>?> ListFlipbooksAsync(CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> RemoveFlipbookFromBookshelfAsync(HeyzineBookshelfFlipbookRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> RemoveUserAccessAsync(HeyzineAccessRemovalRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> SetBookshelfSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default);

    Task<HeyzineApiResult?> SetFlipbookSocialMetadataAsync(HeyzineSocialMetadataRequest request, CancellationToken cancellationToken = default);
}