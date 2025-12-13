using LSL.HttpMessageHandlers.FileSchemes;
using LSL.HttpMessageHandlers.FileSchemes.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// File schemes service collection extensions
/// </summary>
public static class FileSchemesServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="FileSchemeMessageHandler"/>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddFileSchemeMessageHandler(this IHttpClientBuilder source)
    {
        source.AssertNotNull(nameof(source)).Services.TryAddTransient<FileSchemeMessageHandler>();
        return source.AddHttpMessageHandler<FileSchemeMessageHandler>();
    }
}