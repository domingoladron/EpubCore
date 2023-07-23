namespace Penman.EpubSharp.Cli.Managers;

/// <summary>
/// Retrieves an html file from the Epub in question if it can
/// </summary>
public interface IEpubResourceManager
{
    EpubTextFile? RetrieveCss(EpubBook book, string cssFileName);
    EpubTextFile? RetrieveHtml(EpubBook book, string htmlFileName);

    bool RemoveHtml(EpubBook book, string htmlFileName);
    bool RemoveCss(EpubBook book, string cssFileName);
}