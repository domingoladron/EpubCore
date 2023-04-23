using Penman.EpubSharp.Format;

namespace Penman.EpubSharp;

public class EpubTextFile : EpubFile
{
    public string TextContent
    {
        get => Constants.DefaultEncoding.GetString(Content, 0, Content.Length);
        set => Content = Constants.DefaultEncoding.GetBytes(value);
    }
}