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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pract_work_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private SolidColorBrush lg = new SolidColorBrush(Colors.LightGray);
        private SolidColorBrush rd = new SolidColorBrush(Colors.Red);
        private SolidColorBrush gr = new SolidColorBrush(Colors.Green);
        private SolidColorBrush bl = new SolidColorBrush(Colors.Blue);

        private RotateTransform3D hsRt;
        private AxisAngleRotation3D hsAx;
        private Transform3DGroup hsTrGr;

        private DispatcherTimer MyTimer;
        private DispatcherTimer DoorTimer;
        private DispatcherTimer WindowTimer;

        private Transform3DGroup drTrGr;
        private TranslateTransform3D drTr;

        private Transform3DGroup wnTrGr;
        private TranslateTransform3D wnTr;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hsRt = new RotateTransform3D();
            hsAx = new AxisAngleRotation3D();
            hsAx.Axis = new Vector3D(0, 1, 0);
            hsAx.Angle = 0;
            hsRt.Rotation = hsAx;

            hsTrGr = new Transform3DGroup();
            HouseModel.Transform = hsTrGr;
            RoofModel.Transform = hsTrGr;
            hsTrGr.Children.Add(hsRt);

            MyTimer = new DispatcherTimer();
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Interval = new TimeSpan(100000);

            drTrGr = new Transform3DGroup();
            DoorModel.Transform = drTrGr;
            drTrGr.Children.Add(hsRt);

            drTr = new TranslateTransform3D();
            drTr.OffsetX = 0;
            drTrGr.Children.Add(drTr);

            DoorTimer = new DispatcherTimer();
            DoorTimer.Tick += new EventHandler(DoorTimer_Tick);
            DoorTimer.Interval = new TimeSpan(10000);

            wnTrGr = new Transform3DGroup();
            WindowModel.Transform = wnTrGr;
            wnTrGr.Children.Add(hsRt);

            wnTr = new TranslateTransform3D();
            wnTr.OffsetY = 0;
            wnTrGr.Children.Add(wnTr);

            WindowTimer = new DispatcherTimer();
            WindowTimer.Tick += new EventHandler(WindowTimer_Tick);
            WindowTimer.Interval = new TimeSpan(10000);
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            hsAx.Angle += dr;
        }

        private void DoorTimer_Tick(object sender, EventArgs e)
        {
            drTr.OffsetX += drgl;
            if (drTr.OffsetX >= 0.18 || drTr.OffsetX <= 0)
            {
                DoorTimer.Stop();
            }
        }

        private void WindowTimer_Tick(object sender, EventArgs e)
        {
            wnTr.OffsetY += wngl;
            if (wnTr.OffsetY >= 0.2 || wnTr.OffsetY <= 0)
            {
                WindowTimer.Stop();
            }
        }

        private void btP_Click(object sender, RoutedEventArgs e)
        {
            RotationControl(0);
        }

        private void btL_Click(object sender, RoutedEventArgs e)
        {
            RotationControl(1);
        }

        private void btR_Click(object sender, RoutedEventArgs e)
        {
            RotationControl(2);
        }

        private void btDoor_Click(object sender, RoutedEventArgs e)
        {
            DoorControl();
        }

        private void btWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowControl();
        }

        int dr = 0;
        private void RotationControl(int i)
        {
            switch (i)
            {
                case 0:
                    btP.Background = bl;
                    btL.Background = lg;
                    btR.Background = lg;
                    dr = 0;
                    MyTimer.Stop();
                    break;

                case 1:
                    btP.Background = lg;
                    btL.Background = bl;
                    btR.Background = lg;
                    dr = -1;
                    MyTimer.Start();
                    break;

                case 2:
                    btP.Background = lg;
                    btL.Background = lg;
                    btR.Background = bl;
                    dr = 1;
                    MyTimer.Start();
                    break;

                default:
                    btP.Background = lg;
                    btL.Background = lg;
                    btR.Background = lg;

                    break;
            }
        }

        private bool DoorStatus = false;
        private double drgl;
        private void DoorControl()
        {
            DoorStatus = !DoorStatus;

            if (DoorStatus)
            {
                btDoor.Background = gr;
                drgl = 0.01;
                DoorTimer.Start();
            }
            else
            {
                btDoor.Background = rd;
                drgl = -0.01;
                DoorTimer.Start();
            }
        }

        private bool WindowStatus = false;
        private double wngl;
        private void WindowControl()
        {
            WindowStatus = !WindowStatus;

            if(WindowStatus)
            {
                btWindow.Background = gr;
                wngl = 0.01;
                WindowTimer.Start();
            }
            else
            {
                btWindow.Background = rd;
                wngl = -0.01;
                WindowTimer.Start();
            }
        }
    }
}