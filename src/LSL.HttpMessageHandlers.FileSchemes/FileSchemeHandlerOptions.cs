using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LSL.HttpMessageHandlers.FileSchemes;

/// <summary>
/// File scheme handler options
/// </summary>
public class FileSchemeHandlerOptions
{
    /// <summary>
    /// A dictionary to map file extensions to mime types.
    /// </summary>
    /// <remarks>
    /// This defaults to an empty dictionary and as a result
    /// all file scheme content types default to <c>application/octet-stream</c>
    /// </remarks>
    internal Dictionary<string, string> ExtensionToMimeTypeMappings = [];
}