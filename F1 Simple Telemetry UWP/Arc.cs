using System;
using System.Drawing;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace F1Tools
{
    public class Arc
    {
        private static object arc;

        public static Geometry Parse(string value)
        {
            var pathStr = "<Geometry xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">" + value + "</Geometry>";
            return (Geometry)XamlReader.Load(pathStr);
        }

        public static Geometry DrawArc(double r, double start, double end)
        {
            //// 起止角度间隔大于等于360°，直接画圆
            //
            //if (Math.Abs(start - end) >= 360)
            //{
            //    string data = $"M{x + h * 2},{y + w + 0.001} A{h},{w} 0,1,1 {x + h * 2},{y + w}z";
            //    return Parse(data);
            //}
            //
            //bool clockWise = end > start;
            //
            //// 判断优弧/劣弧
            //bool majorArc = Math.Abs(start - end) % 360 >= 180;
            //
            //// 将起始点和终止点转化为椭圆上的坐标
            //double rad = 0;
            //
            //Point startPoint = new Point();
            //if ((90 - start) % 360 == 0)
            //{
            //    startPoint.X = 0;
            //    startPoint.Y = w;
            //}
            //else if ((270 - start) % 360 == 0)
            //{
            //    startPoint.X = 0;
            //    startPoint.Y = -w;
            //}
            //else
            //{
            //    rad = GetRadian(start);
            //    startPoint.X = (int)(h * w / Math.Sqrt(Math.Pow(w, 2) + Math.Pow(h * Math.Tan(rad), 2)));
            //    startPoint.X *= Math.Cos(rad) > 0 ? 1 : -1;
            //    startPoint.Y = (int)(startPoint.X * Math.Tan(rad));
            //}
            //
            //Point endPoint = new Point();
            //if ((90 - end) % 360 == 0)
            //{
            //    endPoint.X = 0;
            //    endPoint.Y = w;
            //}
            //else if ((270 - end) % 360 == 0)
            //{
            //    endPoint.X = 0;
            //    endPoint.Y = -w;
            //}
            //else
            //{
            //    rad = GetRadian(end);
            //    endPoint.X = (int)(h * w / Math.Sqrt(Math.Pow(w, 2) + Math.Pow(h * Math.Tan(rad), 2)));
            //    endPoint.X *= Math.Cos(rad) > 0 ? 1 : -1;
            //    endPoint.Y = (int)(endPoint.X * Math.Tan(rad));
            //}
            //
            //string pathData = $"M{startPoint.X + h + x},{startPoint.Y + w + y} ";
            //pathData += $"A{h},{w} ";
            //pathData += $"0,{(majorArc ? "1" : "0")},{(clockWise ? "1" : "0")} ";
            //pathData += $"{endPoint.X + h + x},{endPoint.Y + w + y}";
            //return Parse(pathData);

            int isLargeArcFlag = Math.Abs(start - end) > 180 ? 1 : 0;
            int sweepDirectionFlag = start > end ? 0 : 1;
            //M 250,0  A 250,250 0 1 1 0,250
            //250 250 
            var sdx = r * Math.Sin(GetRadian(start));
            var sdy = r * Math.Cos(GetRadian(start));
            //250 - x  500-y
            var edx = r * Math.Sin(GetRadian(end));
            var edy = r * Math.Cos(GetRadian(end));
            string s = $"M {250 - sdx},{250 + sdy}  A {r},{r} 0 {isLargeArcFlag} {sweepDirectionFlag} {250 - edx},{250 + edy}";
            return Parse(s);
        }

        private static double GetRadian(double angle)
        {
            return angle / 180.0 * Math.PI;
        }
    }
}

