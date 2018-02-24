using System.Windows;
using System.Threading;
using System;

namespace UltraGR
{
    public partial class VisuWindow : Window
    {
        MainWindow mainWindow;
        Thickness margin = new Thickness();
        Thread thread;
        double windowHeight = 0, windowWidth = 0;
        double ellipseHeight = 0, ellipseWidth = 0;
        String dataText = "";

        public VisuWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            mainWindow.Port.Write("h");
            Thread.Sleep(5);
            mainWindow.Port.Write("a");
            Thread.Sleep(5);
            mainWindow.Port.Write("g");
            thread = new Thread(() =>
            {
                string bufferString = "";
                while (true)
                {
                    try
                    {
                        char value = (char)mainWindow.Port.ReadChar();
                        if (value == ',')
                        {
                            string[] split = bufferString.Split('|');
                            int height = 0;
                            int width = 0;
                            int.TryParse(split[1], out height);
                            int.TryParse(split[0], out width);
                            dataText = "Winkel: " + width + " | Distanz: " + height;
                            Console.WriteLine(width + " | " + height);
                            height = (int)(height * (-(windowHeight - ellipseHeight) / 390) + ((windowHeight - ellipseHeight) * 650) / 390);
                            width = (int)((width * (windowWidth - ellipseWidth) / 64) + ((windowWidth - ellipseWidth) / 2));
                            
                            height = (height < 0) ? 0 : (height > windowHeight) ? (int)(windowHeight - ellipseHeight) : height;
                            width = (width < 0) ? 0 : (width > windowWidth) ? (int)(windowWidth - ellipseWidth) : width;
                            
                            margin = new Thickness(width, height, 0, 0);
                            bufferString = "";
                        }
                        else
                        {
                            bufferString += value;
                        }
                    }
                    catch(ThreadAbortException e){ }
                    catch(IndexOutOfRangeException e) { }
                }
            });
            thread.Start();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 16);
            dispatcherTimer.Start();
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ellipse.Margin = margin;
            labelData.Content = dataText;
        }

        void button_exit(object sender, RoutedEventArgs e)
        {
            Close();
            thread.Abort();
        }
        void button_test(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button was clicked.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ellipseWidth = ellipse.ActualWidth;
            ellipseHeight = ellipse.ActualHeight;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            windowWidth = ActualWidth;
            windowHeight = ActualHeight;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            thread.Abort();
        }
    }
}
