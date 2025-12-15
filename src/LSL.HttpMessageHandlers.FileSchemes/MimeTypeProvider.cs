using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LSL.HttpMessageHandlers.FileSchemes;

internal class MimeTypeProvider
{
    private readonly Lazy<IDictionary<string, string>> _mimeTypeMap = new(
        () => 
        { 
            var assembly = typeof(MimeTypeProvider).Assembly;

            var resourceName = assembly.GetManifestResourceNames().First(n => n.EndsWith("mimeTypes.json"));
            using var stream = assembly.GetManifestResourceStream(resourceName);
            
            return JsonSerializer.Deserialize<IDictionary<string, string>>(stream);
        }
    );

    public string GetMimeType(string filePath) => 
        _mimeTypeMap.Value.TryGetValue(Path.GetExtension(filePath), out var mimeType)
            ? mimeType
            : "application/octet-stream";
}