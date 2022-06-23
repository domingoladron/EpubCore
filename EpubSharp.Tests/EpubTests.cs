﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Penman.EpubSharp.Format;
using Xunit;

namespace Penman.EpubSharp.Tests
{
    public class EpubTests
    {
        [Fact]
        public void ReadWriteEpub30Test()
        {
            var archives = Utils.ZipAndCopyEpubs(Cwd.Combine(@"Samples/epub30"));
            ReadWriteTest(archives);
        }

        [Fact]
        public void ReadWriteEpub31Test()
        {
            var archives = Utils.ZipAndCopyEpubs(Cwd.Combine(@"Samples/epub31"));
            ReadWriteTest(archives);
        }

        [Fact]
        public void ReadWriteEpubAssortedTest()
        {
            var archives = Utils.ZipAndCopyEpubs(Cwd.Combine(@"Samples/epub-assorted"));
            ReadWriteTest(archives);
        }

        private void ReadWriteTest(List<string> archives)
        {
            foreach (var archive in archives)
            {
                var originalEpub = EpubReader.Read(archive);

                var stream = new MemoryStream();
                EpubWriter.Write(originalEpub, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var savedEpub = EpubReader.Read(stream, false);

                AssertEpub(originalEpub, savedEpub);
            }
        }

        private void AssertEpub(EpubBook expected, EpubBook actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.Equal(expected.Title, actual.Title);

            AssertPrimitiveCollection(expected.Authors, actual.Authors, nameof(actual.Authors));

            Assert.Equal(expected.CoverImage == null, actual.CoverImage == null);
            if (expected.CoverImage != null && actual.CoverImage != null)
            {
                Assert.True(expected.CoverImage.Length > 0, "Expected CoverImage.Length > 0");
                Assert.True(actual.CoverImage.Length > 0, "Actual CoverImage.Length > 0");
                Assert.Equal(expected.CoverImage.Length, actual.CoverImage.Length);
            }

            AssertContentFileCollection(expected.Resources.Css, actual.Resources.Css);
            AssertContentFileCollection(expected.Resources.Fonts, actual.Resources.Fonts);
            AssertContentFileCollection(expected.Resources.Html, actual.Resources.Html);
            AssertContentFileCollection(expected.Resources.Images, actual.Resources.Images);
            AssertContentFileCollection(
                // Filter some format related files, because they often are not byte-by-byte the same when are generated by the writers.
                expected.Resources.Other.Where(e => e.Href != expected.Format.Opf.FindNcxPath()),
                actual.Resources.Other.Where(e => e.Href != expected.Format.Opf.FindNcxPath())
            );
            AssertCollection(expected.SpecialResources.HtmlInReadingOrder, actual.SpecialResources.HtmlInReadingOrder, (old, @new) =>
            {
                AssertContentFile(old, @new);
            });

            AssertCollection(expected.TableOfContents, actual.TableOfContents, AssertChapter);

            AssertOcf(expected.Format.Ocf, actual.Format.Ocf);
            AssertOpf(expected.Format.Opf, actual.Format.Opf);
            AssertNcx(expected.Format.Ncx, actual.Format.Ncx);
            AssertNav(expected.Format.Nav, actual.Format.Nav);
        }

        private void AssertCollectionWithIndex<T>(IEnumerable<T> expected, IEnumerable<T> actual, Action<List<T>, List<T>, int> assert)
        {
            Assert.Equal(expected == null, actual == null);
            if (expected != null && actual != null)
            {
                var old = expected.ToList();
                var @new = actual.ToList();

                Assert.Equal(old.Count, @new.Count);

                for (var i = 0; i < @new.Count; ++i)
                {
                    assert(old, @new, i);
                }
            }
        }

        private void AssertCollection<T>(IEnumerable<T> expected, IEnumerable<T> actual, Action<T, T> assert)
        {
            AssertCollectionWithIndex(expected, actual, (a, b, i) =>
            {
                assert(a[i], b[i]);
            });
        }

        private void AssertPrimitiveCollection<T>(IEnumerable<T> expected, IEnumerable<T> actual, string unitName)
        {
            AssertCollectionWithIndex(expected, actual, (a, b, i) =>
            {
                Assert.True(a.Contains(b[i]), unitName);
            });
        }

        private void AssertContentFileCollection<TContent>(Dictionary<string, TContent> expected, Dictionary<string, TContent> actual)
            where TContent : EpubFile
        {
            AssertCollection(expected, actual, (a, b) =>
            {
                Assert.Equal(a.Key, b.Key);
                AssertContentFile(a.Value, b.Value);
            });
        }

        private void AssertContentFileCollection<TContent>(IEnumerable<TContent> expected, IEnumerable<TContent> actual)
            where TContent : EpubFile
        {
            AssertCollection(expected, actual, (a, b) =>
            {
                AssertContentFile(a, b);
            });
        }

        private void AssertContentFile(EpubFile expected, EpubFile actual)
        {
            Assert.Equal(expected.AbsolutePath, actual.AbsolutePath);
            Assert.Equal(expected.Href, actual.Href);
            Assert.True(expected.Content.SequenceEqual(actual.Content));
            Assert.Equal(expected.ContentType, actual.ContentType);
            Assert.Equal(expected.MimeType, actual.MimeType);

            var castedOld = expected as EpubTextFile;
            var castedNew = actual as EpubTextFile;
            Assert.Equal(castedOld == null, castedNew == null);
            if (castedOld != null && castedNew != null)
            {
                Assert.Equal(castedOld.TextContent, castedNew.TextContent);
            }
        }

        private void AssertChapter(EpubChapter expected, EpubChapter actual)
        {
            Assert.Equal(expected.RelativePath, actual.RelativePath);
            Assert.Equal(expected.HashLocation, actual.HashLocation);
            Assert.Equal(expected.Title, actual.Title);

            Assert.Equal(expected.SubChapters.Count, actual.SubChapters.Count);
            for (var i = 0; i < expected.SubChapters.Count; ++i)
            {
                AssertChapter(expected.SubChapters[i], actual.SubChapters[i]);
            }
        }

        private void AssertOcf(OcfDocument expected, OcfDocument actual)
        {
            // There are some epubs with multiple root files.
            // i.e. 1 normal and 1 for braille.
            // We don't have multiple root file support, therefore Take(1) for now.
            // Currently it is also assumed that the first root file is the main root file.
            // This is a dangerous assumption.
            AssertCollection(expected.RootFiles.Take(1), actual.RootFiles, (a, b) =>
            {
                Assert.Equal(a.FullPath, b.FullPath);
                Assert.Equal(a.MediaType, b.MediaType);
            });
            Assert.Equal(expected.RootFilePath, actual.RootFilePath);
        }

        private void AssertOpf(OpfDocument expected, OpfDocument actual)
        {
            Assert.Equal(expected == null, actual == null);
            if (expected != null && actual != null)
            {
                Assert.Equal(expected.UniqueIdentifier, actual.UniqueIdentifier);
                Assert.Equal(expected.EpubVersion, actual.EpubVersion);

                Assert.Equal(expected.Metadata == null, actual.Metadata == null);
                if (expected.Metadata != null && actual.Metadata != null)
                {
                    AssertCreators(expected.Metadata.Creators, actual.Metadata.Creators);
                    AssertCreators(expected.Metadata.Contributors, actual.Metadata.Contributors);

                    AssertCollection(expected.Metadata.Dates, actual.Metadata.Dates, (a, b) =>
                    {
                        Assert.Equal(a.Text, b.Text);
                        Assert.Equal(a.Event, b.Event);
                    });

                    AssertCollection(expected.Metadata.Identifiers, actual.Metadata.Identifiers, (a, b) =>
                    {
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.Scheme, b.Scheme);
                        Assert.Equal(a.Text, b.Text);
                    });

                    AssertCollection(expected.Metadata.Metas, actual.Metadata.Metas, (a, b) =>
                    {
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.Name, b.Name);
                        Assert.Equal(a.Property, b.Property);
                        Assert.Equal(a.Refines, b.Refines);
                        Assert.Equal(a.Scheme, b.Scheme);
                        Assert.Equal(a.Text, b.Text);
                    });

                    AssertCollection(expected.Metadata.Identifiers, actual.Metadata.Identifiers, (a, b) =>
                    {
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.Scheme, b.Scheme);
                        Assert.Equal(a.Text, b.Text);
                    });

                    AssertPrimitiveCollection<string>(expected.Metadata.Coverages, actual.Metadata.Coverages, "Coverage");
                    AssertPrimitiveCollection<string>(expected.Metadata.Descriptions, actual.Metadata.Descriptions, "Description");
                    AssertPrimitiveCollection<string>(expected.Metadata.Languages, actual.Metadata.Languages, "Language");
                    AssertPrimitiveCollection<string>(expected.Metadata.Publishers, actual.Metadata.Publishers, "Publisher");
                    AssertPrimitiveCollection<string>(expected.Metadata.Relations, actual.Metadata.Relations, "Relation");
                    AssertPrimitiveCollection<string>(expected.Metadata.Rights, actual.Metadata.Rights, "Right");
                    AssertPrimitiveCollection<string>(expected.Metadata.Sources, actual.Metadata.Sources, "Source");
                    AssertPrimitiveCollection<string>(expected.Metadata.Subjects, actual.Metadata.Subjects, "Subject");
                    AssertPrimitiveCollection<string>(expected.Metadata.Titles, actual.Metadata.Titles, "Title");
                    AssertPrimitiveCollection<string>(expected.Metadata.Types, actual.Metadata.Types, "Type");
                }

