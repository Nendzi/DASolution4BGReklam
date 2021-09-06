using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public class CustomizedBSplineCurve : CommonMethods, ICustomizedObject
    {
        private BSplineCurve oBSplineCurve;
        private Edge _edge;
        public CustomizedBSplineCurve(Edge edge, InventorServer iApp) : base(iApp)
        {
            oBSplineCurve = edge.Geometry;
            _edge = edge;
        }
        public Edge GetEdge()
        {
            return _edge;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oBSplineCurve.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oBSplineCurve.Evaluator);
        }
        public Point GetXFromObject()
        {
            return GetXForCurve(oBSplineCurve.Evaluator, oBSplineCurve);
        }
    }
}
