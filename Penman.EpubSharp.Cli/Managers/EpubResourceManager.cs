namespace Penman.EpubSharp.Cli.Managers;

public class EpubResourceManager : IEpubResourceManager
{
    public EpubTextFile? RetrieveCss(EpubBook book, string cssFileName)
    {
        return book.Resources.FindExistingStylesheet(cssFileName);
    }

    public EpubTextFile? RetrieveHtml(EpubBook book, string htmlFileName)
    {
        return book.Resources.FindExistingHtml(htmlFileName);
    }

    public bool RemoveHtml(EpubBook book, string htmlFileName)
    {
        return book.Resources.RemoveHtml(htmlFileName);
    }
    public bool RemoveCss(EpubBook book, string cssFileName)
    {
        return book.Resources.RemoveCss(cssFileName);
    }
}