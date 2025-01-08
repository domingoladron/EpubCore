using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EpubCore.Fluent;
using EpubCore.Format;
using Xunit;

namespace EpubCore.Tests.FluentTests;

public class EpubBookBuilderTests 
{
    public static string UniqueIdentifier = Guid.NewGuid().ToString();
    protected string WriteLocation = string.Empty;
    protected Dictionary<string, string> Chapters = new();
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
    public void CanBuildFromTitleAndUniqueIdentifier()
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
    public void CanBuildWithAuthor()
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
    public void CanBuildWithPublisher()
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
    public void CanBuildWithChapters()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
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
            var curHtml = epub.FetchHtmlFileForChapter(curEPubChapter);
            Assert.NotNull(curHtml);
            var fileContents = curHtml.TextContent;
            var curChapterKey = Chapters.Keys.FirstOrDefault(c => c.Equals(curEPubChapter.Title));

            Assert.NotNull(curChapterKey);
            var curChapterContent = Chapters[curChapterKey];

            Assert.NotNull(curChapterContent);
            Assert.Equal(curChapterContent, fileContents);
        }
    }

    [Fact]
    public void CanBuildWithStylesheet()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var stylesheetName = Faker.Lorem.GetFirstWord();
        var stylesheetContents = Faker.Lorem.Paragraph();

        builder
            .AddStylesheet(stylesheetName, stylesheetContents)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var stylesheetFound = epub.Resources.Css.FirstOrDefault();
        Assert.NotNull(stylesheetFound);

        Assert.Equal(stylesheetFound.FileName, stylesheetName);
        Assert.Equal(stylesheetFound.TextContent, stylesheetContents);
    }


    [Fact]
    public void CanBuildWithJpg()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var imageName = Faker.Lorem.GetFirstWord();
        
        var fileBytes = GetFileBytes("Samples/Cover.jpg");

        builder
            .AddJpg(imageName, fileBytes)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var imageFound = epub.Resources.Images.FirstOrDefault();
        Assert.NotNull(imageFound);
        Assert.Equal(EpubContentType.ImageJpeg, imageFound.ContentType);
        Assert.Equal(imageFound.Href, imageName);
        Assert.Equal(imageFound.Content, fileBytes);
    }


    [Fact]
    public void CanBuildWithPng()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var imageName = Faker.Lorem.GetFirstWord();

        var fileBytes = GetFileBytes("Samples/some.png");

        builder
            .AddPng(imageName, fileBytes)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var imageFound = epub.Resources.Images.FirstOrDefault();
        Assert.NotNull(imageFound);
        Assert.Equal(EpubContentType.ImagePng, imageFound.ContentType);
        Assert.Equal(imageFound.Href, imageName);
        Assert.Equal(imageFound.Content, fileBytes);
    }


    [Fact]
    public void CanBuildWithOpenTypeFont()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var fontName = Faker.Lorem.GetFirstWord();

        var fileBytes = GetFileBytes("Samples/opentype.otf");

        builder
            .AddOpenTypeFont(fontName, fileBytes)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var imageFound = epub.Resources.Fonts.FirstOrDefault();
        Assert.NotNull(imageFound);
        Assert.Equal(EpubContentType.FontOpentype, imageFound.ContentType);
        Assert.Equal(imageFound.Href, fontName);
        Assert.Equal(imageFound.Content, fileBytes);
    }

    [Fact]
    public void CanBuildWithTrueTypeFont()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var fontName = Faker.Lorem.GetFirstWord();

        var fileBytes = GetFileBytes("Samples/truetype.ttf");

        builder
            .AddTrueTypeFont(fontName, fileBytes)
            .Build(stream);
        var epub = EpubReader.Read(stream, false);

        var imageFound = epub.Resources.Fonts.FirstOrDefault();
        Assert.NotNull(imageFound);
        Assert.Equal(EpubContentType.FontTruetype, imageFound.ContentType);
        Assert.Equal(imageFound.Href, fontName);
        Assert.Equal(imageFound.Content, fileBytes);
    }

    [Fact]
    public void CanBuildWithLanguage()
    {
        var builder = EpubBookBuilder.Create();
        var stream = new MemoryStream();
        var language = "en-US";
        builder.WithLanguage(language)
          .Build(stream);

        var epub = EpubReader.Read(stream, false);

        Assert.NotNull(epub);
        Assert.Equal(1, epub.Format.Opf.Metadata.Languages.Count);
        Assert.Equal(epub.Format.Opf.Metadata.Languages.First(), language);
    }

    private static byte[] GetFileBytes(string relativeApplicationPath)
    {
        var pathToFile = $"{AppContext.BaseDirectory}/{relativeApplicationPath}";
        return File.ReadAllBytes(pathToFile);
    }

    private void ConfigureTempPath()
    {
        WriteLocation = Path.Join(Path.GetTempPath(), "EpubCore.Tests", "EpubBookBuilderTests");
        if (!Directory.Exists(WriteLocation))
            Directory.CreateDirectory(WriteLocation);
    }
}