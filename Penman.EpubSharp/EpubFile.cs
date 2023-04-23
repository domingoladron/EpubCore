using Penman.EpubSharp.Format;

namespace Penman.EpubSharp
{
    public abstract class EpubFile
    {
        public string AbsolutePath { get; set; }
        public string Href { get; set; }
        public EpubContentType ContentType { get; set; }
        public string MimeType { get; set; }
        public byte[] Content { get; set; }
    }
}