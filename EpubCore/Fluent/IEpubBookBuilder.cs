using System.Collections.Generic;
using System.IO;
using EpubCore.Format;

namespace EpubCore.Fluent
{
    /// <summary>
    /// A Builder for fluently constructing an Epub Book 
    /// </summary>
    public interface IEpubBookBuilder
    {
        IEpubBookBuilder WithVersion(EpubVersion version);
        IEpubBookBuilder WithUniqueIdentifier(string uniqueIdentifier);
        IEpubBookBuilder WithTitle(string title);
        IEpubBookBuilder WithLanguage(string language);
        IEpubBookBuilder AddAuthor(string authorName);

        IEpubBookBuilder AddAuthors(List<string> authorNames);

        IEpubBookBuilder AddPublisher(string publisherName);

        IEpubBookBuilder AddPublishers(List<string> publishers);
        IEpubBookBuilder AddChapter(string title, string chapterContents);

        IEpubBookBuilder AddChapter(string title, string chapterContents, string fileId);

        IEpubBookBuilder AddStylesheet(string stylesheetName, string stylesheetContents);

        IEpubBookBuilder AddStylesheet(string stylesheetName, byte[] stylesheetContents);

        IEpubBookBuilder AddBookCover(string imageName, byte[] imageContents);

        IEpubBookBuilder AddJpg(string imageName, byte[] imageContents);

        IEpubBookBuilder AddGif(string imageName, byte[] imageContents);

        IEpubBookBuilder AddPng(string imageName, byte[] imageContents);

        IEpubBookBuilder AddSvg(string imageName, byte[] imageContents);

        IEpubBookBuilder AddTrueTypeFont(string fontFileName, byte[] fontContents);

        IEpubBookBuilder AddOpenTypeFont(string fontFileName, byte[] fontContents);

        IEpubBookBuilder AddFileContent(string fileName, string fileContents, EpubContentType type);

        IEpubBookBuilder AddFileContent(string fileName, byte[] fileContents, EpubContentType type);

        void Build(string savePath);

        void Build(Stream stream);
    }
}