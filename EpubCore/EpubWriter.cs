using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using EpubCore.Extensions;
using EpubCore.Format;
using EpubCore.Format.Writers;
using EpubCore.Misc;

namespace EpubCore
{
    public class EpubWriter : IEpubWriter
    {
        private readonly string _opfPath = "EPUB/package.opf";
        private readonly string _ncxPath = "EPUB/toc.ncx";

        private readonly EpubFormat _format;
        private readonly EpubResources _resources;

        public EpubWriter() 
        {
            var opf = new OpfDocument
            {
                UniqueIdentifier = Guid.NewGuid().ToString("D"),
                EpubVersion = EpubVersion.Epub3
            };

            opf.UniqueIdentifier = Constants.DefaultOpfUniqueIdentifier;
            opf.Metadata.Identifiers.Add(new OpfMetadataIdentifier { Id = Constants.DefaultOpfUniqueIdentifier, Scheme = "uuid", Text = Guid.NewGuid().ToString("D") });
            opf.Metadata.Dates.Add(new OpfMetadataDate { Text = DateTimeOffset.UtcNow.ToString("o") });
            opf.Manifest.Items.Add(new OpfManifestItem { Id = "ncx", Href = "toc.ncx", MediaType = ContentType.ContentTypeToMimeType[EpubContentType.DtbookNcx] });
            opf.Spine.Toc = "ncx";

            _format = new EpubFormat
            {
                Opf = opf,
                Nav = new NavDocument(),
                Ncx = new NcxDocument()
            };

            _format.Nav.Head.Dom = new XElement(NavElements.Head);
            _format.Nav.Body.Dom =
                new XElement(
                    NavElements.Body,
                        new XElement(NavElements.Nav, new XAttribute(NavNav.Attributes.Type, NavNav.Attributes.TypeValues.Toc),
                            new XElement(NavElements.Ol)));

            _resources = new EpubResources();
        }

        public EpubWriter(EpubBook book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (book.Format?.Opf == null) throw new ArgumentException("book opf instance == null", nameof(book));

            _format = book.Format;
            _resources = book.Resources;

            _opfPath = _format.Ocf.RootFilePath;
            _ncxPath = _format.Opf.FindNcxPath();

            if (_ncxPath != null)
            {
                // Remove NCX file from the resources - Write() will format a new one.
                _resources.Other = _resources.Other.Where(e => e.Href != _ncxPath).ToList();

                _ncxPath = _ncxPath.ToAbsolutePath(_opfPath);
            }
        }

        public void Write(EpubBook book, string epubBookPath)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (string.IsNullOrWhiteSpace(epubBookPath)) throw new ArgumentNullException(nameof(epubBookPath));

            var writer = new EpubWriter(book);
            writer.Write(epubBookPath);
        }

