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

        //List of points for the lines
        

        private Scene3D scene;

        DispatcherTimer timer = new DispatcherTimer();

        private string jsonPath = @".\json1.json";

        public MainWindow()
        {
            InitializeComponent();

            centerX = (float)grid.Width / 2;
            centerY = (float)grid.Height / 2;
            timer.Interval = new TimeSpan(0,0,0,0,30);

            // creates lines and adds them to the window grid
            scene = new Scene3D();
            scene.Load(jsonPath, centerX, centerY);

            scene.addToGrid(grid);

            // is needed beacause else if we start the window and have no input the lines will be visible
            scene.MakeUnvisible();
        }


        //saves the rotaiton if needed
        protected override void OnClosed(EventArgs e)
        {
            if (save.IsChecked == true)
            {
                scene.Save(jsonPath);
            }

            base.OnClosed(e);
        }

        //rotates if button is pressed 
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

        //stops rotaiton if the button isnt pressed
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
            scene.RotatingOnX( centerX, centerY, rotatingAngleX);
        }

        private void RotaitingOnY(object sender, EventArgs e)
        {
            scene.RotatingOnY(centerX,centerY,rotatingAngleY);
        }

        private void ButtonBase_OnClicktn_reset(object sender, RoutedEventArgs e)
        {
            scene.Reset(centerX, centerY);
        }
    }
}
