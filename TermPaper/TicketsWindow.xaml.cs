using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Imaging;

namespace TermPaper
{
    /// <summary>
    /// Логика взаимодействия для TicketsWindow.xaml
    /// </summary>
    public partial class TicketsWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY;Initial Catalog=Football;Integrated Security=True");
        SqlDataAdapter Data;
        SqlCommand Com;
        DataTable dT = new DataTable();
        public TicketsWindow()
        {
            InitializeComponent();
            DataGridFill();
            CB.ItemsSource = GetItems();
            CB.SelectedIndex = 0;
        }
        private void DataGridFill()
        {
            Data = new SqlDataAdapter("SELECT GameDate FROM Games WHERE IDGame > '356' ;", sqlConn);
            DataTable dT1 = new DataTable("G4");
            Data.Fill(dT1);
            Data = new SqlDataAdapter("SELECT IDGame as [Гра №], C1.ClubName as [Перший клуб], " +
                "C2.ClubName as [Другий клуб], st.StadiumName as [Стадіон] " +
                "FROM Games Gm " +
                "INNER JOIN Clubs C1 ON Gm.IDClub1 = C1.IDClub " +
                "INNER JOIN Clubs C2 ON Gm.IDClub2 = C2.IDClub " +
                "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                "WHERE Gm.IDGame > '356' ;", sqlConn);
            dT = new DataTable("T");
            Data.Fill(dT);
            dT.Columns.Add("Дата");
            string[] dates1 = new string[dT.Rows.Count];
            DateTime[] dates2 = new DateTime[dT.Rows.Count];
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                dates2[i] = Convert.ToDateTime(dT1.Rows[i][0]);
                dates1[i] = dates2[i].ToString("MM/dd/yyyy");
                dT.Rows[i][4] = dates1[i];
            }
            dataGrid1.ItemsSource = dT.DefaultView;
        }
        private String[] GetItems()
        {
            String[] Items = { "" };
            Data = new SqlDataAdapter("SELECT IDGame FROM Games WHERE IDGame > '356' ;", sqlConn);
            dT = new DataTable("S");
            Data.Fill(dT);
            Items = new String[dT.Rows.Count + 1];
            Items[0] = "Оберіть № гри";
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                Items[i + 1] = dT.Rows[i][0].ToString();
            }
            return Items;
        }
        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB.SelectedValue.ToString() != "Оберіть № гри")
            {
                string id = CB.SelectedValue.ToString();
                Data = new SqlDataAdapter("SELECT st.StadiumName, st.Capacity FROM Games Gm " +
                    "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                    $"WHERE Gm.IDGame = '{id}' ;", sqlConn);
                dT = new DataTable();
                Data.Fill(dT);
                label_capacity.Content = $"{dT.Rows[0][0]}, {dT.Rows[0][1]}";

                Data = new SqlDataAdapter("SELECT IDTicket as [ID квитка], PlaceNumber as [Місце] FROM Tickets " +
                    $"WHERE IDGame = '{id}' ;", sqlConn);
                DataTable dT1 = new DataTable();
                Data.Fill(dT1);
                dataGrid2.ItemsSource = dT1.DefaultView;
            }
            else
            {
                label_capacity.Content = "Гру не обрано";
                DataTable dT1 = new DataTable();
                dataGrid2.ItemsSource = dT1.DefaultView;
            }
        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            string game = CB.SelectedValue.ToString();
            string place = TB.Text;
            string ticket = $"{game}-{place}";
            sqlConn.Open();
            if (place != "" && game != "Оберіть № гри")
            {
                Data = new SqlDataAdapter("SELECT st.Capacity FROM Games Gm " +
                    "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                    $"WHERE Gm.IDGame = '{game}' ;", sqlConn);
                dT = new DataTable();
                Data.Fill(dT);
                if (Convert.ToInt32(TB.Text) > Convert.ToInt32(dT.Rows[0][0]))
                {
                    MessageBox.Show("На стадіоні немає такого місця");
                    TB.Text = "";
                }
                else
                {
                    string strQ;
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        try
                        {
                            strQ = $"INSERT INTO Tickets (IDTicket, IDGame, PlaceNumber) values('{ticket}', '{game}', '{place}'); ";
                            Com = new SqlCommand(strQ, sqlConn);
                            Com.ExecuteNonQuery();
                            MessageBox.Show("Квиток успішно куплено!");
                            
                            Data = new SqlDataAdapter("SELECT C1.IDClub, C1.ClubName, C2.ClubName, C2.IDClub, tk.IDTicket, tk.PlaceNumber, GameDate " +
                                "FROM Games Gm " +
                                "INNER JOIN Clubs C1 ON C1.IDClub = Gm.IDClub1 " +
                                "INNER JOIN Clubs C2 ON C2.IDClub = Gm.IDClub2 " +
                                "INNER JOIN Tickets tk ON tk.IDGame = Gm.IDGame " +
                                $"WHERE IDTicket = '{ticket}';", sqlConn);
                            dT = new DataTable();
                            Data.Fill(dT);
                            image_club1.Source = new BitmapImage(new Uri($"D:/Icons/{dT.Rows[0][0]}.png"));
                            image_club2.Source = new BitmapImage(new Uri($"D:/Icons/{dT.Rows[0][3]}.png"));
                            label_club1.Content = dT.Rows[0][1];
                            label_club2.Content = dT.Rows[0][2];
                            label_id.Content = dT.Rows[0][4];
                            label_place.Content = dT.Rows[0][5];
                            label_date.Content = dT.Rows[0][6];
                            CB.SelectedIndex = 0;
                            TB.Text = "";
                        }
                        catch
                        {
                            MessageBox.Show("Цей квиток уже куплений, оберіть інший");
                            CB.SelectedIndex = 0;
                            TB.Text = "";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Оберіть потрібну гру та введіть місце щоб придбати квиток");
                CB.SelectedIndex = 0;
                TB.Text = "";
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

