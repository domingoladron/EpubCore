using System.Collections.Generic;
using System.Linq;
using System.Text;
using EpubCore.Format;
using EpubCore.Misc;

namespace EpubCore
{
    public class EpubBook
    {
        internal const string AuthorsSeparator = ", ";

        /// <summary>
        /// Read-only raw epub format structures.
        /// </summary>
        public EpubFormat Format { get; internal set; }

        public string Title => Format.Opf.Metadata.Titles.FirstOrDefault() ?? string.Empty;

        public IList<string> Titles => Format.Opf.Metadata.Titles;

        public IEnumerable<string> Authors => Format.Opf.Metadata.Creators.Select(creator => creator.Text);

        public IList<OpfMetadataCreator> Contributors => Format.Opf.Metadata.Contributors;

        public IEnumerable<string> Publishers => Format.Opf.Metadata.Publishers;

        public string UniqueIdentifier => Format.Opf.UniqueIdentifier;

        public EpubVersion EpubVersion => Format.Opf.EpubVersion;


        /// <summary>
        /// All files within the EPUB.
        /// </summary>
        public EpubResources Resources { get; internal set; }

        /// <summary>
        /// EPUB format specific resources.
        /// </summary>
        public EpubSpecialResources SpecialResources { get; internal set; }

        public byte[] CoverImage { get; internal set; }
        public string CoverImageHref { get; set; }

        public IList<EpubChapter> TableOfContents { get; internal set; }
      

        public string ToPlainText()
        {
            var builder = new StringBuilder();
            foreach (var html in SpecialResources.HtmlInReadingOrder)
            {
                builder.Append(Html.GetContentAsPlainText(html.TextContent));
                builder.Append('\n');
            }
            return builder.ToString().Trim();
        }

        public EpubTextFile FetchHtmlFile(EpubChapter ePubChapter)
        {
            var nameOfFile = ePubChapter.RelativePath;
            var possibleFile = this.Resources.Html.FirstOrDefault(g => g.FileName.Equals(nameOfFile));
            return possibleFile;
        }
    }
}
