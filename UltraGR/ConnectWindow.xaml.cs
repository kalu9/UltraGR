using System;
using System.Collections.Generic;
using System.Windows;

namespace UltraGR
{
    /// <summary>
    /// Interaktionslogik für ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        MainWindow mainWindow;

        public ConnectWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            comboBox.Items.Clear();
            foreach (String port in GetAllPorts())
                comboBox.Items.Add(port);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (mainWindow.Port.IsOpen)
            {
                connectButton.Content = "Trennen";
                comboBox.IsEnabled = false;
            }
            else
            {
                connectButton.Content = "Verbinden";
                comboBox.IsEnabled = true;
            }
        }
        public List<String> GetAllPorts()
        {
            List<String> allPorts = new List<String>();
            foreach (String portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                allPorts.Add(portName);
            }
            return allPorts;
        }

        void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (!mainWindow.Port.IsOpen)
            {
                mainWindow.Port.Close();
                mainWindow.Port = new System.IO.Ports.SerialPort((String)comboBox.SelectedValue);
                mainWindow.Port.BaudRate = Int32.Parse(textBox.Text);
                Console.WriteLine(mainWindow.Port.BaudRate);
                mainWindow.Port.StopBits = System.IO.Ports.StopBits.One;
                mainWindow.Port.Open();

                this.Close();
            }
            else
                mainWindow.Port.Close();
        }
    }
}
