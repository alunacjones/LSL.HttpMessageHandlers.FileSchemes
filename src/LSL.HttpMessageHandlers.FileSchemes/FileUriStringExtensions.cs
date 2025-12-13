using System;

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
    public static string ToFileUriString(this string source) => ToFileUri(source).ToString();

    /// <summary>
    /// Convert a string to a file schemed URI
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Uri ToFileUri(this string source) => new($"file://{source}");
}