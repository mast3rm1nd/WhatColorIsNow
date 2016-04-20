using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;
using System.Diagnostics;

namespace WhatColorIsNow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //static Label[] labels = new Label[10];


        static Label clocksLabel = new Label();
        static Label colorLabel = new Label();

        static int fontSize = (int)(System.Windows.SystemParameters.PrimaryScreenWidth / 10.5);

        Thread UpdatingThread;

        BrushConverter bc = new BrushConverter();

        public MainWindow()
        {
            InitializeComponent();

            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            
            
            //Grid_MainWindow.Background = (Brush)bc.ConvertFrom("#C7DFFC");

            InitializeLabels();

            UpdatingThread = new Thread(new ThreadStart(UpdateUI));
            UpdatingThread.IsBackground = true;
            UpdatingThread.Start();

            //Grid_MainWindow.Background = SolidColorBrush.
        }

        void UpdateUI()
        {
            while(true)
            {
                

                Dispatcher.BeginInvoke(new Action(delegate
                {
                    var time = "";
                    var backgroundBrush = (Brush)bc.ConvertFrom(DateTime.Now.ToString("#HHmmss"));

                    time = DateTime.Now.ToString("HH:mm:ss");

                    clocksLabel.Content = time;
                    clocksLabel.Background = backgroundBrush;

                    colorLabel.Content = DateTime.Now.ToString("#HHmmss");
                    colorLabel.Background = backgroundBrush;

                    Grid_MainWindow.Background = backgroundBrush;
                }));
                

                Thread.Sleep(1000);
            }            
        }




        void InitializeLabels()
        {
            clocksLabel.VerticalAlignment = VerticalAlignment.Center;
            clocksLabel.HorizontalAlignment = HorizontalAlignment.Center;

            clocksLabel.Margin = new Thickness(0, 0, 0, 0); //left, top, right, bottom

            clocksLabel.Background = new SolidColorBrush(Colors.Black);
            clocksLabel.Foreground = new SolidColorBrush(Colors.White);
            //clocksLabel.Content = "Test";
            //clocksLabel.FontFamily = new System.Windows.Media.FontFamily()
            clocksLabel.FontSize = fontSize;

            Grid_MainWindow.Children.Add(clocksLabel);





            colorLabel.VerticalAlignment = VerticalAlignment.Center;
            colorLabel.HorizontalAlignment = HorizontalAlignment.Center;

            colorLabel.Margin = new Thickness(0, Height / 2, 0, 0); //left, top, right, bottom

            colorLabel.Background = new SolidColorBrush(Colors.Black);
            colorLabel.Foreground = new SolidColorBrush(Colors.White);
            //colorLabel.Content = "Test";
            //colorLabel.FontFamily = new System.Windows.Media.FontFamily()
            colorLabel.FontSize = fontSize / 3.5;

            Grid_MainWindow.Children.Add(colorLabel);
        }



        static string GetDateTimeNowWithBase(int Base)
        {
            if (Base <= 2)
                return DateTime.Now.ToString("HH:mm:ss");

            var hours = DateTime.Now.Hour;
            var minutes = DateTime.Now.Minute;
            var seconds = DateTime.Now.Second;

            return String.Format("{0} : {1} : {2}",
                Convert.ToString(hours, Base).ToUpper().PadLeft(2, '0'),
                Convert.ToString(minutes, Base).ToUpper().PadLeft(2, '0'),
                Convert.ToString(seconds, Base).ToUpper().PadLeft(2, '0'));
        }




        private void MainWndw_MouseMove(object sender, MouseEventArgs e)
        {
            var timePassed = DateTime.Now - Process.GetCurrentProcess().StartTime;
            if (timePassed.TotalSeconds > 8)
                this.Close();
        }

        private void MainWndw_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        private void MainWndw_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var timePassed = DateTime.Now - Process.GetCurrentProcess().StartTime;
            if (timePassed.TotalSeconds > 5)
                this.Close();
        }

        private void MainWndw_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
