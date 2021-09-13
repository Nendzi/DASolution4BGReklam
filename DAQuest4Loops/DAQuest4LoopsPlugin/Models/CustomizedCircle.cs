using Inventor;
using Newtonsoft.Json;

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
        public string GetEdgeData()
        {
            string result;
            CircleData circleData = new CircleData();
            circleData.edgeType = _edge.GeometryType.ToString();
            oCircle.GetCircleData(ref circleData.center, ref circleData.axisVector, out circleData.raduis);
            result = JsonConvert.SerializeObject(circleData);
            return result;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oCircle.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oCircle.Evaluator);
        }
        internal class CircleData
        {
            public string edgeType;
            public double[] center = new double[3];
            public double[] axisVector = new double[3];
            public double raduis = 0;
        }
    }
}