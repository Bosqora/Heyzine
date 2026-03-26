# Heyzine

A .NET client for the Heyzine API (see [heyzine.com/developers](https://heyzine.com/developers)). Currently, only the REST API's POST is
supported. We're working in the background to add support for the other endpoints.

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

### Using the service

```csharp
using Bosqora.Heyzine.Clients;
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
