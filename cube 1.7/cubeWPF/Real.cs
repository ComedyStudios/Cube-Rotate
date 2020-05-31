using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Shapes;

namespace cubeWPF
{
     class Real
    {
        public List<Line> MakeLine(List<Lines> linesV, List<Line>linesR, List<Cordinate>points, float centerX, float centerY)
        {
            


            foreach (var line in linesV)
            {
                linesR.Add(
                    new Line
                    {
                        Stroke = System.Windows.Media.Brushes.Black,
                        X1 = centerX + points[line.id1].x,
                        Y1 = centerY + points[line.id1].y * (-1),
                        X2 = centerX + points[line.id2].x,
                        Y2 = centerY + points[line.id2].y * (-1),
                        StrokeThickness = 3,
                    }
                );
            }

            return linesR;
        }


        public void MakeUnvsible( List<Lines> linesV, List<Line> linesR, List<Cordinate>p)
        {
            var unvisiablePointsId = new List<int>();
            double furthestPoint = 0;
            foreach (var point in p)
            {
                if (point.z > furthestPoint)
                {
                    furthestPoint = point.z;
                }
            }

            for (var i = 0; i < p.Count; i++)
            {
                if (p[i].z == furthestPoint)
                {
                    unvisiablePointsId.Add(i);
                }
            }

            CheckVisibility(unvisiablePointsId, linesV, linesR);

            //return unvisiablePointsId;
        }
        public void CheckVisibility(List<int> unvisiablePointsId, List<Lines> linesV, List<Line>linesR)
        {
            foreach (var invisibleId in unvisiablePointsId)
            {
                for (int j = 0; j < linesV.Count; j++)
                {
                    var line = linesV[j];
                    if (line.id1 == invisibleId || line.id2 == invisibleId ||
                        line.id1 == invisibleId && line.id2 == invisibleId)
                    {
                        linesR[j].Opacity = 0;
                    }
                    else

                    {
                        linesR[j].Opacity = 1;
                    }
                }
            }
        }

        public void RotatingOnY( List<Cordinate>point, List<Line>lineR,List<Lines> linesV,  float centerX, float centerY, double rotatingAngleY)
        {
            List<Cordinate> newp = new List<Cordinate>();
            foreach (var p in point)
            {
                var yVirtual = new Virtual();
                newp.Add(yVirtual.RotatingOnTheYAxis(p.x, p.y, p.z, rotatingAngleY));
            };

            var i = 0;
            foreach (var line in lineR)
            {
                line.X1 = newp[linesV[i].id1].x + centerX;
                line.Y1 = newp[linesV[i].id1].y * (-1) + centerY;
                line.X2 = newp[linesV[i].id2].x + centerX;
                line.Y2 = newp[linesV[i].id2].y * (-1) + centerY;
                i++;
            }

            var i2 = 0;
            while (i2 < point.Count)
            {
                point[i2] = newp[i2];
                i2++;
            }

            MakeUnvsible(linesV, lineR, point );
        }
        public void RotatingOnX( List<Cordinate> point, List<Line> lineR, List<Lines> linesV, float centerX, float centerY, double rotatingAngleX)
        {
            List<Cordinate> newp = new List<Cordinate>();
            foreach (var p in point)
            {
                var yVirtual = new Virtual();
                newp.Add(yVirtual.RotatingOnTheXAxis(p.x, p.y, p.z, rotatingAngleX));
            };

            var i = 0;
            foreach (var line in lineR)
            {
                line.X1 = newp[linesV[i].id1].x + centerX;
                line.Y1 = newp[linesV[i].id1].y * (-1) + centerY;
                line.X2 = newp[linesV[i].id2].x + centerX;
                line.Y2 = newp[linesV[i].id2].y * (-1) + centerY;
                i++;
            }

            var i2 = 0;
            while (i2 < point.Count)
            {
                point[i2] = newp[i2];
                i2++;
            }

            MakeUnvsible(linesV, lineR, point);
        }
    }
}
