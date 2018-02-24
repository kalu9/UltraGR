using System.Windows;
using System.Collections.Generic;
using System;

namespace UltraGR
{
    public partial class CalibrationWindow : Window
    {
        MainWindow mainWindow;

        public CalibrationWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            new System.Threading.Thread(() =>
            {
                
            }).Start();
        }

        void button_exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Closing...");
        }
        void button_test(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button was clicked.");
        }
    }
}
