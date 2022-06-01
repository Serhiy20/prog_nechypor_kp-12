using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Imaging;

namespace TermPaper
{
    /// <summary>
    /// Логика взаимодействия для StadiumsWindow.xaml
    /// </summary>
    public partial class StadiumsWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY;Initial Catalog=Football;Integrated Security=True");
        SqlDataAdapter Data;
        DataTable dT = new DataTable();
        public StadiumsWindow()
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
                Data = new SqlDataAdapter("SELECT * FROM Stadiums", sqlConn);
                dT = new DataTable("S");
                Data.Fill(dT);
                Items = new String[dT.Rows.Count + 1];
                Items[0] = "Оберіть стадіон";
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
            string stadium = CB.SelectedValue.ToString();
            int a = CB.SelectedIndex;
            try
            {
                Image.Source = new BitmapImage(new Uri($"D:/Stadiums/{a}.jpg"));

                Data = new SqlDataAdapter($"SELECT StadiumName, City, Capacity FROM Stadiums WHERE StadiumName= '{stadium}' ;", sqlConn);
                dT = new DataTable("S");
                Data.Fill(dT);
                Label1.Content = dT.Rows[0][0].ToString();
                Label2.Content = dT.Rows[0][1].ToString();
                Label3.Content = dT.Rows[0][2].ToString();

                Data = new SqlDataAdapter($"SELECT ClubName FROM Clubs WHERE IDClub= '{a}' ;", sqlConn);
                dT = new DataTable("S");
                Data.Fill(dT);
                Label4.Content = dT.Rows[0][0].ToString();

                Data = new SqlDataAdapter($"SELECT ClubName as [Клуб], Dist as [Відстань] " +
                    $"FROM Clubs Cl INNER JOIN Distance Ds ON Cl.IDClub = Ds.IDClub " +
                    $"WHERE IDStadium= '{a}' ;", sqlConn);
                dT = new DataTable("C");
                Data.Fill(dT);
                dataGrid1.ItemsSource = dT.DefaultView;
            }
            catch
            {
                MessageBox.Show("Оберіть стадіон!");
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
