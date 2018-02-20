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
            foreach (String port in GetAllPorts())
                comboBox.Items.Add(port);
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
            mainWindow.Port.Close();
            mainWindow.Port = new System.IO.Ports.SerialPort((String)comboBox.SelectedValue);
            mainWindow.Port.BaudRate = 921600;
            mainWindow.Port.StopBits = System.IO.Ports.StopBits.One;
            mainWindow.Port.Open();
            mainWindow.Port.Write("h");
            mainWindow.Port.Write("a");
            mainWindow.Port.Write("g");
            this.Close();
        }
    }
}
