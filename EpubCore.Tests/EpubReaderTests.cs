using System.Linq;
using EpubCore.Format;
using FluentAssertions;
using Xunit;

namespace EpubCore.Tests
{
    public class EpubReaderTests
    {
        [Fact]
        public void ReadPg68377FormatTest()
        {
            var book = EpubReader.Read(Cwd.Combine(@"Samples/pg68377.epub"));
            var format = book.Format;

            Assert.NotNull(format);

            Assert.NotNull(format.Ocf);
            Assert.Equal(1, format.Ocf.RootFiles.Count);
            Assert.Equal("OEBPS/content.opf", format.Ocf.RootFiles.ElementAt(0).FullPath);
            Assert.Equal("application/oebps-package+xml", format.Ocf.RootFiles.ElementAt(0).MediaType);
            Assert.Equal("OEBPS/content.opf", format.Ocf.RootFilePath);

            Assert.NotNull(format.Opf);
            Assert.Equal("id", format.Opf.UniqueIdentifier);
            Assert.Equal(EpubVersion.Epub2, format.Opf.EpubVersion);

          
            Assert.NotNull(format.Opf.Guide);
            Assert.Equal(1, format.Opf.Guide.References.Count);

            Assert.Equal("wrap0000.html", format.Opf.Guide.References.ElementAt(0).Href);
            Assert.Equal("Cover", format.Opf.Guide.References.ElementAt(0).Title);
            Assert.Equal("cover", format.Opf.Guide.References.ElementAt(0).Type);

            Assert.NotNull(format.Opf.Manifest);
            Assert.Equal(7, format.Opf.Manifest.Items.Count);

            // <item id="body097" href="xhtml/chapter_083.xhtml" media-type="application/xhtml+xml" properties="svg"/>
            var item = format.Opf.Manifest.Items.First(e => e.Id == "item5");
            Assert.Equal("5982094653992132714_68377-h-0.htm.html", item.Href);
            Assert.Equal("application/xhtml+xml", item.MediaType);
            Assert.Equal(0, item.Properties.Count);
            Assert.Null(item.Fallback);
            Assert.Null(item.FallbackStyle);
            Assert.Null(item.RequiredModules);
            Assert.Null(item.RequiredNamespace);

            // <item id="css2" href="styles/big.css" media-type="text/css"/>
            item = format.Opf.Manifest.Items.First(e => e.Id == "item3");
            Assert.Equal("0.css", item.Href);
            Assert.Equal("text/css", item.MediaType);
            Assert.Equal(0, item.Properties.Count);
            Assert.Null(item.Fallback);
            Assert.Null(item.FallbackStyle);
            Assert.Null(item.RequiredModules);
            Assert.Null(item.RequiredNamespace);

           
            Assert.NotNull(format.Opf.Metadata);
            Assert.Equal(0, format.Opf.Metadata.Contributors.Count);
            Assert.Equal(0, format.Opf.Metadata.Coverages.Count);
            Assert.Equal(1, format.Opf.Metadata.Creators.Count);
            Assert.Equal("Isaac Asimov", format.Opf.Metadata.Creators.ElementAt(0).Text);
            Assert.Equal(2, format.Opf.Metadata.Dates.Count);
            Assert.Equal("2022-06-22", format.Opf.Metadata.Dates.ElementAt(0).Text);
            Assert.Equal(0, format.Opf.Metadata.Descriptions.Count);
            Assert.Equal(0, format.Opf.Metadata.Formats.Count);

            Assert.Equal(1, format.Opf.Metadata.Identifiers.Count);
            Assert.Equal("http://www.gutenberg.org/68377", format.Opf.Metadata.Identifiers.ElementAt(0).Text);
            Assert.Equal("id", format.Opf.Metadata.Identifiers.ElementAt(0).Id);

            Assert.Equal(1, format.Opf.Metadata.Languages.Count);
            Assert.Equal("en", format.Opf.Metadata.Languages.ElementAt(0));

            Assert.Equal(1, format.Opf.Metadata.Metas.Count);
            Assert.True(format.Opf.Metadata.Metas.All(e => e.Id == null));
            Assert.True(format.Opf.Metadata.Metas.All(e => e.Scheme == null));

            var meta = format.Opf.Metadata.Metas.Single(e => e.Name == "cover");
            Assert.Equal("item1", meta.Text);
            Assert.Null(meta.Id);
            Assert.Null(meta.Property);
            Assert.Null(meta.Refines);
            Assert.Null(meta.Scheme);

            Assert.Equal(0, format.Opf.Metadata.Publishers.Count);

            Assert.Equal(0, format.Opf.Metadata.Relations.Count);

            Assert.Equal(1, format.Opf.Metadata.Rights.Count);
            Assert.Equal("Public domain in the USA.", format.Opf.Metadata.Rights.ElementAt(0));

            Assert.Equal(1, format.Opf.Metadata.Sources.Count);
            Assert.Equal("https://www.gutenberg.org/files/68377/68377-h/68377-h.htm", format.Opf.Metadata.Sources.ElementAt(0));

            Assert.Equal(0, format.Opf.Metadata.Subjects.Count);
            Assert.Equal(0, format.Opf.Metadata.Types.Count);

            Assert.Equal(1, format.Opf.Metadata.Titles.Count);
            Assert.Equal("Let's Get Together", format.Opf.Metadata.Titles.ElementAt(0));

            Assert.Equal(1, format.Opf.Metadata.Identifiers.Count);
            Assert.Null(format.Opf.Metadata.Identifiers.ElementAt(0).Scheme);
            Assert.Equal("id", format.Opf.Metadata.Identifiers.ElementAt(0).Id);
            Assert.Equal("http://www.gutenberg.org/68377", format.Opf.Metadata.Identifiers.ElementAt(0).Text);

            Assert.Equal("ncx", format.Opf.Spine.Toc);
            Assert.Equal(2, format.Opf.Spine.ItemRefs.Count);
            Assert.Equal(0, format.Opf.Spine.ItemRefs.Count(e => e.Properties.Contains("page-spread-right")));
            Assert.Equal(1, format.Opf.Spine.ItemRefs.Count(e => e.IdRef == "coverpage-wrapper"));

            Assert.Null(format.Ncx.DocAuthor);
            Assert.Equal("Let's Get Together", format.Ncx.DocTitle);

          
            Assert.Equal(5, format.Ncx.Meta.Count);

            Assert.Equal("dtb:uid", format.Ncx.Meta.ElementAt(0).Name);
            Assert.Equal("http://www.gutenberg.org/68377", format.Ncx.Meta.ElementAt(0).Content);
            Assert.Null(format.Ncx.Meta.ElementAt(0).Scheme);

            Assert.Equal("dtb:depth", format.Ncx.Meta.ElementAt(1).Name);
            Assert.Equal("1", format.Ncx.Meta.ElementAt(1).Content);
            Assert.Null(format.Ncx.Meta.ElementAt(1).Scheme);

            Assert.Equal("dtb:generator", format.Ncx.Meta.ElementAt(2).Name);
            Assert.Equal("Ebookmaker 0.11.30 by Project Gutenberg", format.Ncx.Meta.ElementAt(2).Content);
            Assert.Null(format.Ncx.Meta.ElementAt(2).Scheme);

            Assert.Null(format.Ncx.NavList);
            Assert.Null(format.Ncx.PageList);

            Assert.NotNull(format.Ncx.NavMap);
            Assert.NotNull(format.Ncx.NavMap.Dom);
            Assert.Equal(1, format.Ncx.NavMap.NavPoints.Count);
            foreach (var point in format.Ncx.NavMap.NavPoints)
            {
                Assert.NotNull(point.Id);
                Assert.NotNull(point.PlayOrder);
                Assert.NotNull(point.ContentSrc);
                Assert.NotNull(point.NavLabelText);
                Assert.Null(point.Class);
                Assert.False(point.NavPoints.Any());
            }

            var navPoint = format.Ncx.NavMap.NavPoints.Single(e => e.Id == "np-1");
            Assert.Equal(1, navPoint.PlayOrder);
            Assert.Equal("LET'S GET TOGETHER", navPoint.NavLabelText);
            Assert.Equal("5982094653992132714_68377-h-0.htm.html#pgepubid00000", navPoint.ContentSrc);
        }

