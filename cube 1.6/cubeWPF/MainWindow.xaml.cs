using System;
using System.Collections.Generic;
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

        private float centerX;
        private float centerY;

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
        private List<Line> _lines = new List<Line>();

        DispatcherTimer timer = new DispatcherTimer();

        private string jsonPath = @".\json1.json";
        private JsonFormat Variables;
        Json json = new Json();
        
        Real real = new Real();
        public MainWindow()
        {
            InitializeComponent();

            Variables = json.Read(jsonPath);

            if (Variables != null)
            {
                _Lines = Variables.l;
                _points = Variables.p;
            }
            

            centerX = (float)grid.Width / 2;
            centerY = (float)grid.Height / 2;
            timer.Interval = new TimeSpan(0,0,0,0,30);

            
           _lines = real.MakeLine(_Lines,_lines,_points,centerX,centerY);

            foreach (Line line in _lines)
            {
                grid.Children.Add(line);
            }

            real.MakeUnivsible(_Lines, _lines, _points);
        }

        protected override void OnClosed(EventArgs e)
        {
            Variables = new JsonFormat
            {
                p = _points,
                l = _Lines
            };
            json.Wirte(Variables, jsonPath);
            base.OnClosed(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            timer.Tick -= RotaitingOnY;
            timer.Tick -= RotaitingOnX;
            switch (e.Key)
            {
                case Key.A:
                    rotatingAngleY = 1;
                    timer.Tick += RotaitingOnY;
                    timer.Start();
                    break;
                case Key.D:
                    rotatingAngleY = -1;
                    timer.Tick +=RotaitingOnY;
                    timer.Start();
                    break;
                case Key.W:
                    rotatingAngleX = 1;
                    timer.Tick += RotaitingOnX;
                    timer.Start();
                    break;
                case Key.S:
                    rotatingAngleX = -1;
                    timer.Tick += RotaitingOnX;
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

        private void RotaitingOnX(object sender, EventArgs e)
        {
            real.RotatingOnX(_points, _lines, _Lines, centerX, centerY, rotatingAngleX);
        }

        private void RotaitingOnY(object sender, EventArgs e)
        {
            real.RotatingOnY(_points,_lines,_Lines,centerX,centerY,rotatingAngleY);
        }
    }
}
