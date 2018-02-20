using System.Windows;

namespace UltraGR
{
    /// <summary>
    /// Interaktionslogik für ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        public ConnectWindow()
        {
            InitializeComponent();
            textBox.Text = "Test123";   
        }

        void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void button_test(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button was clicked.");
        }
    }
}