        public void Write(EpubBook book, Stream stream)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var writer = new EpubWriter(book);
            writer.Write(stream);
        }

        public void InsertFileBefore(string filename, byte[] content, EpubContentType type, string beforeFileName = null)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentNullException(nameof(filename));
            if (content == null) throw new ArgumentNullException(nameof(content));

            var file = new EpubByteFile
            {
                AbsolutePath = filename,
                Href = filename,
                ContentType = type,
                Content = content
            };
            file.MimeType = ContentType.ContentTypeToMimeType[file.ContentType];

            switch (type)
            {
                case EpubContentType.Css:
                    if (string.IsNullOrEmpty(beforeFileName))
                    {
                        _resources.Css.Add(file.ToTextFile());
                    }
                    else
                    {
                        var soughtResource = _resources.Css.FirstOrDefault(g => g.FileName.Equals(beforeFileName));
                        if (soughtResource != null)
                        {
                            var index = _resources.Css.IndexOf(soughtResource);
                            _resources.Css.Insert(index, file.ToTextFile());
                        }
                        else
                        {
                            _resources.Css.Add(file.ToTextFile());
                        }
                    }
                   
                    break;

                case EpubContentType.FontOpentype:
                case EpubContentType.XFontOpentype:
                case EpubContentType.FontTruetype:
                case EpubContentType.XFontTruetype:
                    if (string.IsNullOrEmpty(beforeFileName))
                    {
                        _resources.Fonts.Add(file);
                    }
                    else
                    {
                        var soughtResource = _resources.Fonts.FirstOrDefault(g => g.Href.Equals(beforeFileName));
                        if (soughtResource != null)
                        {
                            var index = _resources.Fonts.IndexOf(soughtResource);
                            _resources.Fonts.Insert(index, file);
                        }
                        else
                        {
                            _resources.Fonts.Add(file);
                        }
                    }

                    break;

                case EpubContentType.ImageGif:
                case EpubContentType.ImageJpeg:
                case EpubContentType.ImagePng:
                case EpubContentType.ImageSvg:
                    if (string.IsNullOrEmpty(beforeFileName))
                    {
                        _resources.Images.Add(file);
                    }
                    else
                    {
                        var soughtResource = _resources.Images.FirstOrDefault(g => g.Href.Equals(beforeFileName));
                        if (soughtResource != null)
                        {
                            var index = _resources.Images.IndexOf(soughtResource);
                            _resources.Images.Insert(index, file);
                        }
                        else
                        {
                            _resources.Images.Add(file);
                        }
                    }

                    break;

                case EpubContentType.Xml:
                case EpubContentType.Xhtml11:
                case EpubContentType.Other:
                    if (string.IsNullOrEmpty(beforeFileName))
                    {
                        _resources.Other.Add(file);
                    }
                    else
                    {
                        var soughtResource = _resources.Other.FirstOrDefault(g => g.Href.Equals(beforeFileName));
                        if (soughtResource != null)
                        {
                            var index = _resources.Other.IndexOf(soughtResource);
                            _resources.Other.Insert(index, file);
                        }
                        else
                        {
                            _resources.Other.Add(file);
                        }
                    }

                    break;

                default:
                    throw new InvalidOperationException($"Unsupported file type: {type}");
            }

            var manifestEntry = new OpfManifestItem
            {
                Id = Guid.NewGuid().ToString("N"),
                Href = filename,
                MediaType = file.MimeType
            };

            if (string.IsNullOrEmpty(beforeFileName))
            {
                _format.Opf.Manifest.Items.Add(manifestEntry);
            }
            else
            {
                var soughtResource = _format.Opf.Manifest.Items.FirstOrDefault(g =>g.Href.Equals(beforeFileName));
                if (soughtResource != null)
                {
                    var index = _format.Opf.Manifest.Items.IndexOf(soughtResource);
                    _format.Opf.Manifest.Items.Insert(index, manifestEntry); 
                }
                else
                {
                    _format.Opf.Manifest.Items.Add(manifestEntry);
                }
            }
        }

        public void InsertFileBefore(string filename, string content, EpubContentType type,
            string beforeFileName = null)
        {
            InsertFileBefore(filename, Constants.DefaultEncoding.GetBytes(content), type, beforeFileName);
        }

        public void AddFile(string filename, byte[] content, EpubContentType type)
        {
           InsertFileBefore(filename, content, type);
        }

        public void AddFile(string filename, string content, EpubContentType type)
        {
            AddFile(filename, Constants.DefaultEncoding.GetBytes(content), type);
        }

        public void AddAuthor(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName)) throw new ArgumentNullException(nameof(authorName));
            _format.Opf.Metadata.Creators.Add(new OpfMetadataCreator { Text = authorName });
        }

        public void AddPublisher(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName)) throw new ArgumentNullException(nameof(publisherName));
            _format.Opf.Metadata.Publishers.Add(publisherName);
        }

        public void ClearPublishers()
        {
            _format.Opf.Metadata.Publishers.Clear();
        }

        public void RemovePublisher(string publisherName)
        {
            if (string.IsNullOrWhiteSpace(publisherName)) throw new ArgumentNullException(nameof(publisherName));

            _format.Opf.Metadata.Publishers.Remove(publisherName);
        }



        public void ClearAuthors()
        {
            _format.Opf.Metadata.Creators.Clear();
        }

        public void RemoveAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author)) throw new ArgumentNullException(nameof(author));
            foreach (var entity in _format.Opf.Metadata.Creators.Where(e => e.Text == author).ToList())
            {
                _format.Opf.Metadata.Creators.Remove(entity);
            }
        }
        
        public void RemoveTitle()
        {
           _format.Opf.Metadata.Titles.Clear();
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
            //Remove the old title 
            RemoveTitle();
            _format.Opf.Metadata.Titles.Add(title);
        }

        public void AddLanguage(string language)
        {
            _format.Opf.Metadata.Languages.Add(language);
        }

        public void SetVersion(EpubVersion version)
        {
            _format.Opf.EpubVersion = version;
        }

        public void SetTitles(IEnumerable<string> titles)
        {
            var enumerable = titles as string[] ?? titles.ToArray();
            if (!enumerable.Any()) throw new ArgumentNullException(nameof(titles));
            //Remove the old title 
            RemoveTitle();
            foreach (var curTitle in enumerable.ToList())
            {
                _format.Opf.Metadata.Titles.Add(curTitle);
            }
            
        }

        public void SetUniqueIdentifier(string uniqueIdentifier)
        {
            if (string.IsNullOrWhiteSpace(uniqueIdentifier)) throw new ArgumentNullException(nameof(uniqueIdentifier));
            _format.Opf.UniqueIdentifier = uniqueIdentifier;
        }

        public EpubChapter InsertChapterBefore(string title, string html, string fileId = null, string beforeFileName = null)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException(nameof(title));
            if (html == null) throw new ArgumentNullException(nameof(html));

            fileId = fileId ?? Guid.NewGuid().ToString("N");
            EpubTextFile beforeEpubTextFile = null;
            OpfManifestItem selectedManifestItem = null;

            var file = new EpubTextFile
            {
                AbsolutePath = fileId + ".html",
                Href = fileId + ".html",
                TextContent = html,
                ContentType = EpubContentType.Xhtml11
            };
            file.MimeType = ContentType.ContentTypeToMimeType[file.ContentType];

            if (string.IsNullOrEmpty(beforeFileName))
            {
                _resources.Html.Add(file);
            }
            else
            {
                beforeEpubTextFile = _resources.Html.FirstOrDefault(g => g.FileName.Equals(beforeFileName));
                if (beforeEpubTextFile == null)
                {
                    _resources.Html.Add(file);
                }
                else
                {
                    var index = _resources.Html.IndexOf(beforeEpubTextFile);
                    _resources.Html.Insert(index, file);
                }
            }
            
            var manifestItem = new OpfManifestItem
            {
                Id = fileId,
                Href = file.Href,
                MediaType = file.MimeType
            };

            if (string.IsNullOrWhiteSpace(beforeFileName))
            {
                _format.Opf.Manifest.Items.Add(manifestItem);
            }
            else
            {
                selectedManifestItem = _format.Opf.Manifest.Items.FirstOrDefault(g => g.Href == beforeFileName);
                if (selectedManifestItem == null)
                {
                    _format.Opf.Manifest.Items.Add(manifestItem);
                }
                else
                {
                    var index = _format.Opf.Manifest.Items.IndexOf(selectedManifestItem);

                    _format.Opf.Manifest.Items.Insert(index, manifestItem);
                }
            }
            

            var spineItem = new OpfSpineItemRef { IdRef = manifestItem.Id, Linear = true };
            if (selectedManifestItem == null)
            {
                _format.Opf.Spine.ItemRefs.Add(spineItem);
            }
            else
            {
                var beforeSpinItem =
                    _format.Opf.Spine.ItemRefs.FirstOrDefault(g => g.IdRef.Equals(selectedManifestItem.Id));
                if (beforeSpinItem == null)
                {
                    _format.Opf.Spine.ItemRefs.Add(spineItem);
                }
                else
                {
                    var index = _format.Opf.Spine.ItemRefs.IndexOf(beforeSpinItem);
                    _format.Opf.Spine.ItemRefs.Insert(index, spineItem);
                }
            }

            var tocElement = new XElement(NavElements.Li,
                new XElement(NavElements.A, new XAttribute("href", file.Href), title));

            if (beforeEpubTextFile == null)
            {
                FindNavTocOl()?.Add(tocElement);
            }
            else
            {
                var beforeTocItem = FindNavTocOl()?.Descendants().FirstOrDefault(g =>
                    g.Attribute("href") != null && g.Attribute("href")!.Value == beforeEpubTextFile.Href);
                if (beforeTocItem == null)
                {
                    FindNavTocOl()?.Add(tocElement);
                }
                else
                {
                    beforeTocItem.AddBeforeSelf(tocElement);
                }
            }

            var nxcNavPoint = new NcxNavPoint
            {
                Id = Guid.NewGuid().ToString("N"),
                NavLabelText = title,
                ContentSrc = file.Href
            };

            var defaultPlayOrder = _format.Ncx.NavMap.NavPoints.Any()
                ? _format.Ncx.NavMap.NavPoints.Max(e => e.PlayOrder)
                : 1;

            if (beforeEpubTextFile == null)
            {
                nxcNavPoint.PlayOrder = defaultPlayOrder;
                _format.Ncx?.NavMap.NavPoints.Add(nxcNavPoint);
            }
            else
            {
                var beforeNcxNavPoint =
                    _format.Ncx?.NavMap.NavPoints.FirstOrDefault(g => g.ContentSrc == beforeEpubTextFile.Href);
                if (beforeNcxNavPoint == null)
                {
                    nxcNavPoint.PlayOrder = defaultPlayOrder;
                    _format.Ncx?.NavMap.NavPoints.Add(nxcNavPoint);
                }
                else
                {
                    nxcNavPoint.PlayOrder = (beforeNcxNavPoint.PlayOrder <= 0) ? 0 : beforeNcxNavPoint.PlayOrder - 1;

                    var index = _format.Ncx?.NavMap.NavPoints.IndexOf(beforeNcxNavPoint);

                    _format.Ncx?.NavMap.NavPoints.Insert(index??0, nxcNavPoint);
                }

                _format.Ncx?.NavMap.ReorderNavPointsPlayOrder();
            }

            return new EpubChapter
            {
                Id = fileId,
                Title = title,
                RelativePath = file.AbsolutePath
            };
        }




        public EpubChapter AddChapter(string title, string html, string fileId = null)
        {
            return InsertChapterBefore(title, html, fileId);
        }

        public void ClearChapters()
        {
            var spineItems = _format.Opf.Spine.ItemRefs.Select(spine => _format.Opf.Manifest.Items.Single(e => e.Id == spine.IdRef));
            var otherItems = _format.Opf.Manifest.Items.Where(e => !spineItems.Contains(e)).ToList();

            foreach (var item in spineItems)
            {
                var href = new Href(item.Href);
                if (otherItems.All(e => new Href(e.Href).Path != href.Path))
                {
                    // The HTML file is not referenced by anything outside spine item, thus can be removed from the archive.
                    var file = _resources.Html.Single(e => e.Href == href.Path);
                    _resources.Html.Remove(file);
                }

                _format.Opf.Manifest.Items.Remove(item);
            }

            _format.Opf.Spine.ItemRefs.Clear();
            _format.Opf.Guide = null;
            _format.Ncx?.NavMap.NavPoints.Clear();
            FindNavTocOl()?.Descendants().Remove();

            // Remove all images except the cover.
            // I can't think of a case where this is a bad idea.
            var coverPath = _format.Opf.FindCoverPath();
            foreach (var item in _format.Opf.Manifest.Items.Where(e => e.MediaType.StartsWith("image/") && e.Href != coverPath).ToList())
            {
                _format.Opf.Manifest.Items.Remove(item);

                var image = _resources.Images.Single(e => e.Href == new Href(item.Href).Path);
                _resources.Images.Remove(image);
            }
        }

        //public void InsertChapter(string title, string html, int index, EpubChapter parent = null)
        //{
        //    throw new NotImplementedException("Implement me!");
        //}

        public string RemoveCover()
        {
            var path = _format.Opf.FindAndRemoveCover();
            if (path == null) return null;

            var resource = _resources.Images.SingleOrDefault(e => e.Href == path);
            if (resource != null)
            {
                _resources.Images.Remove(resource);
            }

            return path;
        }

        public void SetCover(byte[] data, ImageFormat imageFormat)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var pathToOldCover = RemoveCover();

            string filename;
            EpubContentType type;

            switch (imageFormat)
            {
                case ImageFormat.Gif:
                    filename = "cover.gif";
                    type = EpubContentType.ImageGif;
                    break;
                case ImageFormat.Jpeg:
                    filename = "cover.jpeg";
                    type = EpubContentType.ImageJpeg;
                    break;
                case ImageFormat.Png:
                    filename = "cover.png";
                    type = EpubContentType.ImagePng;
                    break;
                case ImageFormat.Svg:
                    filename = "cover.svg";
                    type = EpubContentType.ImageSvg;
                    break;
                default:
                    throw new ArgumentException($"Unsupported cover format: {_format}", nameof(_format));
            }

            var coverResource = new EpubByteFile
            {
                AbsolutePath = filename,
                Href = filename,
                ContentType = type,
                Content = data
            };
            coverResource.MimeType = ContentType.ContentTypeToMimeType[coverResource.ContentType];
            _resources.Images.Add(coverResource);

            var coverItem = new OpfManifestItem
            {
                Id = OpfManifest.ManifestItemCoverImageProperty,
                Href = coverResource.Href,
                MediaType = coverResource.MimeType
            };
            coverItem.Properties.Add(OpfManifest.ManifestItemCoverImageProperty);
            _format.Opf.Manifest.Items.Add(coverItem);
            _format.Opf.Metadata.Metas.Add(new OpfMetadataMeta() { 
                Name= "cover",                
                Content = "cover-image"
            });

            if (string.IsNullOrEmpty(pathToOldCover) || string.IsNullOrEmpty(coverResource.Href)) return;
            var htmlFiles = _resources.Html;
            foreach (var htmlFile in htmlFiles)
            {
                htmlFile.ReplaceValue(pathToOldCover, coverResource.Href);
            }
        }

        public byte[] Write()
        {
            var stream = new MemoryStream();
            Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream.ReadToEnd();
        }
        
        public void Write(string epubBookPath)
        {
            using var fileStream = File.Create(epubBookPath);
            Write(fileStream);
            fileStream.Flush();
            fileStream.Dispose();
        }

        public void Write(Stream stream)
        {
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create, true);
            archive.CreateEntry("mimetype", MimeTypeWriter.Format());
            archive.CreateEntry(Constants.OcfPath, OcfWriter.Format(_opfPath));
           
            archive.CreateEntry(_opfPath, OpfWriter.Format(_format.Opf));

            if (_format.Ncx != null)
            {
                archive.CreateEntry(_ncxPath, NcxWriter.Format(_format.Ncx));
            }

            var allFiles = new[]
            {
                _resources.Html.Cast<EpubFile>(),
                _resources.Css,
                _resources.Images,
                _resources.Fonts,
                _resources.Other
            }.SelectMany(collection => collection as EpubFile[] ?? collection.ToArray());
            var relativePath = PathExt.GetDirectoryPath(_opfPath);
            foreach (var file in allFiles)
            {
                var absolutePath = PathExt.Combine(relativePath, file.Href);
                archive.CreateEntry(absolutePath, file.Content);
            }

            archive.Dispose();
        }

        public XElement FindNavTocOl()
        {
            if (_format.Nav == null)
            {
                return null;
            }

            var ns = _format.Nav.Body.Dom.Name.Namespace;
            var element = _format.Nav.Body.Dom.Descendants(ns + NavElements.Nav)
                .SingleOrDefault(e => (string)e.Attribute(NavNav.Attributes.Type) == NavNav.Attributes.TypeValues.Toc)
                ?.Element(ns + NavElements.Ol);

            if (element == null)
            {
                throw new EpubWriteException(@"Missing ol: <nav type=""toc""><ol/></nav>");
            }

            return element;
        }

        public ImageFormat GetContentTypeForImageName(string imageName)
        {
            var extension = Path.GetExtension(imageName);
            switch (extension)
            {
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".svg":
                    return ImageFormat.Svg;
                case ".gif":
                    return ImageFormat.Gif;
                default:
                    throw new ArgumentException($"Unsupported cover format: {extension}");
            }
        }


        /// <summary>
        /// Clones the book instance by writing and reading it from memory.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public static EpubBook MakeCopy(EpubBook book)
        {
            var stream = new MemoryStream();
            var writer = new EpubWriter(book);
            writer.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            var epub = EpubReader.Read(stream, false);
            return epub;
        }
    }
}
