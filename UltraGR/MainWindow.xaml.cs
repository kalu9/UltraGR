using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Media;

namespace UltraGR
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort port = new SerialPort();
        char[] buffer = new char[1];

        public SerialPort Port
        {
            get { return port; }
            set { port = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(port.IsOpen)
            {
                label_connection_status.Content = "Verbunden";
                label_connection_status.Foreground = (Brush)(new BrushConverter()).ConvertFromString("Green");
            }
            else
            {
                label_connection_status.Content = "Getrennt";
                label_connection_status.Foreground = (Brush)(new BrushConverter()).ConvertFromString("Red");
            }
        }

        void button_visu(object sender, RoutedEventArgs e)
        {
            VisuWindow vWin = new VisuWindow(this);
            vWin.Show();
        }

        void button_exit(object sender, RoutedEventArgs e)
        {
            suicide();
        }

        void button_distance_click(object sender, RoutedEventArgs e)
        {
            //Port.Write("d");
            MessageBox.Show("Test");
        }

        void button_connect(object sender, RoutedEventArgs e)
        {
            ConnectWindow cwin = new ConnectWindow(this);

            cwin.Show();
        }


        void button_signalexport_click(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread exportThread = new System.Threading.Thread(() =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV|*.csv";
                saveFileDialog.Title = "Signal als CSV exportieren";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    System.IO.FileStream fileStream = (System.IO.FileStream)saveFileDialog.OpenFile();
                    port.Write("p");
                    System.Threading.Thread.Sleep(5);
                    while(true)
                    {
                        try
                        {
                            char value = (char)port.ReadChar();
                            Console.Write(value);
                        }
                        catch (System.Threading.ThreadAbortException exception) { }
                    }
                }
            });
            exportThread.Start();
        }

        void button_calibration_click(object sender, RoutedEventArgs e)
        {
            CalibrationWindow cawin = new CalibrationWindow(this);

            cawin.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            suicide();
        }

        private void suicide()
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            p.Kill();
        }
    }
}
