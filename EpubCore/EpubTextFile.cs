using EpubCore.Format;

namespace EpubCore
{
    public class EpubTextFile : EpubFile
    {
        public string TextContent
        {
            get => Constants.DefaultEncoding.GetString(Content, 0, Content.Length);
            set => Content = Constants.DefaultEncoding.GetBytes(value);
        }

        public string FileName { get; set; } = string.Empty;

        public string FullFilePath { get; set; } = string.Empty;

        public void ReplaceValue(string value1, string value2)
        {
            TextContent = TextContent.Replace(value1, value2);
        }
    }
}