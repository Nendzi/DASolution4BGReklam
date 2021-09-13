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