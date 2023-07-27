using System.Collections.Generic;
using System.Xml.Linq;

namespace EpubCore.Format;

public class NcxNavPoint
{
    internal static class Attributes
    {
        public static readonly XName Id = "id";
        public static readonly XName Class = "class";
        public static readonly XName PlayOrder = "playOrder";
        public static readonly XName ContentSrc = "src";
    }

    public string Id { get; set; }
    public string Class { get; set; }
    public int? PlayOrder { get; set; }
    // NavLabelText and ContentSrc are flattened elements for convenience.
    // In case <navLabel> or <content/> need to carry more data, then they should have a dedicated model created.
    public string NavLabelText { get; set; }
    public string ContentSrc { get; set; }
    public IList<NcxNavPoint> NavPoints { get; internal set; } = new List<NcxNavPoint>();

    public override string ToString()
    {
        return $"Id: {Id}, ContentSource: {ContentSrc}";
    }
}