using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace cubeWPF
{
    public partial class MainWindow : Window
    {
        
        private double rotatingAngleX = 1;
        private double rotatingAngleY = 1; 

        private List<Cordinate> _points = new List<Cordinate> { 
            new Cordinate { x = -100, y = -100, z = -100 },
            new Cordinate { x = -100, y = 100, z = -100 },
            new Cordinate { x = 100, y = 100, z = -100 },
            new Cordinate { x = 100, y = -100, z = -100 },
            new Cordinate { x = 100, y = 100, z = 100 },
            new Cordinate { x = -100, y = 100, z = 100 },
            new Cordinate { x = -100, y = -100, z = 100 },
            new Cordinate { x = 100, y = -100, z = 100 },


        };
        private List<Lines> _Lines = new List<Lines>
        {
           new Lines {id1 = 0, id2 =1 },
           new Lines {id1 = 1, id2 =2 },
           new Lines {id1 = 2, id2 =3 },
           new Lines {id1 = 3, id2 =0 },
           new Lines {id1 = 2, id2 =4 },
           new Lines {id1 = 1, id2 =5 },
           new Lines {id1 = 0, id2 =6 },
           new Lines {id1 = 3, id2 =7 },
           new Lines {id1 = 4, id2 =5 },
           new Lines {id1 = 5, id2 =6 },
           new Lines {id1 = 6, id2 =7 },
           new Lines {id1 = 7, id2 =4 },
        };

        private string jsonPath = @".\json1.json";

        private List<Line> _lines = new List<Line>();
        DispatcherTimer timer = new DispatcherTimer();
        
        private List<int> _unvisiablePointsId = new List <int>();

        private JsonFormat Variables;

        Real real = new Real();
        public MainWindow()
        {
            InitializeComponent();

            Variables = JsonRead();

            if (Variables != null)
            {
                _Lines = Variables.l;
                _points = Variables.p;
            }
            

            float centerX = (float)grid.Width / 2;
            float centerY = (float)grid.Height / 2;
            timer.Interval = new TimeSpan(0,0,0,0,30);

            
           _lines = real.MakeLine(_Lines,_lines,_points,centerX,centerY);

            foreach (Line line in _lines)
            {
                grid.Children.Add(line);
            }

            MakeUnvisible();
        }

        protected override void OnClosed(EventArgs e)
        {
            // TODO: write to json
            Variables = new JsonFormat
            {
                p = _points,
                l = _Lines
            };
            JsonWrite(Variables);
            base.OnClosed(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            timer.Tick -= RotatingOnY;
            timer.Tick -= RotaitngOnX;
            switch (e.Key)
            {
                case Key.A:
                    rotatingAngleY = 1;
                    timer.Tick += RotatingOnY;
                    timer.Start();
                    break;
                case Key.D:
                    rotatingAngleY = -1;
                    timer.Tick +=RotatingOnY;
                    timer.Start();
                    break;
                case Key.W:
                    rotatingAngleX = 1;
                    timer.Tick += RotaitngOnX;
                    timer.Start();
                    break;
                case Key.S:
                    rotatingAngleX = -1;
                    timer.Tick += RotaitngOnX;
                    timer.Start();
                    break;

            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            switch (e.Key)
            {
                case Key.A:
                    timer.Stop();
                    break;
                case Key.D:
                    timer.Stop();
                    break;
                case Key.S:
                    timer.Stop();
                    break;
                case Key.W:
                    timer.Stop();
                    break;
            }
        }

        private void RotaitngOnX(object sender, EventArgs e)
        {
            List<Cordinate> newp = new List<Cordinate> ();
            foreach (var p in _points)
            {
                var xVirtual = new Virtual();
                newp.Add(xVirtual.RotatingOnTheXAxis(p.x, p.y, p.z, rotatingAngleX));
            };

            var i = 0; 
            foreach(var line in _lines)
            {
                var centerX = grid.Width / 2;
                var centerY = grid.Height / 2;
                line.X1 = newp[_Lines[i].id1].x + centerX;
                line.Y1 = newp[_Lines[i].id1].y * (-1) + centerY;
                line.X2 = newp[_Lines[i].id2].x + centerX;
                line.Y2 = newp[_Lines[i].id2].y*(-1)   +centerY;
                i++;    
            }

            var i2 = 0; 
            while (i2 < _points.Count)
            {
                _points[i2] = newp[i2];
                i2++;
            }

            MakeUnvisible();
        }

        private void RotatingOnY(object sender, EventArgs e)
        {
            List<Cordinate> newp = new List<Cordinate>();
            foreach (var p in _points)
            {
                var yVirtual = new Virtual();
                newp.Add(yVirtual.RotatingOnTheYAxis(p.x, p.y, p.z,rotatingAngleY));
            };

            var i = 0;
            foreach (var line in _lines)
            {
                var centerX = grid.Width / 2;
                var centerY = grid.Height / 2;
                line.X1 = newp[_Lines[i].id1].x + centerX;
                line.Y1 = newp[_Lines[i].id1].y * (-1) + centerY;
                line.X2 = newp[_Lines[i].id2].x + centerX;
                line.Y2 = newp[_Lines[i].id2].y * (-1) + centerY;
                i++;
            }

            var i2 = 0;
            while (i2 < _points.Count)
            {
                _points[i2] = newp[i2];
                i2++;
            }

            MakeUnvisible();
        }

        private void MakeUnvisible()
        {
            _unvisiablePointsId.Clear();
            double furthestPoint = 0;
            foreach (var point in _points)
            {
                if (point.z > furthestPoint)
                {
                    furthestPoint = point.z;
                }
            }

            for (var i = 0; i < _points.Count; i++)
            {
                if (_points[i].z == furthestPoint)
                {
                    _unvisiablePointsId.Add(i);
                }
            }

            real.MakeUnvisible(_unvisiablePointsId,_Lines,_lines);
        }


        private JsonFormat JsonRead()
        {
            var jsonContent = File.ReadAllText(jsonPath);
            JsonFormat deserializedProduct = JsonConvert.DeserializeObject<JsonFormat>(jsonContent);
            return deserializedProduct;

        }

        private void JsonWrite(JsonFormat json)
        {
            string jsonSring = JsonConvert.SerializeObject(json);

            File.WriteAllText(jsonPath, jsonSring);
        }
    }
}
