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



Console.WriteLine($"Book Title: {title}");
Console.WriteLine($"Book Authors: {string.Join(",", authors)}");
Console.WriteLine($"Book Publishers: {string.Join(",", publishers)}");
foreach (var curChapter in chapters)
{
    Console.WriteLine($"Chapter Title: {curChapter.Title}");
    Console.WriteLine( $"Chapter Content Type: {curChapter.LinkedResource?.ContentType}");
}