using System.Windows;

namespace Prac3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AdminMode_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            Hide();
            admin.Show();
        }
        private void UserMode_Click(object sender, RoutedEventArgs e)
        {
            UserWindow user = new UserWindow();
            Hide();
            user.Show();
        }
        private void AboutDev_Click(object sender, RoutedEventArgs e)
        {
            DevWindow devWindow = new DevWindow();
            Hide();
            devWindow.Show();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
