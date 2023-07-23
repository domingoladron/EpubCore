using JasperFx.Core;

namespace Penman.EpubSharp.Cli.Retrievers;

public class EpubResourceRetriever : IEpubResourceRetriever
{
    public EpubTextFile? RetrieveHtml(EpubBook book, string htmlFileName)
    {
        return book.Resources.Html.FirstOrDefault(g => g.FileName.EqualsIgnoreCase(htmlFileName));
    }
}