using System;
using System.IO;
using System.Linq;
using Penman.EpubSharp.Fluent;
using Xunit;

namespace Penman.EpubSharp.Tests.FluentTests;

public class EpubBookBuilderTests 
{
    public static string UniqueIdentifier = Guid.NewGuid().ToString();
    protected string WriteLocation = string.Empty;
    public EpubBookBuilderTests()
    {
        ConfigureTempPath();
    }

    [Fact]
    public void CanConstructFromTitleAndUniqueIdentifier()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var title = Faker.Lorem.Sentence(2);
        builder.WithTitle(title)
            .WithUniqueIdentifier(UniqueIdentifier)
            .Build(stream);

        var epub = EpubReader.Read(stream, false);

        Assert.NotNull(epub);
        Assert.Equal(title, epub.Title);
        Assert.Equal(UniqueIdentifier, epub.UniqueIdentifier);
    }

    [Fact]
    public void CanConstructWithAuthor()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var author = Faker.Name.FullName();

        builder.AddAuthor(author)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var foundAuthor = epub.Authors.FirstOrDefault(g => g.Equals(author));

        Assert.NotNull(foundAuthor);
    }

    [Fact]
    public void CanConstructWithPublisher()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var publisherName = Faker.Name.FullName();

        builder.AddPublisher(publisherName)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var foundPublisher = epub.Publishers.FirstOrDefault(g => g.Equals(publisherName));

        Assert.NotNull(foundPublisher);
    }



    private void ConfigureTempPath()
    {
        WriteLocation = Path.Join(Path.GetTempPath(), "Penman.EpubSharp.Tests", "EpubBookBuilderTests");
        if (!Directory.Exists(WriteLocation))
            Directory.CreateDirectory(WriteLocation);
    }
}