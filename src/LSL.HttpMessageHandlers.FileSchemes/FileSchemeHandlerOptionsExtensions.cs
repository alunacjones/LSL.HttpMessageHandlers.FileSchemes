using LSL.HttpMessageHandlers.FileSchemes.Infrastructure;

namespace LSL.HttpMessageHandlers.FileSchemes;

/// <summary>
/// File scheme handler options extensions
/// </summary>
public static class FileSchemeHandlerOptionsExtensions
{
    /// <summary>
    /// Add an extension mime type
    /// </summary>
    /// <remarks>
    /// The extensions include a dot e.g. <c>.txt</c>
    /// </remarks>
    /// <param name="source"></param>
    /// <param name="extension"></param>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    public static FileSchemeHandlerOptions WithExtensionMimeType(this FileSchemeHandlerOptions source, string extension, string mimeType)
    {
        source.AssertNotNull(nameof(source))
            .ExtensionToMimeTypeMappings[extension.AssertNotNull(nameof(extension))] = mimeType.AssertNotNull(nameof(mimeType));
            
        return source;
    }
}