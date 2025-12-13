[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-httpmessagehandlers-fileschemes.svg)](https://ci.appveyor.com/project/alunacjones/lsl-httpmessagehandlers-fileschemes)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.HttpMessageHandlers.FileSchemes)](https://coveralls.io/github/alunacjones/LSL.HttpMessageHandlers.FileSchemes)
[![NuGet](https://img.shields.io/nuget/v/LSL.HttpMessageHandlers.FileSchemes.svg)](https://www.nuget.org/packages/LSL.HttpMessageHandlers.FileSchemes/)

# LSL.HttpMessageHandlers.FileSchemes

This library provides a `DelegatingHandler` to be used with `HttpClient`'s to handle the `file:` URI scheme.

## Registering the message handler

```csharp
services.AddHttpClient<MyClient>(c => c.AddFileSchemeMessageHandler());
```

<!-- HIDE -->
## Further Documentation

More in-depth documentation can be found [here](https://alunacjones.github.io/LSL.HttpMessageHandlers.FileSchemes/)
<!-- END:HIDE -->