using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cubeWPF
{
    public class Virtual
    {

        public Cordinate RotatingOnTheYAxis(double x, double y, double z, double rotatingAngleY)
        {
            Cordinate currentCubeCordinates = new Cordinate();
            var angleToRadian = rotatingAngleY * Math.PI / 180;
            currentCubeCordinates.x = x * Math.Cos(angleToRadian) + z * Math.Sin(angleToRadian);
            currentCubeCordinates.y = y;
            currentCubeCordinates.z = x * -Math.Sin(angleToRadian) + z * Math.Cos(angleToRadian);
            return currentCubeCordinates;
        }

        public Cordinate RotatingOnTheXAxis(double x, double y, double z, double rotatingAngleX)
        {
            Cordinate currentCubeCordinates = new Cordinate();
            var angleToRadian = rotatingAngleX * Math.PI / 180;
            currentCubeCordinates.x = x;
            currentCubeCordinates.y = y * Math.Cos(angleToRadian) - Math.Sin(angleToRadian) * z;
            currentCubeCordinates.z = y * Math.Sin(angleToRadian) + z * Math.Cos(angleToRadian);
            return currentCubeCordinates;
        }
    }
}
