using Inventor;
using Newtonsoft.Json;

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
        public string GetEdgeData()
        {
            string result;
            EllipticArcData ellipticArcData = new EllipticArcData();
            ellipticArcData.edgeType = _edge.GeometryType.ToString();
            oEllipticalArc.GetEllipticalArcData(
                ref ellipticArcData.center, 
                ref ellipticArcData.majorAxis, 
                ref ellipticArcData.minorAxis,
                out ellipticArcData.majorRaduis, 
                out ellipticArcData.minorRaduis, 
                out ellipticArcData.startAngle, 
                out ellipticArcData.sweepAngle
                );
            result = JsonConvert.SerializeObject(ellipticArcData);
            return result;
        }

        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oEllipticalArc.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oEllipticalArc.Evaluator);
        }
        internal class EllipticArcData
        {
            public string edgeType;
            public double[] center = new double[3];
            public double[] majorAxis = new double[3];
            public double[] minorAxis = new double[3];
            public double minorRaduis = 0;
            public double majorRaduis = 0;
            public double startAngle = 0;
            public double sweepAngle = 180;
        }
    }
}
