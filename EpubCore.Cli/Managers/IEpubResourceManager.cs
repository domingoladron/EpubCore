namespace EpubCore.Cli.Managers;

/// <summary>
/// Retrieves an html file from the Epub in question if it can
/// </summary>
public interface IEpubResourceManager
{
    EpubTextFile? RetrieveCss(EpubBook book, string cssFileName);
    EpubTextFile? RetrieveHtml(EpubBook book, string htmlFileName);

    bool RemoveResource(EpubBook book, EpubWriter epubWriter, string resourceName, EpubResourceType resourceType);

    bool AddResource(EpubBook book, EpubWriter epubWriter, EpubFile epubFileToAdd, string? addBeforeThisResource, EpubResourceType resourceType);
   
}