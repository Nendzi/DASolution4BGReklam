using DAQuest4LoopsPlugin.Models;
using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAQuest4LoopsPlugin
{
    public class CommonMethods
    {
        private InventorServer _iApp;
        public CommonMethods(InventorServer inventorServer)
        {
            _iApp = inventorServer;
        }
        public Point GetPointFromArray(double[] inputArray)
        {
            Point result = _iApp.TransientGeometry.CreatePoint();
            result.X = inputArray[0];
            result.Y = inputArray[1];
            result.Z = inputArray[2];

            return result;
        }
        public void CompareXValueWithExteremes(object Caller, double inputValue)
        {
            if (ExtremeValues.XHasValue)
            {
                if (inputValue < ExtremeValues.MinX)
                {
                    ExtremeValues.MinX = inputValue;
                    ExtremeValues.CurveOnMinX = Caller;
                }
                else if (inputValue > ExtremeValues.MaxX)
                {
                    ExtremeValues.MaxX = inputValue;
                    ExtremeValues.CurveOnMaxX = Caller;
                }
            }
            else
            {
                ExtremeValues.MinX = inputValue;
                ExtremeValues.MaxX = inputValue;
                ExtremeValues.XHasValue = true;
            }
        }
        public Point GetXForLine(LineSegment oLine)
        {
            double[] StartPoint = new double[3];
            double[] EndPoint = new double[3];
            oLine.GetLineSegmentData(StartPoint, EndPoint);
            CompareXValueWithExteremes(oLine, StartPoint[0]);
            CompareXValueWithExteremes(oLine, EndPoint[0]);

            return GetPointFromArray(StartPoint);
        }
        public Point GetXForCurve(CurveEvaluator curveEvaluator, object caller)
        {
            double[] Params = new double[1];
            double[] Points = new double[3];
            curveEvaluator.GetParamExtents(out double Param1, out double Param2);
            for (double i = Param1; i < Param2; i += 0.001)
            {
                Params[0] = i;
                curveEvaluator.GetPointAtParam(Params, Points);
                CompareXValueWithExteremes(caller, Points[0]);
            }
            return GetPointFromArray(Points);
        }
        public Point GetStartPoint(CurveEvaluator curveEvaluator)
        {
            double[] point = GetPoint(curveEvaluator, CurvePoint.Start);
            return GetPointFromArray(point);
        }
        public Point GetEndPoint(CurveEvaluator curveEvaluator)
        {
            double[] point = GetPoint(curveEvaluator, CurvePoint.End);
            return GetPointFromArray(point);
        }
        private double[] GetPoint(CurveEvaluator curveEvaluator, CurvePoint point)
        {
            double[] StartPoint = new double[3];
            double[] EndPoint = new double[3];

            curveEvaluator.GetEndPoints(ref StartPoint, ref EndPoint);

            if (point == CurvePoint.Start)
            {
                return StartPoint;
            }
            else
            {
                return EndPoint;
            }
        }
    }
}
