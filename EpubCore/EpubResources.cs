using System.Collections.Generic;
using System.Linq;

namespace EpubCore
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

        public EpubByteFile FindExistingImage(string fileName)
        {
            return Images.FirstOrDefault(g => g.Href.ToLower().Equals(fileName.ToLower()));
        }

        public EpubByteFile FindExistingFont(string fileName)
        {
            return Fonts.FirstOrDefault(g => g.Href.ToLower().Equals(fileName.ToLower()));
        }

        public EpubFile FindExistingOther(string fileName)
        {
            return Other.FirstOrDefault(g => g.Href.ToLower().Equals(fileName.ToLower()));
        }

        public bool RemoveResource(string resourceName, EpubResourceType resourceType)
        {
            switch (resourceType) 
            {
                case EpubResourceType.Html:
                    return RemoveHtml(resourceName);
                case EpubResourceType.Css:
                    return RemoveCss(resourceName);
                case EpubResourceType.Image:
                    return RemoveImage(resourceName);
                case EpubResourceType.Font:
                    return RemoveFont(resourceName);
                default:
                    return RemoveOther(resourceName);
            }
        }

        private bool RemoveOther(string resourceName)
        {
            var resource = FindExistingOther(resourceName);
            return resource != null && Other.Remove(resource);
        }

        private bool RemoveFont(string resourceName)
        {
            var resource = FindExistingFont(resourceName);
            return resource != null && Fonts.Remove(resource);
        }

        private bool RemoveImage(string resourceName)
        {
            var resource = FindExistingImage(resourceName);
            return resource != null && Images.Remove(resource);
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