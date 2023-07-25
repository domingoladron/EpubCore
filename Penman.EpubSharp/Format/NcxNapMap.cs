using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Penman.EpubSharp.Format;

public class NcxNapMap
{
    /// <summary>
    /// Populated only when an EPUB with NCX is read.
    /// </summary>
    public XElement Dom { get; internal set; }
    public IList<NcxNavPoint> NavPoints { get; internal set; } = new List<NcxNavPoint>();

    /// <summary>
    /// Reorder the Nav Points by their Play Order
    /// </summary>
    public  void ReorderNavPointsPlayOrder()
    {
        var myNavPoints = NavPoints;
        if (myNavPoints == null) return;
        myNavPoints = myNavPoints.OrderBy(x => x.PlayOrder).ToList();
        for (var x = 0; x < myNavPoints.Count; x++)
        {
            var curItem = myNavPoints[x];
            var curNavPoint = NavPoints.FirstOrDefault(g => g.Id.Equals(curItem.Id));
            if (curNavPoint != null)
            {
                curNavPoint.PlayOrder = x;
            }
        }
    }
}