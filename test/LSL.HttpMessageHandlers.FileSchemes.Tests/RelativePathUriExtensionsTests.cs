using System;
using FluentAssertions;

namespace LSL.HttpMessageHandlers.FileSchemes.Tests;

public class RelativePathUriExtensionsTests
{
    [TestCase(true, "http://nowhere.com/", "../test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(true, "http://nowhere.com/a-path", "../test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(true, "http://nowhere.com/a-path", "../../test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(true, "http://nowhere.com/a-path/a-file.txt", "test/file.txt", "http://nowhere.com/a-path/test/file.txt")]
    [TestCase(true, "http://nowhere.com/a-path/a-file.txt", "./test/file.txt", "http://nowhere.com/a-path/test/file.txt")]

    [TestCase(false, "http://nowhere.com/", "../test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path", "../test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path", "../../test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path/a-file.txt", "test/file.txt", "http://nowhere.com/a-path/a-file.txt/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path/a-file.txt", "./test/file.txt", "http://nowhere.com/a-path/a-file.txt/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path/.", "./test/file.txt", "http://nowhere.com/a-path/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path/..", "./test/file.txt", "http://nowhere.com/test/file.txt")]
    [TestCase(false, "http://nowhere.com/a-path", "./test/file.txt", "http://nowhere.com/a-path/test/file.txt")]
    public void SetRelativePath_ShouldChangeThePathAsExpected(bool excludeFileNames, string uri, string relativePath, string expectedUri)
    {
        new Uri(uri).SetRelativePath(relativePath, excludeFileNames).Should().Be(new Uri(expectedUri));
    }

    [Test]
    public void SetRelativePath_ShouldFailIfGivenAnInvalidRelativePath()
    {
        new Action(() => new Uri("http://nowhere.com").SetRelativePath("test/../file.txt"))
            .Should()
            .ThrowExactly<InvalidRelativeUrlException>()
            .WithMessage("Invalid relative path of 'test/../file.txt' for URI 'http://nowhere.com/'. '.' or '..' segments must always be at the start.")
            .And
            .Should().BeEquivalentTo(new
            {
                Uri = new Uri("http://nowhere.com"),
                RelativePath = "test/../file.txt"
            });
    }
}