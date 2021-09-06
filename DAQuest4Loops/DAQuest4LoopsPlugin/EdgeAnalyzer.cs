using DAQuest4LoopsPlugin.Models;
using Inventor;
using System;
using System.Collections.Generic;

namespace DAQuest4LoopsPlugin
{
    public class EdgeAnalyzer
    {
        private readonly InventorServer _iApp;
        private readonly PartDocument _part;
        private PartComponentDefinition partCompDef;
        private SurfaceBodies surfaceBodies;
        private Edges allEdges;
        List<CurveNode> curveNodes = new List<CurveNode>();
        List<ICustomizedObject> customizedEdges = new List<ICustomizedObject>();

        public EdgeAnalyzer(InventorServer App, PartDocument part)
        {
            _iApp = App;
            _part = part;
            partCompDef = part.ComponentDefinition;
            surfaceBodies = partCompDef.SurfaceBodies;
        }
        public void Analyze()
        {
            GetAllEdges();
            FindExtemesOnXAxis();
            CreateNodesList();
            AddEdgesToNodes();
            //Napraviti listu sa čvorovima i nagibima krivih linija
            //Pronađi krajnju levu liniju i pronađi sve povezane linije
            //Napravi listu povezanih linija koji čine spoljašnju konturu
        }
        #region Common private function
        private void SeparateEdges()
        {
            foreach (Edge oEdge in allEdges)
            {
                if (oEdge.Geometry is Arc3d)
                {
                    ICustomizedObject oArc3D = new CustomizedArc3D(oEdge, _iApp);
                    customizedEdges.Add(oArc3D);
                }
                if (oEdge.Geometry is LineSegment)
                {
                    ICustomizedObject oLine = new CustomizedLineSegment(oEdge, _iApp);
                    customizedEdges.Add(oLine);
                }
                if (oEdge.Geometry is BSplineCurve)
                {
                    ICustomizedObject oBSplineCurve = new CustomizedBSplineCurve(oEdge, _iApp);
                    customizedEdges.Add(oBSplineCurve);
                }
                if (oEdge.Geometry is EllipticalArc)
                {
                    ICustomizedObject oEllipticalArc = new CustomizedEllipticalArc(oEdge, _iApp);
                    customizedEdges.Add(oEllipticalArc);
                }
                if (oEdge.Geometry is Circle)
                {
                    ICustomizedObject oCircle = new CustomizedCircle(oEdge, _iApp);
                    customizedEdges.Add(oCircle);
                }
            }
        }
        #endregion
        #region Get all edges
        private void GetAllEdges()
        {
            allEdges = surfaceBodies[1].Edges;
            SeparateEdges();
        }
        #endregion
        #region Find exteme values on X axis
        private void FindExtemesOnXAxis()
        {
            foreach (ICustomizedObject oEdge in customizedEdges)
            {
                oEdge.GetXFromObject();
            }
        }
        #endregion
        #region Nodes list Creation
        public void CreateNodesList()
        {
            foreach (ICustomizedObject oEdge in customizedEdges)
            {
                CurveNode curveNode = new CurveNode
                {
                    NodePoint = oEdge.GetEndPointFromObject()
                };
                curveNodes.Add(curveNode);
            }
            int j = curveNodes.Count - 1;
            for (int i = 0; i < j; i++)
            {
                RemoveDuplicates(i);
                j = curveNodes.Count - 1;
            }
        }
        private void RemoveDuplicates(int index)
        {
            Point refPoint = curveNodes[index].NodePoint;
            for (int i = curveNodes.Count - 1; i >= 0; i--)
            {
                CurveNode item = curveNodes[i];
                if (PointCompare(item.NodePoint, refPoint))
                {
                    curveNodes.Remove(item);
                }
            }
        }
        private bool PointCompare(Point a, Point b)
        {
            bool result = false;
            bool Cond1 = Math.Abs(a.X - b.X) < 0.000001;
            bool Cond2 = Math.Abs(a.Y - b.Y) < 0.000001;
            bool Cond3 = Math.Abs(a.Z - b.Z) < 0.000001;

            if (Cond1 && Cond2 && Cond3)
            {
                result = true;
            }
            return result;
        }
        #endregion
        #region Add Edges to their nodes
        public void AddEdgesToNodes()
        {
            foreach (CurveNode item in curveNodes)
            {
                foreach (ICustomizedObject edge in customizedEdges)
                {
                    if (item.NodePoint == edge.GetStartPointFromObject() || item.NodePoint == edge.GetEndPointFromObject())
                    {
                        item.Edges.Add(edge);
                    }
                }
            }
        }
        #endregion
    }
}
