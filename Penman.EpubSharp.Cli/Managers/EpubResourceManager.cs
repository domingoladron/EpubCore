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

    public bool RemoveResource(EpubBook book, string resourceName, EpubResourceType resourceType)
    {
        return book.Resources.RemoveResource(resourceName, resourceType);
    }
}