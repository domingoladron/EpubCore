using Penman.EpubSharp;

var pathToEPubToRead = $"{AppContext.BaseDirectory}/siddhartha-hermanhesse.epub";

var book = EpubReader.Read(pathToEPubToRead);

var uniqueIdentifier = book.UniqueIdentifier;
var title = book.Title;
var authors = book.Authors;
var publishers = book.Publishers;

var cover = book.CoverImage;
var coverImageName = book.CoverImageHref;

// Get table of contents
var chapters = book.TableOfContents;

// Get contained files
var htmlFiles = book.Resources.Html;
var css = book.Resources.Css;
var images = book.Resources.Images;
var fonts = book.Resources.Fonts;

foreach (var curHtml in htmlFiles)
{
    Console.WriteLine(curHtml.FileName);

}