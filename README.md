# Heyzine

A .NET client for the Heyzine API (see [heyzine.com/developers](https://heyzine.com/developers)).

The package exposes:

- a conversion client for the PDF-to-flipbook and oEmbed endpoints
- a management client for flipbooks, bookshelves, social metadata, and password-protection endpoints

## Why?

When trying to integrate flipbook functionality into a web application, we found Heyzine to be a service that exactly did what we needed.
Unfortunately, there was no simple library available... so we built one 😃. Since it gets the job done and it might help someone
else who's also trying to integrate the service, we decided to migrate it into this NuGet package.

## Example code

### Registering the service

```csharp
using Bosqora.Heyzine.Extensions;

...

builder.Services.AddHeyzine();
```

Set these environment variables before use:

- `HeyzineClientId` for conversion endpoints
- `HeyzineApiKey` for management endpoints

### Using the API endpoints

```csharp
using Bosqora.Heyzine.Clients.Interfaces;

public class YourClassThatUsesHeyzine(IHeyzineRestClient heyzineRestClient)
{

    public async Task<string> Convert(Uri pdfUrl)
    {
         var response = await heyzineRestClient.ConvertPdfAsync(pdfUrl);

         return response?.Url.AbsoluteUri;
    }
}
```

### Using the management endpoints

```csharp
using Bosqora.Heyzine.Clients.Interfaces;

public class YourAdminClass(IHeyzineManagementClient heyzineManagementClient)
{
    public async Task<int> CountFlipbooksAsync()
    {
        var flipbooks = await heyzineManagementClient.ListFlipbooksAsync();

        return flipbooks?.Count ?? 0;
    }
}
```
