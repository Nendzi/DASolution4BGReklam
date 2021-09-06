using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public interface ICustomizedObject
    {
        Point GetXFromObject();
        Point GetStartPointFromObject();
        Point GetEndPointFromObject();
        Edge GetEdge();
    }
}