namespace Penman.EpubSharp.Cli.Retrievers;

/// <summary>
/// Retrieves an html file from the Epub in question if it can
/// </summary>
public interface IEpubResourceRetriever
{
    EpubTextFile? RetrieveCss(EpubBook book, string cssFileName);
    EpubTextFile? RetrieveHtml(EpubBook book, string htmlFileName);
}