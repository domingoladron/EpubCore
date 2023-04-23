using System.Collections.Generic;
using System.IO;
using Penman.EpubSharp.Format;

namespace Penman.EpubSharp.Fluent
{
    /// <summary>
    /// A Builder for fluently constructing an Epub Book 
    /// </summary>
    public interface IEpubBookBuilder
    {
        IEpubBookBuilder WithTitle(string title);
        IEpubBookBuilder AddAuthor(string authorName);

        IEpubBookBuilder AddAuthors(List<string> authorNames);
        IEpubBookBuilder AddChapter(string title, string chapterContents);

        IEpubBookBuilder AddStylesheet(string stylesheetName, string stylesheetContents);

        IEpubBookBuilder AddStylesheet(string stylesheetName, byte[] stylesheetContents);

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