        [Fact]
        public void ReadIOSHackersHandbookTest()
        {
            var book = EpubReader.Read(Cwd.Combine(@"Samples/ios-hackers-handbook.epub"));
            book.TableOfContents.Should().HaveCount(14);
            book.TableOfContents.SelectMany(e => e.SubChapters).Concat(book.TableOfContents).Should().HaveCount(78);
            book.TableOfContents[0].AbsolutePath.Should().Be("OEBPS/9781118240755cover.xhtml");
            book.TableOfContents[1].AbsolutePath.Should().Be("OEBPS/9781118240755c01.xhtml");
            book.TableOfContents[1].SubChapters.Should().HaveCount(6);
            book.TableOfContents[1].SubChapters[0].AbsolutePath.Should().Be("OEBPS/9781118240755c01.xhtml");
        }

        [Fact]
        public void SetsChapterParents()
        {
            var book = EpubReader.Read(Cwd.Combine(@"Samples/ios-hackers-handbook.epub"));

            foreach (var chapter in book.TableOfContents)
            {
                chapter.Parent.Should().BeNull();
                chapter.SubChapters.All(e => e.Parent == chapter).Should().BeTrue();
            }
        }

        [Fact]
        public void SetsChapterPreviousNext()
        {
            var book = EpubReader.Read(Cwd.Combine(@"Samples/ios-hackers-handbook.epub"));

            EpubChapter previousChapter = null;
            var currentChapter = book.TableOfContents[0];
            currentChapter.Previous.Should().Be(previousChapter);

            for (var i = 1; i <= 77; ++i)
            {
                previousChapter = currentChapter;
                currentChapter = currentChapter.Next;

                previousChapter.Next.Should().Be(currentChapter);
                currentChapter.Previous.Should().Be(previousChapter);
            }

            EpubChapter nextChapter = null;
            currentChapter.Next.Should().Be(nextChapter);
            
            for (var i = 1; i <= 77; ++i)
            {
                nextChapter = currentChapter;
                currentChapter = currentChapter.Previous;

                currentChapter.Next.Should().Be(nextChapter);
                nextChapter.Previous.Should().Be(currentChapter);
            }

            currentChapter.Previous.Should().BeNull();
        }
    }
}
