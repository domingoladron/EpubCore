using Penman.EpubSharp;


// An example of reading from an epub.

var pathToEPubToRead = $"{AppContext.BaseDirectory}/siddhartha-hermanhesse.epub";

var book = EpubReader.Read(pathToEPubToRead);

var uniqueIdentifier = book.UniqueIdentifier;
var title = book.Title;
var authors = book.Authors;
var publishers = book.Publishers;
var coverImageName = book.CoverImageHref;

// Get table of contents
var chapters = book.TableOfContents;

// Get contained files
var css = book.Resources.Css;
var cssFileNames = css.Select(cssFile => cssFile.FileName).ToList();
var fonts = book.Resources.Fonts;



Console.WriteLine($"Book Title: {title.Trim()}");
Console.WriteLine($"Book Unique Identifier: {uniqueIdentifier}");
Console.WriteLine($"Book Authors: {string.Join(",", authors)}");
Console.WriteLine($"Book Publishers: {string.Join(",", publishers)}");
Console.WriteLine($"Book Cover file name: {coverImageName}");
Console.WriteLine($"Book Css Files: {string.Join(",", cssFileNames)}");
Console.WriteLine($"Book Fonts: {string.Join(",", fonts)}");
Console.WriteLine("Chapters");
foreach (var curChapter in chapters)
{
    Console.WriteLine($"Chapter Title: {curChapter.Title}");
}