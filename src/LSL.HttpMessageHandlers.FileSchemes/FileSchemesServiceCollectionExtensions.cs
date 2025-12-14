using System;
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
        var services = source.AssertNotNull(nameof(source)).Services;
        services.TryAddTransient<FileSchemeMessageHandler>();
        services.TryAddSingleton<StreamContentHeaderProvider>();
        services.TryAddSingleton<MimeTypeProvider>();
        
        return source.AddHttpMessageHandler<FileSchemeMessageHandler>();
    }

    /// <summary>
    /// Configures the options for all file scheme message handlers
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureFileSchemeMessageHandler(this IServiceCollection source, Action<FileSchemeHandlerOptions> configurator) =>
        source.AssertNotNull(nameof(source)).Configure(configurator.AssertNotNull(nameof(configurator)));
}