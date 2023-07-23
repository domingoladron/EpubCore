using JasperFx.Core;

namespace Penman.EpubSharp.Cli.Retrievers;

public class EpubResourceRetriever : IEpubResourceRetriever
{
    public EpubTextFile? RetrieveCss(EpubBook book, string cssFileName)
    {
        return book.Resources.FindExistingStylesheet(cssFileName);
    }

    public EpubTextFile? RetrieveHtml(EpubBook book, string htmlFileName)
    {
        return book.Resources.FindExistingHtml(htmlFileName);
    }
}