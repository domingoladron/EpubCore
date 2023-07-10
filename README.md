# Penman.EpubSharp
.NET 6 / .NET Standard 2.0 library for reading and writing EPUB files.  An upgrade of [asido/EpubSharp](https://github.com/asido/EpubSharp)

Supported EPUB versions: **2.0**, **3.0**, **3.1**

# Installation

```
Install-Package Penman.EpubSharp
```

# Supported Frameworks
`.NET6.0`, `NETSTANDARD2.0`

# Usage

## Reading an EPUB

```cs
// Read an epub file
EpubBook book = EpubReader.Read("my.epub");

// Read metadata
string title = book.Title;
string[] authors = book.Authors;
Image cover = book.CoverImage;

// Get table of contents
ICollection<EpubChapter> chapters = book.TableOfContents;

// Get contained files
ICollection<EpubTextFile> html = book.Resources.Html;
ICollection<EpubTextFile> css = book.Resources.Css;
ICollection<EpubByteFile> images = book.Resources.Images;
ICollection<EpubByteFile> fonts = book.Resources.Fonts;

// Convert to plain text
string text = book.ToPlainText();

// Access internal EPUB format specific data structures.
EpubFormat format = book.Format;
OcfDocument ocf = format.Ocf;
OpfDocument opf = format.Opf;
NcxDocument ncx = format.Ncx;
NavDocument nav = format.Nav;

// Create an EPUB
EpubWriter.Write(book, "new.epub");
```

## Writing an EPUB
_**Editing capabilities are currently very limited and might not work at all. 
Use it at your own risk. The next release will bring a true write support.**_
```cs
EpubWriter writer = new EpubWriter();

writer.AddAuthor("Foo Bar");
writer.SetCover(imgData, ImageFormat.Png);

writer.Write("new.epub");
```

## Epub Book Builder

Use the fluent EpubBookBuilder to create your Epub.  Much cleaner syntax and easier to understand.

```cs
var pathToSaveEPub = "~/myepub.epub";
var uniqueIdentifier = Guid.NewGuid().ToString();
var title = "The Title of my EBook";
var author = "Domingo Ladron";

var builder = EpubBookBuilder.Create();

builder
    .WithTitle(title)
    .WithUniqueIdentifier(UniqueIdentifier)
    .AddAuthor(author)
    .AddChapter("Chapter 1", "<html><body><h1>It was a dark and stormy night.</h1></body></html>")
    .Build(pathToSaveEPub);
```
