using System.IO;

namespace Penman.EpubSharp
{
    public interface IEpubWriter
    {
        void Write(EpubBook book, string epubBooKPath);
        void Write(EpubBook book, Stream stream);
    }
}