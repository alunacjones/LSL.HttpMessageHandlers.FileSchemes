using System;
using LSL.HttpMessageHandlers.FileSchemes.Infrastructure;

namespace LSL.HttpMessageHandlers.FileSchemes;

/// <summary>
/// 
/// </summary>
public static class FileUriStringExtensions
{
    /// <summary>
    /// Convert a string to a file schemed URI string
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToFileUriString(this string source) => 
        $"file:///{source.AssertNotNull(nameof(source)).Replace("\\", "/")}";

    /// <summary>
    /// Convert a string to a file schemed URI
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Uri ToFileUri(this string source) =>
        new(source.ToFileUriString());
}