using System.Collections.Generic;
using System.IO;
using EpubCore.Format;

namespace EpubCore.Fluent
{
    public class EpubBookBuilder : IEpubBookBuilder
    {
        private readonly IEpubWriter _writer;

        public EpubBookBuilder(IEpubWriter writer)
        {
            _writer = writer;
        }

        public IEpubBookBuilder WithVersion(EpubVersion version)
        {
            _writer.SetVersion(version);
            return this;
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

        public IEpubBookBuilder WithLanguage(string language)
        {
            _writer.AddLanguage(language);
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
                if (string.IsNullOrEmpty(curAuthorName))
                    continue;

                _writer.AddAuthor(curAuthorName);
            }

            return this;
        }

        public IEpubBookBuilder AddPublisher(string publisherName)
        {
            if (string.IsNullOrEmpty(publisherName))
                return this;

            _writer.AddPublisher(publisherName);
            return this;
        }

        public IEpubBookBuilder AddPublishers(List<string> publishers)
        {
            foreach (var curPublisherName in publishers)
            {
                if (string.IsNullOrEmpty(curPublisherName))
                    continue;
                _writer.AddPublisher(curPublisherName);
            }

            return this;
        }


        public IEpubBookBuilder AddChapter(string title, string chapterContents, string fileId)
        {
            _writer.AddChapter(title, chapterContents, fileId);
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

        public IEpubBookBuilder AddBookCover(string imageName, byte[] imageContents)
        {
            _writer.SetCover(imageContents, _writer.GetContentTypeForImageName(imageName));
            return this;
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
            return new EpubBookBuilder(new EpubWriter());
        }
    }
}