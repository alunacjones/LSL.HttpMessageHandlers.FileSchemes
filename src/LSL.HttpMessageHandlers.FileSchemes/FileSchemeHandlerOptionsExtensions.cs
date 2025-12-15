using System;
using System.Text.RegularExpressions;
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
        source.AssertNotNull(nameof(source));
        extension.AssertNotNull(nameof(extension)).AssertIsAFileExtension();
        mimeType.AssertNotNull(nameof(mimeType));

        source.ExtensionToMimeTypeMappings[extension] = mimeType;
            
        return source;
    }

    internal static string AssertIsAFileExtension(this string source) =>
        _extensionRegEx.IsMatch(source) ? source : throw new ArgumentException($"File extension '{source}' is invalid. It must start with a dot followed by letters or numbers");

    private static readonly Regex _extensionRegEx = new(@"^\.[a-zA-z0-9]+$", RegexOptions.Compiled);
}