                Assert.Equal(expected.Guide == null, actual.Guide == null);
                if (expected.Guide != null && actual.Guide != null)
                {
                    AssertCollection(expected.Guide.References, actual.Guide.References, (a, b) =>
                    {
                        Assert.Equal(a.Title, b.Title);
                        Assert.Equal(a.Type, b.Type);
                        Assert.Equal(a.Href, b.Href);
                    });
                }

                Assert.Equal(expected.Manifest == null, actual.Manifest == null);
                if (expected.Manifest != null && actual.Manifest != null)
                {
                    AssertCollection(expected.Manifest.Items, actual.Manifest.Items, (a, b) =>
                    {
                        Assert.Equal(a.Fallback, b.Fallback);
                        Assert.Equal(a.FallbackStyle, b.FallbackStyle);
                        Assert.Equal(a.Href, b.Href);
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.MediaType, b.MediaType);
                        Assert.Equal(a.RequiredModules, b.RequiredModules);
                        Assert.Equal(a.RequiredNamespace, b.RequiredNamespace);
                        AssertPrimitiveCollection<string>(a.Properties, b.Properties, "Item.Property");
                    });
                }

                Assert.Equal(expected.Spine == null, actual.Spine == null);
                if (expected.Spine != null && actual.Spine != null)
                {
                    Assert.Equal(expected.Spine.Toc, actual.Spine.Toc);
                    AssertCollection(expected.Spine.ItemRefs, actual.Spine.ItemRefs, (a, b) =>
                    {
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.IdRef, b.IdRef);
                        Assert.Equal(a.Linear, b.Linear);
                        AssertPrimitiveCollection<string>(a.Properties, b.Properties, "ItemRef.Property");
                    });
                }

                Assert.Equal(expected.FindCoverPath(), actual.FindCoverPath());
                Assert.Equal(expected.FindNavPath(), actual.FindNavPath());
                Assert.Equal(expected.FindNcxPath(), actual.FindNcxPath());
            }
        }

        private void AssertCreators(IEnumerable<OpfMetadataCreator> expected, IEnumerable<OpfMetadataCreator> actual)
        {
            AssertCollection(expected, actual, (a, b) =>
            {
                Assert.Equal(a.AlternateScript, b.AlternateScript);
                Assert.Equal(a.FileAs, b.FileAs);
                Assert.Equal(a.Role, b.Role);
                Assert.Equal(a.Text, b.Text);
            });
        }

        private void AssertNcx(NcxDocument expected, NcxDocument actual)
        {
            Assert.Equal(expected == null, actual == null);
            if (expected != null && actual != null)
            {
                Assert.Equal(expected.DocAuthor, actual.DocAuthor);
                Assert.Equal(expected.DocTitle, actual.DocTitle);

                AssertCollection(expected.Meta, actual.Meta, (a, b) =>
                {
                    Assert.Equal(a.Name, b.Name);
                    Assert.Equal(a.Content, b.Content);
                    Assert.Equal(a.Scheme, b.Scheme);
                });

                Assert.Equal(expected.NavList == null, actual.NavList == null);
                if (expected.NavList != null && actual.NavList != null)
                {
                    Assert.Equal(expected.NavList.Id, actual.NavList.Id);
                    Assert.Equal(expected.NavList.Class, actual.NavList.Class);
                    Assert.Equal(expected.NavList.Label, actual.NavList.Label);

                    AssertCollection(expected.NavList.NavTargets, actual.NavList.NavTargets, (a, b) =>
                    {
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.Class, b.Class);
                        Assert.Equal(a.Label, b.Label);
                        Assert.Equal(a.PlayOrder, b.PlayOrder);
                        Assert.Equal(a.ContentSource, b.ContentSource);
                    });
                }

                AssertCollection(expected.NavMap.NavPoints, actual.NavMap.NavPoints, (a, b) =>
                {
                    Assert.Equal(a.Id, b.Id);
                    Assert.Equal(a.PlayOrder, b.PlayOrder);
                    Assert.Equal(a.NavLabelText, b.NavLabelText);
                    Assert.Equal(a.Class, b.Class);
                    Assert.Equal(a.ContentSrc, b.ContentSrc);
                    AssertNavigationPoints(a.NavPoints, b.NavPoints);
                });

                Assert.Equal(expected.PageList == null, actual.PageList == null);
                if (expected.PageList != null && actual.PageList != null)
                {
                    AssertCollection(expected.PageList.PageTargets, actual.PageList.PageTargets, (a, b) =>
                    {
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.Class, b.Class);
                        Assert.Equal(a.ContentSrc, b.ContentSrc);
                        Assert.Equal(a.NavLabelText, b.NavLabelText);
                        Assert.Equal(a.Type, b.Type);
                        Assert.Equal(a.Value, b.Value);
                    });
                }
            }
        }

        private void AssertNavigationPoints(IEnumerable<NcxNavPoint> expected, IEnumerable<NcxNavPoint> actual)
        {
            AssertCollection(expected, actual, (a, b) =>
            {
                Assert.Equal(a.Id, b.Id);
                Assert.Equal(a.Class, b.Class);
                Assert.Equal(a.ContentSrc, b.ContentSrc);
                Assert.Equal(a.NavLabelText, b.NavLabelText);
                Assert.Equal(a.PlayOrder, b.PlayOrder);
                Assert.Equal(a.NavPoints == null, b.NavPoints == null);
                if (a.NavPoints != null && b.NavPoints != null)
                {
                    AssertNavigationPoints(a.NavPoints, b.NavPoints);
                }
            });
        }

        private void AssertNav(NavDocument expected, NavDocument actual)
        {
            Assert.Equal(expected == null, actual == null);
            if (expected != null && actual != null)
            {
                Assert.Equal(expected.Head == null, actual.Head == null);
                if (expected.Head != null && actual.Head != null)
                {
                    Assert.Equal(expected.Head.Title, actual.Head.Title);
                    AssertCollection(expected.Head.Links, actual.Head.Links, (a, b) =>
                    {
                        Assert.Equal(a.Class, b.Class);
                        Assert.Equal(a.Href, b.Href);
                        Assert.Equal(a.Rel, b.Rel);
                        Assert.Equal(a.Title, b.Title);
                        Assert.Equal(a.Type, b.Type);
                        Assert.Equal(a.Media, b.Media);
                    });

                    AssertCollection(expected.Head.Metas, actual.Head.Metas, (a, b) =>
                    {
                        Assert.Equal(a.Charset, b.Charset);
                        Assert.Equal(a.Name, b.Name);
                        Assert.Equal(a.Content, b.Content);
                    });
                }

                Assert.Equal(expected.Body == null, actual.Body == null);
                if (expected.Body != null && actual.Body != null)
                {
                    Assert.Equal(expected.Body.Dom == null, actual.Body.Dom == null);
                    AssertCollection(expected.Body.Navs, actual.Body.Navs, (a, b) =>
                    {
                        Assert.Equal(a.Dom == null, b.Dom == null);
                        Assert.Equal(a.Class, b.Class);
                        Assert.Equal(a.Hidden, b.Hidden);
                        Assert.Equal(a.Id, b.Id);
                        Assert.Equal(a.Type, b.Type);
                    });
                }
            }
        }
    }
}
