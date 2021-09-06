using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public class CustomizedLineSegment : CommonMethods, ICustomizedObject
    {
        private LineSegment oLine;
        private Edge _edge;
        public CustomizedLineSegment(Edge edge, InventorServer iApp) : base(iApp)
        {
            oLine = edge.Geometry;
            _edge = edge;
        }
        public Edge GetEdge()
        {
            return _edge;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oLine.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oLine.Evaluator);
        }
        public Point GetXFromObject()
        {
            return GetXForLine(oLine);
        }
    }
}
