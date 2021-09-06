using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public class CustomizedEllipticalArc : CommonMethods, ICustomizedObject
    {
        private EllipticalArc oEllipticalArc;
        private Edge _edge;
        public CustomizedEllipticalArc(Edge edge, InventorServer iApp) : base(iApp)
        {
            oEllipticalArc = edge.Geometry;
            _edge = edge;
        }
        public Edge GetEdge()
        {
            return _edge;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oEllipticalArc.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oEllipticalArc.Evaluator);
        }
        public Point GetXFromObject()
        {
            return GetXForCurve(oEllipticalArc.Evaluator, oEllipticalArc);
        }
    }
}
