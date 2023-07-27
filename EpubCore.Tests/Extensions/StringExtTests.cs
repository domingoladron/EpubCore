using EpubCore.Extensions;
using FluentAssertions;
using Xunit;

namespace EpubCore.Tests.Extensions
{
    public class StringExtTests
    {
        [Fact]
        public void CanGetAbsolutePathInIdealScenario()
        {
            AssertionExtensions.Should((string)"file.txt".ToAbsolutePath("one/two/")).Be("one/two/file.txt");
            AssertionExtensions.Should((string)"file.txt".ToAbsolutePath("/one/two/")).Be("/one/two/file.txt");
        }

        [Fact]
        public void CanGetAbsolutePathByTrimmingPathFilename()
        {
            AssertionExtensions.Should((string)"file.txt".ToAbsolutePath("one/two")).Be("one/file.txt");
            AssertionExtensions.Should((string)"file.txt".ToAbsolutePath("/one/two")).Be("/one/file.txt");
        }

        [Fact]
        public void CanGetAbsolutePathFromFileAndFile()
        {
            AssertionExtensions.Should((string)"bar.txt".ToAbsolutePath("foo.txt")).Be("/bar.txt");
            AssertionExtensions.Should((string)"bar.txt".ToAbsolutePath("/one/foo.txt")).Be("/one/bar.txt");
            AssertionExtensions.Should((string)"/one/bar.txt".ToAbsolutePath("foo.txt")).Be("/one/bar.txt");
            AssertionExtensions.Should((string)"two/bar.txt".ToAbsolutePath("/one/foo.txt")).Be("/one/two/bar.txt");
            AssertionExtensions.Should((string)"/two/bar.txt".ToAbsolutePath("/one/foo.txt")).Be("/two/bar.txt");
        }

        [Fact]
        public void CanGetAbsolutePathForRelativeFile()
        {
            AssertionExtensions.Should((string)"./foo.txt".ToAbsolutePath("/one/")).Be("/one/foo.txt");
            AssertionExtensions.Should((string)"./two/foo.txt".ToAbsolutePath("/one/")).Be("/one/two/foo.txt");
            AssertionExtensions.Should((string)"../foo.txt".ToAbsolutePath("/one/")).Be("/foo.txt");
            AssertionExtensions.Should((string)"../two/foo.txt".ToAbsolutePath("/one/")).Be("/two/foo.txt");
        }
    }
}
