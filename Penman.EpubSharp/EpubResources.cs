using System.Collections.Generic;

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
    }
}