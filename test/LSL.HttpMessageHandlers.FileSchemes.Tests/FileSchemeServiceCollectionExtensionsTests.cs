using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Diamond.Core.System.TemporaryFolder;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace LSL.HttpMessageHandlers.FileSchemes.Tests;

public class FileSchemeServiceCollectionExtensionsTests : BaseTest
{
    [Test]
    public async Task GivenAFileScheme_ItShouldReturnTheExpectedStream()
    {
        await RunTest(async client =>
        {
            // Arrange
            using var tempFolder = new TemporaryFolderFactory().Create();
            var fileContent = "Hello";
            var filePath = Path.Combine(tempFolder.FullPath, "temp.txt");

            await File.WriteAllTextAsync(filePath, fileContent);

            // Act
            var response = await client.GetAsync(filePath.ToFileUri());

            // Assert
            using var assertionScope = new AssertionScope();

            response.Should().BeSuccessful();
            (await response.Content.ReadAsStringAsync()).Should().Be(fileContent);
        });
    }

    [Test]
    public async Task GivenAFileSchemeForANonExistentFile_ItShouldReturnNotFound()
    {
        await RunTest(async client =>
        {
            // Arrange
            using var tempFolder = new TemporaryFolderFactory().Create();
            var filePath = Path.Combine(tempFolder.FullPath, "temp.txt");

            // Act
            var response = await client.GetAsync(filePath.ToFileUri());

            // Assert
            using var assertionScope = new AssertionScope();

            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        });
    }

    [Test]
    public async Task GivenANonFileUri_ItShouldDelegateTheActionAndReturnOk()
    {
        await RunTest(async client =>
        {
            // Act
            var response = await client.GetAsync(new Uri("https://example.com/"));

            // Assert
            using var assertionScope = new AssertionScope();

            response.Should().BeSuccessful();
        });        
    }

    [Test]
    public void GivenANullHttpClientBuilder_ItShouldThrow()
    {
        new Action(() => ((IHttpClientBuilder)null!).AddFileSchemeMessageHandler())
            .Should()
            .ThrowExactly<ArgumentNullException>();
    }
}
