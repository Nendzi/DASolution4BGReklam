using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public class CustomizedCircle : CommonMethods, ICustomizedObject
    {
        private Circle oCircle;
        private Edge _edge;
        public CustomizedCircle(Edge edge, InventorServer iApp) : base(iApp)
        {
            oCircle = edge.Geometry;
            _edge = edge;
        }
        public Edge GetEdge()
        {
            return _edge;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oCircle.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oCircle.Evaluator);
        }
        public Point GetXFromObject()
        {
            return GetXForCurve(oCircle.Evaluator, oCircle);
        }
    }
}
