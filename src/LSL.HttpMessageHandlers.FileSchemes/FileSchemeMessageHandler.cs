using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LSL.HttpMessageHandlers.FileSchemes;

/// <summary>
/// File scheme <see cref="DelegatingHandler"/>
/// </summary>
internal class FileSchemeMessageHandler(StreamContentHeaderProvider streamContentHeaderProvider) : DelegatingHandler
{
    /// <inheritdoc/>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
        (request.RequestUri.Scheme, request.RequestUri.LocalPath) switch
        {
            ("file", string path) => CreateFileResult(path),
            _ => base.SendAsync(request, cancellationToken)
        };

    private Task<HttpResponseMessage> CreateFileResult(string path) => 
        Task.FromResult<HttpResponseMessage>(
            File.Exists(path)
                ? new(HttpStatusCode.OK) { Content = streamContentHeaderProvider.AddHeaders(new StreamContent(File.OpenRead(path)), path) }
                : new(HttpStatusCode.NotFound)
        );
}
