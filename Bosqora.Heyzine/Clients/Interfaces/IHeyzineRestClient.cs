using Bosqora.Heyzine.Models;

namespace Bosqora.Heyzine.Clients.Interfaces;

public interface IHeyzineRestClient
{
    Task<HeyzineResponse?> ConvertPdfAsync(Uri pdfLocation);
}