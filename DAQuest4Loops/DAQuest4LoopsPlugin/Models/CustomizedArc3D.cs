using Inventor;
using Newtonsoft.Json;

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
        public string GetEdgeData()
        {
            string result;
            ArcData arcData = new ArcData();
            arcData.edgeType = _edge.GeometryType.ToString();
            oArc3D.GetArcData(
                ref arcData.center,
                ref arcData.axisVector,
                ref arcData.refVector,
                out arcData.raduis,
                out arcData.startAngle,
                out arcData.sweepAngle
                );
            result = JsonConvert.SerializeObject(arcData);
            return result;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oArc3D.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oArc3D.Evaluator);
        }
        internal class ArcData
        {
            public string edgeType;
            public double[] center = new double[3];
            public double[] axisVector = new double[3];
            public double[] refVector = new double[3];
            public double raduis = 0;
            public double startAngle = 0;
            public double sweepAngle = 180;
        }
    }
}