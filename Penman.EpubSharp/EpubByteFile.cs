namespace Penman.EpubSharp;

public class EpubByteFile : EpubFile
{
    internal EpubTextFile ToTextFile()
    {
        return new EpubTextFile
        {
            Content = Content,
            ContentType = ContentType,
            AbsolutePath = AbsolutePath,
            Href = Href,
            MimeType = MimeType
        };
    }
}