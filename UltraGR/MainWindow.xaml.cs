using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace UltraGR
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serial = new SerialPort();
        char[] buffer = new char[1];

        public MainWindow()
        {
            InitializeComponent();

            serial = new SerialPort("COM3");
            serial.BaudRate = 921600;
            serial.StopBits = StopBits.One;
            serial.Open();
            serial.Write("h");
            System.Threading.Thread.Sleep(50);
            serial.Write("a");
            System.Threading.Thread.Sleep(50);
            serial.Write("g");
            System.Threading.Thread.Sleep(50);
            new System.Threading.Thread(() =>
            {
                while (true)
                {
                    //serial.Read(buffer, 0, buffer.Count());
                    Console.Write((char)serial.ReadByte());
                    //Console.WriteLine("\nHELLO JEEFIPDHSLDKJHSRELKFHSRLKFHSERGSRGF-----------------------####################");
                    //Console.Write(buffer);
                }
            }).Start();
        }

        void button_visu(object sender, RoutedEventArgs e)
        {
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
            ConnectWindow cwin = new ConnectWindow();

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
