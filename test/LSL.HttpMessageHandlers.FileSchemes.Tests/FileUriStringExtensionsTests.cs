using FluentAssertions;

namespace LSL.HttpMessageHandlers.FileSchemes.Tests;

public class FileUriStringExtensionsTests
{
    [TestCase(@"c:\temp\file.txt", "file:///c:/temp/file.txt")]
    [TestCase(@"/temp/file.txt", "file:///temp/file.txt")]
    public void GivenAFilePath_ItShouldProduceTheExpectedResult(string filePath, string expectedResult)
    {
        filePath.ToFileUriString().Should().Be(expectedResult);
    }
}