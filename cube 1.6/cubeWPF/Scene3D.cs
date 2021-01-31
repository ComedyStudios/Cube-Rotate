using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using PLP3D = PLP.Engine3D;

namespace cubeWPF
{
    public class Scene3D
    {
        private readonly List<Line> realLines = new List<Line>();

        private PLP3D.ModelCube3D model = new PLP3D.ModelCube3D();

        public void Load(string path, float centerX, float centerY)
        {
            this.model = PLP3D.ModelCube3D.Load(path);

            foreach (var connection in model.LineConnections)
            {
                realLines.Add(
                    new Line
                    {
                        Stroke = System.Windows.Media.Brushes.Black,
                        X1 = centerX + model.Points[connection.Id1].X,
                        Y1 = centerY + model.Points[connection.Id1].Y * (-1),
                        X2 = centerX + model.Points[connection.Id2].X,
                        Y2 = centerY + model.Points[connection.Id2].Y * (-1),
                        StrokeThickness = 3,
                    }
                );
            }

            //rotating so the lines shown in the window update
            RotatingOnX(centerX, centerY, 0);
        }

        public void Save(string path)
        {
            this.model.Save(path);
        }

        public void Reset(float centerX, float centerY)
        {
            this.model.Reset();

            //rotating so the lines shown in the window update
            RotatingOnX(centerX, centerY, 0);
        }

        public void addToGrid(Grid grid)
        {
            foreach (Line line in realLines)
            {
                grid.Children.Add(line);
            }
        }

        //hide the lines that are at the back of the cube
        public void MakeUnvisible()
        {
            var unvisiablePointsId = new List<int>();
            double furthestPoint = 0;
            foreach (var point in model.Points)
            {
                if (point.Z > furthestPoint)
                {
                    furthestPoint = point.Z;
                }
            }

            for (var i = 0; i < model.Points.Count; i++)
            {
                if (model.Points[i].Z == furthestPoint)
                {
                    unvisiablePointsId.Add(i);
                }
            }
            foreach (var invisibleId in unvisiablePointsId)
            {
                for (int j = 0; j < model.LineConnections.Count; j++)
                {
                    var line = model.LineConnections[j];
                    if (line.Id1 == invisibleId || line.Id2 == invisibleId ||
                        line.Id1 == invisibleId && line.Id2 == invisibleId)
                    {
                        realLines[j].Opacity = 0;
                    }
                    else

                    {
                        realLines[j].Opacity = 1;
                    }
                }
            }

        }

        //rotates the line on the y and x axis 
        public void RotatingOnY(float centerX, float centerY, double rotatingAngleY)
        {
            var newPointsList = new List<PLP3D.Point3D>();
            foreach (var p in model.Points)
            {
                var newPoint = new PLP3D.Point3D(p);
                newPoint.RotatingOnTheYAxis(rotatingAngleY);
                newPointsList.Add(newPoint);
            }

            var i = 0;
            foreach (var line in realLines)
            {
                line.X1 = newPointsList[model.LineConnections[i].Id1].X + centerX;
                line.Y1 = newPointsList[model.LineConnections[i].Id1].Y * (-1) + centerY;
                line.X2 = newPointsList[model.LineConnections[i].Id2].X + centerX;
                line.Y2 = newPointsList[model.LineConnections[i].Id2].Y * (-1) + centerY;
                i++;
            }

            var i2 = 0;
            while (i2 < model.Points.Count)
            {
                model.Points[i2] = newPointsList[i2];
                i2++;
            }

            MakeUnvisible();
        }
        public void RotatingOnX( float centerX, float centerY, double rotatingAngleX)
        {
            var newPointsList = new List<PLP3D.Point3D>();
            foreach (var p in model.Points)
            {
                var newPoint = new PLP3D.Point3D(p);
                newPoint.RotatingOnTheXAxis(rotatingAngleX);
                newPointsList.Add(newPoint);
            }

            var i = 0;
            foreach (var line in realLines)
            {
                line.X1 = newPointsList[model.LineConnections[i].Id1].X + centerX;
                line.Y1 = newPointsList[model.LineConnections[i].Id1].Y * (-1) + centerY;
                line.X2 = newPointsList[model.LineConnections[i].Id2].X + centerX;
                line.Y2 = newPointsList[model.LineConnections[i].Id2].Y * (-1) + centerY;
                i++;
            }

            var i2 = 0;
            while (i2 < model.Points.Count)
            {
                model.Points[i2] = newPointsList[i2];
                i2++;
            }

            MakeUnvisible();
        }
    }
}
