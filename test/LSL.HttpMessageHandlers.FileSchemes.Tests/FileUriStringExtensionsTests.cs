using FluentAssertions;

namespace LSL.HttpMessageHandlers.FileSchemes.Tests;

public class FileUriStringExtensionsTests
{
    [TestCase(@"c:\temp\file.txt", "file:///c:/temp/file.txt")]
    [TestCase(@"/temp/file.txt", "file:///temp/file.txt")]
    public void ToFileUriString_GivenAFilePath_ItShouldProduceTheExpectedResult(string filePath, string expectedResult)
    {
        filePath.ToFileUriString().Should().Be(expectedResult);
    }

    [TestCase("a-path", false)]
    [TestCase(".", false)]
    [TestCase("..", false)]
    [TestCase(null, false)]
    [TestCase("file.txt", true)]
    public void IsLikeAFileName_GivenAString_ItShouldReturnTheExpectedResult(string path, bool expectedResult)
    {
        path.IsLikeAFileName().Should().Be(expectedResult);
    }
}