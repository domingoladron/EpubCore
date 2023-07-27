using EpubCore.Cli.ActionHandlers;

namespace EpubCore.Cli.Models;

/// <summary>
/// A model for storing the basic data of what's inside the Epub 
/// </summary>
public class GetEpubDetails
{
    public string UniqueIdentifier { get; set; } = null!;

    public List<string> Titles { get; set; } = null!;
    public List<string> Authors { get; set; } = null!;
    public List<string> Contributors { get; set; } = null!;

    public List<string> Publishers { get; set; } = null!;

    public List<string> TableOfContents { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string CoverImage { get; set; } = null!;

    public List<string> Images { get; set; } = null!;

    public List<string> Css { get; set; } = null!;

    public List<string> Fonts { get; set; } = null!;

    public List<string> Html { get; set; } = null!;

    public GetEpubDetails()
    {
        
    }
    public GetEpubDetails(EpubBook book, ICollection<GetEpubFilterKey>? filterKeys)
    {
        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.UniqueIdentifier))
        {
            UniqueIdentifier = book.UniqueIdentifier;
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Titles))
        {
            Titles = book.Titles.ToList();
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Authors))
        {
            Authors = book.Authors.ToList();
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Publishers))
        {
            Publishers = book.Publishers.ToList();
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Version))
        {
            Version = book.EpubVersion.ToString();
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Cover))
        {
            CoverImage = book.CoverImageHref;
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Css))
        {
            Css = new List<string>();
            foreach (var curCss in book.Resources.Css)
            {
                Css.Add(curCss.FileName);
            }
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Html))
        {
            Html = new List<string>();
            foreach (var curHtml in book.Resources.Html)
            {
                Html.Add(curHtml.FileName);
            }
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Fonts))
        {
            Fonts = new List<string>();
            foreach (var curFont in book.Resources.Fonts)
            {
                Fonts.Add(curFont.Href);
            }
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Images))
        {
            Images = new List<string>();
            foreach (var curImage in book.Resources.Images)
            {
                Images.Add(curImage.Href);
            }
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Contributors))
        {
            Contributors = new List<string>();
            foreach (var curContributor in book.Contributors)
            {
                Contributors.Add(curContributor.Text);
            }
        }

        if (ShouldBeIncluded(filterKeys, GetEpubFilterKey.Toc))
        {
            TableOfContents = new List<string>();
            foreach (var curItem in book.TableOfContents)
            {
                TableOfContents.Add(curItem.Title);
            }
        }
    }

    private bool ShouldBeIncluded(ICollection<GetEpubFilterKey>? getEpubFilterKeys, GetEpubFilterKey key)
    {
        if (getEpubFilterKeys == null || !getEpubFilterKeys.Any()) return true;
        return (getEpubFilterKeys.Contains(key));
    }
}