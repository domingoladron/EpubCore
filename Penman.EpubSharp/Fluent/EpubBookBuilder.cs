using System.Collections.Generic;
using System.IO;
using Penman.EpubSharp.Format;

namespace Penman.EpubSharp.Fluent
{
    public class EpubBookBuilder : IEpubBookBuilder
    {
        private readonly IEpubWriter _writer;

        public EpubBookBuilder(IEpubWriter writer)
        {
            _writer = writer;
        }

        public IEpubBookBuilder WithUniqueIdentifier(string uniqueIdentifier)
        {
            _writer.SetUniqueIdentifier(uniqueIdentifier);
            return this;
        }

        public IEpubBookBuilder WithTitle(string title)
        {
            _writer.SetTitle(title);
            return this;
        }

        public IEpubBookBuilder AddAuthor(string authorName)
        {
            _writer.AddAuthor(authorName);
            return this;
        }

        public IEpubBookBuilder AddAuthors(List<string> authorNames)
        {
            foreach (var curAuthorName in authorNames)
            {
                _writer.AddAuthor(curAuthorName);
            }

            return this;
        }

        public IEpubBookBuilder AddPublisher(string publisherName)
        {
            _writer.AddPublisher(publisherName);
            return this;
        }

        public IEpubBookBuilder AddPublishers(List<string> publishers)
        {
            foreach (var curPublisherName in publishers)
            {
                _writer.AddPublisher(curPublisherName);
            }

            return this;
        }

        public IEpubBookBuilder AddChapter(string title, string chapterContents)
        {
            _writer.AddChapter(title, chapterContents);
            return this;
        }

        public IEpubBookBuilder AddStylesheet(string stylesheetName, string stylesheetContents)
        {
            return AddFileContent(stylesheetName, stylesheetContents, EpubContentType.Css);
        }

        public IEpubBookBuilder AddStylesheet(string stylesheetName, byte[] stylesheetContents)
        {
            return AddFileContent(stylesheetName, stylesheetContents, EpubContentType.Css);
        }

        public IEpubBookBuilder AddJpg(string imageName, byte[] imageContents)
        {
            return AddFileContent(imageName, imageContents, EpubContentType.ImageJpeg);
        }

        public IEpubBookBuilder AddGif(string imageName, byte[] imageContents)
        {
            return AddFileContent(imageName, imageContents, EpubContentType.ImageGif);
        }

        public IEpubBookBuilder AddPng(string imageName, byte[] imageContents)
        {
            return AddFileContent(imageName, imageContents, EpubContentType.ImagePng);
        }

        public IEpubBookBuilder AddSvg(string imageName, byte[] imageContents)
        {
            return AddFileContent(imageName, imageContents, EpubContentType.ImageSvg);
        }

        public IEpubBookBuilder AddTrueTypeFont(string fontFileName, byte[] fontContents)
        {
            return AddFileContent(fontFileName, fontContents, EpubContentType.FontTruetype);
        }

        public IEpubBookBuilder AddOpenTypeFont(string fontFileName, byte[] fontContents)
        {
            return AddFileContent(fontFileName, fontContents, EpubContentType.FontOpentype);
        }

        public IEpubBookBuilder AddFileContent(string fileName, string fileContents, EpubContentType type)
        {
            _writer.AddFile(fileName, fileContents, type);
            return this;
        }

        public IEpubBookBuilder AddFileContent(string fileName, byte[] fileContents, EpubContentType type)
        {
            _writer.AddFile(fileName, fileContents, type);
            return this;
        }

        public void Build(string savePath)
        {
            _writer.Write(savePath);
        }

        public void Build(Stream stream)
        {
            _writer.Write(stream);
        }

        public static EpubBookBuilder Create()
        {
            return new EpubBookBuilder(new EpubWriter(new EpubBook()));
        }
    }
}