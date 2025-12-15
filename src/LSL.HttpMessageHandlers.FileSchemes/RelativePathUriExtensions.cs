using System;
using System.Collections.Generic;
using System.Linq;
using LSL.HttpMessageHandlers.FileSchemes.Infrastructure;

namespace LSL.HttpMessageHandlers.FileSchemes;

/// <summary>
/// Relative path uri extensions
/// </summary>
public static class RelativePathUriExtensions
{
    /// <summary>
    /// Set the Uri path relatively to the existing path
    /// </summary>
    /// <example>
    /// new Uri("http://nowhere.com/asd").SetRelativePath("../test/file.txt")
    /// </example>
    /// <param name="source"></param>
    /// <param name="relativePath"></param>
    /// <param name="ignoreFileLikeLastSegment"></param>
    /// <returns></returns>
    public static Uri SetRelativePath(this Uri source, string relativePath, bool ignoreFileLikeLastSegment = true)
    {
        var builder = new UriBuilder(source.AssertNotNull(nameof(source)));
        var relativePathSegments = relativePath.AssertNotNull(nameof(relativePath)).SplitUriPath();        
        var originalPathSegments = source.AbsolutePath.SplitUriPath();

        if (ignoreFileLikeLastSegment && originalPathSegments.TryGetLastItem(out var lastElement) && lastElement.IsLikeAFileName())
        {
            originalPathSegments = [.. originalPathSegments.AsEnumerable().Take(originalPathSegments.Length - 1)];
        }

        var newPaths = relativePathSegments.Aggregate(
            new PathAggregation(originalPathSegments, false, source, relativePath),
            (agg, i) =>
                i switch
                {
                    "." => agg.AssertNotConcatenated(),
                    ".." => agg.AssertNotConcatenated() with { Segments = agg.Segments.Skip(1) },
                    _ => agg with { Segments = agg.Segments.Concat([i]), HasConcatenated = true }
                }
            );

        builder.Path = string.Join("/", newPaths.Segments);

        return builder.Uri;        
    }

    internal static string[] SplitUriPath(this string source) => source.Split(['/'], StringSplitOptions.RemoveEmptyEntries);

    private readonly record struct PathAggregation(IEnumerable<string> Segments, bool HasConcatenated, Uri SourceUri, string RelativePath)
    {
        public PathAggregation AssertNotConcatenated() => HasConcatenated 
            ? throw new InvalidRelativeUrlException(SourceUri, RelativePath)
            : this;
    }
}