using System.IO;
using System.Xml.Linq;
using EpubCore.Format;

namespace EpubCore
{
    public interface IEpubWriter
    {
        void Write(EpubBook book, string epubBookPath);
        void Write(EpubBook book, Stream stream);
        void AddFile(string filename, byte[] content, EpubContentType type);
        void AddFile(string filename, string content, EpubContentType type);
        void InsertFileBefore(string filename, byte[] content, EpubContentType type, string beforeFileName = null);
        void InsertFileBefore(string filename, string content, EpubContentType type,
            string beforeFileName = null);
        void AddAuthor(string authorName);
        void AddPublisher(string publisherName);
        void ClearPublishers();

        void RemovePublisher(string publisherName);
        void ClearAuthors();
        void RemoveAuthor(string author);
        void RemoveTitle();
        void SetTitle(string title);
        void AddLanguage(string language);

        void SetVersion(EpubVersion version);
        void SetUniqueIdentifier(string uniqueIdentifier);
        EpubChapter AddChapter(string title, string html, string fileId = null);
        EpubChapter InsertChapterBefore(string title, string html, string fileId = null, string beforeFileName = null);
        void ClearChapters();
        string RemoveCover();
        void SetCover(byte[] data, ImageFormat imageFormat);
        byte[] Write();
        void Write(string epubBookPath);
        void Write(Stream stream);
        XElement FindNavTocOl();

        ImageFormat GetContentTypeForImageName(string imageName);
    }
}