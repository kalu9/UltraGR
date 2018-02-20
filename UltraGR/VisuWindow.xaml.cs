using System.Windows;
using System.Collections.Generic;
using System;

namespace UltraGR
{
    public partial class VisuWindow : Window
    {
        MainWindow mainWindow;
        Thickness margin = new Thickness();

        public VisuWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            new System.Threading.Thread(() =>
            {
                string bufferString = "";
                while (true)
                {
                    char value = (char)mainWindow.Port.ReadChar();
                    if (value == ',')
                    {
                        string[] split = bufferString.Split('|');
                        try
                        {
                            int height = 0;
                            int.TryParse((split[0]), out height);
                            margin = new Thickness(0, height / 2, 0, 0);
                            Console.WriteLine(split[0] + " " + split[1]);
                        }
                        catch (IndexOutOfRangeException e) { }

                        bufferString = "";
                    }
                    else
                    {
                        bufferString += value;
                    }
                }
            }).Start();
        }

        void button_exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Closing... Visu...Waiting..");
        }
        void button_test(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button was clicked.");
        }
    }
}
