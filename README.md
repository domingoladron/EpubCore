# EpubCore
![](docs/wiki/img/logo2.png)

.NET 6 / .NET Standard 2.0 library and tools for reading, writing and manipulating EPUB files.  

Supported EPUB versions: **2.0**, **3.0**, **3.1**

# Installation

```
Install-Package EpubCore
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

## EpubCore CLI
As of 1.6.0, I've released a simple but effective epub CLI.  This allows you to manipulate Epub files from the command line without needing to do any coding.

For complete details on installing and using the `epub` CLI, you can get details here

[epub cli documentation](docs/wiki/epub-cli.md)
