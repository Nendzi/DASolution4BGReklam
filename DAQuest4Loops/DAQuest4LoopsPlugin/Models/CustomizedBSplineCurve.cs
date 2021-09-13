using Inventor;
using Newtonsoft.Json;

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
        public string GetEdgeData()
        {
            string result;
            BSplineCurveData bSplineCurveData = new BSplineCurveData();
            bSplineCurveData.edgeType = _edge.GeometryType.ToString();
            oBSplineCurve.GetBSplineData(ref bSplineCurveData.pole, ref bSplineCurveData.knots, ref bSplineCurveData.weights);
            result = JsonConvert.SerializeObject(bSplineCurveData);
            return result;
        }

        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oBSplineCurve.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oBSplineCurve.Evaluator);
        }
        internal class BSplineCurveData
        {
            public string edgeType;
            public double[] pole = new double[3];
            public double[] knots = new double[3];
            public double[] weights = new double[6];
        }
    }
}
