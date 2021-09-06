using Inventor;

namespace DAQuest4LoopsPlugin.Models
{
    public class CustomizedArc3D : CommonMethods, ICustomizedObject
    {
        private Arc3d oArc3D;
        private Edge _edge;
        public CustomizedArc3D(Edge edge, InventorServer iApp) : base(iApp)
        {
            oArc3D = edge.Geometry;
            _edge = edge;
        }
        public Edge GetEdge()
        {
            return _edge;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oArc3D.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oArc3D.Evaluator);
        }
        public Point GetXFromObject()
        {
            return GetXForCurve(oArc3D.Evaluator,oArc3D);
        }
    }
}
