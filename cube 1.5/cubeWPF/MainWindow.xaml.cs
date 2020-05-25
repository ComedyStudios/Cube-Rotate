using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Schema;
using System.Threading;
using System.Windows.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;

namespace cubeWPF
{
    public partial class MainWindow : Window
    {
        
        private double rotatingAngle = 1;

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
        public MainWindow()
        {
            
            InitializeComponent();


            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += VectorRotation;

            float centerX = (float)grid.Width / 2;
            float centerY = (float)grid.Height / 2;

   
            foreach (var line in _Lines)
            {
                
                    _lines.Add  (
                                new Line 
                                {
                                    Stroke = System.Windows.Media.Brushes.Black,
                                    X1 = centerX + _points[line.id1].x,
                                    Y1 = centerY + _points[line.id1].y * (-1),
                                    X2 = centerX + _points[line.id2].x,
                                    Y2 = centerY + _points[line.id2].y * (-1),
                                    StrokeThickness = 3,
                                }
                                 );
            }

            foreach (Line line in _lines)
            {
                grid.Children.Add(line);
            }
            grid.UpdateLayout();
            
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);            
            
            if (e.Key == Key.Enter)
            {
               
                timer.Start();
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Key.Enter)
            {
                timer.Stop();
            }
        }



        private void VectorRotation(object sender, EventArgs e)
        {
            RotatingOnY();
            RotaingOnX();
        }

        private void RotaingOnX()
        {
            List<Cordinate> newp = new List<Cordinate> ();
            foreach (var p in _points)
            {
                newp.Add(RotatingOnTheXAxis(p.x, p.y, p.z));
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
        }

        private void RotatingOnY()
        {
            List<Cordinate> newp = new List<Cordinate>();
            foreach (var p in _points)
            {
                newp.Add(RotatingOnTheYAxis(p.x, p.y, p.z));
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

          
        }
        private Cordinate RotatingOnTheYAxis(double x, double y, double z)
        {
            Cordinate currentCubeCordinates = new Cordinate();
            var angleToRadian = rotatingAngle * Math.PI / 180;
            currentCubeCordinates.x = x * Math.Cos(angleToRadian) + z * Math.Sin(angleToRadian);
            currentCubeCordinates.y = y; 
            currentCubeCordinates.z = x * -Math.Sin(angleToRadian) + z * Math.Cos(angleToRadian);
            return currentCubeCordinates;
        }
        private Cordinate RotatingOnTheXAxis(double x, double y, double z)
        {
            Cordinate currentCubeCordinates = new Cordinate();
            var angleToRadian = rotatingAngle * Math.PI / 180;
            currentCubeCordinates.x = x;
            currentCubeCordinates.y = y * Math.Cos(angleToRadian) - Math.Sin(angleToRadian) * z;
            currentCubeCordinates.z = y * Math.Sin(angleToRadian) + z * Math.Cos(angleToRadian);
            return currentCubeCordinates;
        }
    }
}
