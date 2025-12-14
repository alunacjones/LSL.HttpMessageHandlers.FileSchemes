using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace LSL.HttpMessageHandlers.FileSchemes.Tests;

public class BaseTest
{
    protected static Task RunTest(Func<HttpClient, Task> testDelegate)
    {
        var serviceProvider = BuildServiceProvider();
        var httpClient = serviceProvider.GetRequiredService<HttpClient>();

        return testDelegate(httpClient);
    }

    private static IServiceProvider BuildServiceProvider()
    {
        return new ServiceCollection()
            .ConfigureFileSchemeMessageHandler(c => c.WithExtensionMimeType(".als", "text/plain"))
            .AddHttpClient()
            .ConfigureHttpClientDefaults(c => c.AddFileSchemeMessageHandler())
            .BuildServiceProvider();    
    }    
}