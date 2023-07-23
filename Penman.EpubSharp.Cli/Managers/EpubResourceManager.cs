using JasperFx.Core;

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

    public bool RemoveResource(EpubBook book, EpubWriter epubWriter, string resourceName, EpubResourceType resourceType)
    {
        // Handle the cover page removal as well if that's the
        // image being removed
        if (resourceType.Equals(EpubResourceType.Image) && book.CoverImageHref.EqualsIgnoreCase(resourceName))
        {
            epubWriter.RemoveCover();
            return true;
        }

        return book.Resources.RemoveResource(resourceName, resourceType);
    }
}