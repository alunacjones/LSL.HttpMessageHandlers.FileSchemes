using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Options;

namespace LSL.HttpMessageHandlers.FileSchemes;

internal class StreamContentHeaderProvider(MimeTypeProvider mimeTypeProvider, IOptionsMonitor<FileSchemeHandlerOptions> options)
{
    public StreamContent AddHeaders(StreamContent streamContent, string filePath)
    {
        streamContent.Headers.ContentType = new(options
            .CurrentValue
            .ExtensionToMimeTypeMappings
            .TryGetValue(Path.GetExtension(filePath), out var value)
                ? value
                : mimeTypeProvider.GetMimeType(filePath)
        );

        return streamContent;
    }
}