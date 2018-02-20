using System;
using System.IO.Ports;
using System.Windows;

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

        void button_sponsoren(object sender, RoutedEventArgs e)
        {
        }

        void button_connect(object sender, RoutedEventArgs e)
        {
            ConnectWindow cwin = new ConnectWindow(this);

            cwin.Show();
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
