using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Imaging;

namespace TermPaper
{
    /// <summary>
    /// Логика взаимодействия для ClubsWindow.xaml
    /// </summary>
    public partial class ClubsWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY;Initial Catalog=Football;Integrated Security=True");
        SqlDataAdapter Data;
        DataTable dT = new DataTable();
        public ClubsWindow()
        {
            InitializeComponent();
            CB.ItemsSource = GetItems();
            CB.SelectedIndex = 0;
        }
        private String[] GetItems()
        {
            sqlConn.Open();
            String[] Items = { "" };
            if (sqlConn.State == ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT * FROM Clubs", sqlConn);
                dT = new DataTable("C");
                Data.Fill(dT);
                Items = new String[dT.Rows.Count + 1];
                Items[0] = "Оберіть клуб";
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    Items[i + 1] = dT.Rows[i][1].ToString();
                }
            }
            sqlConn.Close();
            return Items;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            string club = CB.SelectedValue.ToString();
            int a = CB.SelectedIndex;
            try
            {
                Image.Source = new BitmapImage(new Uri($"D:/Icons/{a}.png"));

                Data = new SqlDataAdapter($"SELECT ClubName, HomeCity, Coach FROM Clubs WHERE ClubName= '{club}' ;", sqlConn);
                dT = new DataTable("C");
                Data.Fill(dT);
                Label1.Content = dT.Rows[0][0].ToString();
                Label2.Content = dT.Rows[0][1].ToString();
                Label3.Content = dT.Rows[0][2].ToString();

                Data = new SqlDataAdapter($"SELECT StadiumName FROM Stadiums WHERE IDStadium= '{a}' ;", sqlConn);
                dT = new DataTable("C");
                Data.Fill(dT);
                Label4.Content = dT.Rows[0][0].ToString();

                Data = new SqlDataAdapter($"SELECT StadiumName as [Стадіон], Dist as [Відстань] " +
                    $"FROM Stadiums St INNER JOIN Distance Ds ON St.IDStadium = Ds.IDStadium " +
                    $"WHERE IDClub= '{a}' ;", sqlConn);
                dT = new DataTable("S");
                Data.Fill(dT);
                dataGrid1.ItemsSource = dT.DefaultView;

                Data = new SqlDataAdapter($"SELECT PlayerName as [Ім'я], PlayerSurname as [Прізвище], Age as [Вік], Position as [Позиція] " +
                    $"FROM Players Pl INNER JOIN PlayerInClub Pc ON Pl.IDPlayer = Pc.IDPlayer " +
                    $"WHERE IDClub= '{a}' ;", sqlConn);
                dT = new DataTable("P");
                Data.Fill(dT);
                dataGrid2.ItemsSource = dT.DefaultView;
            }
            catch
            {
                MessageBox.Show("Оберіть клуб!");
            }
            sqlConn.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
