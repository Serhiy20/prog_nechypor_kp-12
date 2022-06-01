using System.Windows;

namespace TermPaper
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
        private void ClubsBtn_Click(object sender, RoutedEventArgs e)
        {
            ClubsWindow clubs = new ClubsWindow();
            Hide();
            clubs.Show();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GamesBtn_Click(object sender, RoutedEventArgs e)
        {
            GamesWindow games = new GamesWindow();
            Hide();
            games.Show();
        }

        private void ResultsBtn_Click(object sender, RoutedEventArgs e)
        {
            ResultsWindow res = new ResultsWindow();
            Hide();
            res.Show();
        }

        private void StadiumsBtn_Click(object sender, RoutedEventArgs e)
        {
            StadiumsWindow stadiums = new StadiumsWindow();
            Hide();
            stadiums.Show();
        }

        private void TicketsBtn_Click(object sender, RoutedEventArgs e)
        {
            TicketsWindow tickets = new TicketsWindow();
            Hide();
            tickets.Show();
        }
    }
}
