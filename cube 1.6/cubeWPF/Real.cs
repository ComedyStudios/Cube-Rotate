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

        public void MakeUnvisible(List<int> unvisiablePointsId, List<Lines> linesV, List<Line>linesR)
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
    }
}
