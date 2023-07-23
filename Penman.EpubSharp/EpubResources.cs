using System.Collections.Generic;
using System.Linq;

namespace Penman.EpubSharp
{
    public class EpubResources
    {
        public IList<EpubTextFile> Html { get; internal set; } = new List<EpubTextFile>();
        public IList<EpubTextFile> Css { get; internal set; } = new List<EpubTextFile>();
        public IList<EpubByteFile> Images { get; internal set; } = new List<EpubByteFile>();
        public IList<EpubByteFile> Fonts { get; internal set; } = new List<EpubByteFile>();
        public IList<EpubFile> Other { get; internal set; } = new List<EpubFile>();

        /// <summary>
        /// This is a concatenation of all the resources files in the epub: html, css, images, etc.
        /// </summary>
        public IList<EpubFile> All { get; internal set; } = new List<EpubFile>();


        public EpubTextFile FindExistingStylesheet(string fileName)
        {
            return Css.FirstOrDefault(g => g.FileName.ToLower().Equals(fileName.ToLower()));
        }

        public EpubTextFile FindExistingHtml(string fileName)
        {
            return Html.FirstOrDefault(g => g.FileName.ToLower().Equals(fileName.ToLower()));
        }

        public bool RemoveHtml(string htmlFileName)
        {
            var html = FindExistingHtml(htmlFileName);
            return html != null && Html.Remove(html);
        }

        public bool RemoveCss(string cssFileName)
        {
            var css = FindExistingStylesheet(cssFileName);
            return css != null && Css.Remove(css);
        }
    }
}