using Inventor;
using Newtonsoft.Json;

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
        public string GetEdgeData()
        {
            string result;
            SegmentLineData segmentLineData = new SegmentLineData();
            segmentLineData.edgeType = _edge.GeometryType.ToString();
            oLine.GetLineSegmentData(ref segmentLineData.startPoint,ref segmentLineData.endPoint);
            result = JsonConvert.SerializeObject(segmentLineData);
            return result;
        }
        public Point GetEndPointFromObject()
        {
            return GetEndPoint(oLine.Evaluator);
        }
        public Point GetStartPointFromObject()
        {
            return GetStartPoint(oLine.Evaluator);
        }
        internal class SegmentLineData
        {
            public string edgeType;
            public double[] startPoint = new double[3];
            public double[] endPoint = new double[3];
        }
    }
}
