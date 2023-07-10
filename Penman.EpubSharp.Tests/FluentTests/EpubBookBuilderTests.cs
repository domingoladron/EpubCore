using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Penman.EpubSharp.Fluent;
using Xunit;

namespace Penman.EpubSharp.Tests.FluentTests;

public class EpubBookBuilderTests 
{
    public static string UniqueIdentifier = Guid.NewGuid().ToString();
    protected string WriteLocation = string.Empty;
    protected Dictionary<string, string> Chapters = new Dictionary<string, string>();
    public EpubBookBuilderTests()
    {
        ConfigureTempPath();

        var numberOfChapters = new Random().Next(1, 10);

        for (var x = 0; x < numberOfChapters; x++)
        {
            var chapterName = Faker.Lorem.GetFirstWord() + x;
            var chapterContents = Faker.Lorem.Paragraph(10);

            Chapters.Add(chapterName, chapterContents);
        }


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

    [Fact]
    public void CanConstructWithChapters()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var publisherName = Faker.Name.FullName();
        foreach (var curKey in Chapters.Keys)
        {
            if(Chapters.TryGetValue(curKey, out var curChapterContents))
            {
                builder.AddChapter(curKey, curChapterContents);
            }
        }

        builder
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        Assert.Equal(epub.Resources.Html.Count, Chapters.Count);
        Assert.Equal(epub.TableOfContents.Count, Chapters.Count);

        foreach (var curEPubChapter in epub.TableOfContents)
        {
            var curHtml = epub.FetchHtmlFile(curEPubChapter);
            Assert.NotNull(curHtml);
            var fileContents = curHtml.TextContent;
            var curChapterKey = Chapters.Keys.FirstOrDefault(c => c.Equals(curEPubChapter.Title));

            var curChapterContent = Chapters[curChapterKey];

            Assert.NotNull(curChapterContent);

            Assert.Equal(curChapterContent, fileContents);
        }

    }



    private void ConfigureTempPath()
    {
        WriteLocation = Path.Join(Path.GetTempPath(), "Penman.EpubSharp.Tests", "EpubBookBuilderTests");
        if (!Directory.Exists(WriteLocation))
            Directory.CreateDirectory(WriteLocation);
    }
}