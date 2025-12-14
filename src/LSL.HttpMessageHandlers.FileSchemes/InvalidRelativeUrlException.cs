using System;

namespace LSL.HttpMessageHandlers.FileSchemes;

/// <summary>
/// Invalid relative url exception
/// </summary>
/// <remarks>
/// Thrown when a relative URL has <c>.</c> or <c>..</c> path segments
/// after a normal path segment.
/// </remarks>
public class InvalidRelativeUrlException(Uri uri, string relativePath) : Exception
{
    /// <summary>
    /// The relative path that failed
    /// </summary>
    public string RelativePath => relativePath;

    /// <summary>
    /// The uri that was being modified
    /// </summary>
    public Uri Uri => uri;

    /// <inheritdoc/>
    public override string Message => $"Invalid relative path of '{relativePath}' for URI '{Uri}'. '.' or '..' segments must always be at the start.";
}