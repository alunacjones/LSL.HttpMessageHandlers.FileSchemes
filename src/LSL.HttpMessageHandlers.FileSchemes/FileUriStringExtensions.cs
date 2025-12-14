using System;
using System.Text.RegularExpressions;

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

    /// <summary>
    /// Tests a string to see if it is like a filename
    /// </summary>
    /// <remarks>
    /// Returns true if:
    /// <list type="bullet">
    ///     <item>The string is not <see langword="null"/></item>
    ///     <item>The string is not <c>.</c></item>
    ///     <item>The string is not <c>..</c></item>
    ///     <item>The string contains <c>.</c> with non dot characters after it</item>
    /// </list>
    /// </remarks>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsLikeAFileName(this string source) => 
        source is not null && _fileNameRegex.IsMatch(source);

    private static readonly Regex _fileNameRegex = new(@"\.[^\.]+$", RegexOptions.Compiled);
}