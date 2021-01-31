using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLP.Engine3D
{
    /// <summary>
    /// This class stores coordinates of the 3D Point.
    /// </summary>
    public class Point3D
    {
        public Point3D()
        {
            
        }

        public Point3D(Point3D src)
        {
            this.X = src.X;
            this.Y = src.Y;
            this.Z = src.Z;
        }

        public Point3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

      
        public void RotatingOnTheYAxis(double rotatingAngleY)
        {
            var rotationAngle = rotatingAngleY * Math.PI / 180;
            this.X = this.X * Math.Cos(rotationAngle) + this.Z * Math.Sin(rotationAngle);
            this.Z = this.X * -Math.Sin(rotationAngle) + this.Z * Math.Cos(rotationAngle);
        }

        public void RotatingOnTheXAxis(double rotatingAngleX)
        {
            var rotationAngle = rotatingAngleX * Math.PI / 180;
            this.Y = this.Y * Math.Cos(rotationAngle) - this.Z * Math.Sin(rotationAngle);
            this.Z = this.Y * Math.Sin(rotationAngle) + this.Z * Math.Cos(rotationAngle);
        }
    }
}
