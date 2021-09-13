using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public interface ICustomizedObject
    {
        Point GetStartPointFromObject();
        Point GetEndPointFromObject();
        Edge GetEdge();
        string GetEdgeData();
    }
}