using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace TermPaper
{
    /// <summary>
    /// Логика взаимодействия для GamesWindow.xaml
    /// </summary>
    public partial class GamesWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY;Initial Catalog=Football;Integrated Security=True");
        SqlDataAdapter Data;
        DataTable dT = new DataTable();
        public GamesWindow()
        {
            InitializeComponent();
            CBClub.ItemsSource = GetItemsClub();
            CBClub.SelectedIndex = 0;
            CBStadium.ItemsSource = GetItemsStadium();
            CBStadium.SelectedIndex = 0;
            CBDate.ItemsSource = GetItemsDate();
            CBDate.SelectedIndex = 0;
        }
        private String[] GetItemsClub()
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
        private String[] GetItemsStadium()
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
        private String[] GetItemsDate()
        {
            sqlConn.Open();
            int j = 0;
            DateTime[] Items1 = { };
            String[] Items2 = { "" };
            if (sqlConn.State == ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT * FROM Games", sqlConn);
                dT = new DataTable("D");
                Data.Fill(dT);
                Items1 = new DateTime[41];
                Items2 = new String[41];
                Items2[0] = "Оберіть дату";
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    if (Convert.ToDateTime(dT.Rows[i][6]) != Items1[j])
                    {
                        Items1[j + 1] = Convert.ToDateTime(dT.Rows[i][6].ToString());
                        Items2[j + 1] = Items1[j + 1].ToString("MM/dd/yyyy");
                        j++;
                    }
                }
            }
            sqlConn.Close();
            return Items2;
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            string club = CBClub.SelectedValue.ToString();
            if (club == "Оберіть клуб")
            {
                MessageBox.Show("Ви не обрали клуб!");
            }
            else
            {
                Data = new SqlDataAdapter("SELECT C1.ClubName, C2.ClubName, GameDate FROM Games Gm " +
                    "INNER JOIN Clubs C1 ON Gm.IDClub1 = C1.IDClub " +
                    "INNER JOIN Clubs C2 ON Gm.IDClub2 = C2.IDClub " +
                    $"WHERE C1.ClubName = '{club}' OR C2.ClubName = '{club}' ;", sqlConn);
                DataTable dT1 = new DataTable("G4");
                Data.Fill(dT1);
                Data = new SqlDataAdapter("SELECT C1.ClubName as [Перший клуб], Club1Goals as [Голи першого клубу], Club2Goals as [Голи другого клубу], " +
                    "C2.ClubName as [Другий клуб], st.StadiumName as [Стадіон], " +
                    "p1.PlayerSurname as [1-ий гол першого клубу], p2.PlayerSurname as [2-ий гол першого клубу], p3.PlayerSurname as [3-ій гол першого клубу], " +
                    "p4.PlayerSurname as [1-ий гол другого клубу], p5.PlayerSurname as [2-ий гол другого клубу], p6.PlayerSurname as [3-ій гол другого клубу] " +
                    "FROM Games Gm INNER JOIN Clubs C1 ON Gm.IDClub1 = C1.IDClub " +
                    "INNER JOIN Clubs C2 ON Gm.IDClub2 = C2.IDClub " +
                    "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                    "LEFT JOIN Players p1 on Gm.C1G1Player = p1.IDPlayer " +
                    "LEFT JOIN Players p2 on Gm.C1G2Player = p2.IDPlayer " +
                    "LEFT JOIN Players p3 on Gm.C1G3Player = p3.IDPlayer " +
                    "LEFT JOIN Players p4 on Gm.C2G1Player = p4.IDPlayer " +
                    "LEFT JOIN Players p5 on Gm.C2G2Player = p5.IDPlayer " +
                    "LEFT JOIN Players p6 on Gm.C2G3Player = p6.IDPlayer " +
                    $"WHERE C1.ClubName = '{club}' OR C2.ClubName = '{club}' ;", sqlConn);
                dT = new DataTable("G1");
                Data.Fill(dT);
                dT.Columns.Add("Дата");
                string[] dates1 = new string[dT.Rows.Count];
                DateTime[] dates2 = new DateTime[dT.Rows.Count];
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    dates2[i] = Convert.ToDateTime(dT1.Rows[i][2]);
                    dates1[i] = dates2[i].ToString("MM/dd/yyyy");
                    dT.Rows[i][11] = dates1[i];
                }
                dataGrid.ItemsSource = dT.DefaultView;
            }
            sqlConn.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            string stadium = CBStadium.SelectedValue.ToString();
            if (stadium == "Оберіть стадіон")
            {
                MessageBox.Show("Ви не обрали стадіон!");
            }
            else
            {
                Data = new SqlDataAdapter("SELECT st.StadiumName, GameDate FROM Games Gm " +
                    "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                    $"WHERE st.StadiumName = '{stadium}' ;", sqlConn);
                DataTable dT1 = new DataTable("G4");
                Data.Fill(dT1);
                Data = new SqlDataAdapter("SELECT C1.ClubName as [Перший клуб], Club1Goals as [Голи першого клубу], Club2Goals as [Голи другого клубу], " +
                    "C2.ClubName as [Другий клуб], st.StadiumName as [Стадіон], " +
                    "p1.PlayerSurname as [1-ий гол першого клубу], p2.PlayerSurname as [2-ий гол першого клубу], p3.PlayerSurname as [3-ій гол першого клубу], " +
                    "p4.PlayerSurname as [1-ий гол другого клубу], p5.PlayerSurname as [2-ий гол другого клубу], p6.PlayerSurname as [3-ій гол другого клубу] " +
                    "FROM Games Gm INNER JOIN Clubs C1 ON Gm.IDClub1 = C1.IDClub " +
                    "INNER JOIN Clubs C2 ON Gm.IDClub2 = C2.IDClub " +
                    "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                    "LEFT JOIN Players p1 on Gm.C1G1Player = p1.IDPlayer " +
                    "LEFT JOIN Players p2 on Gm.C1G2Player = p2.IDPlayer " +
                    "LEFT JOIN Players p3 on Gm.C1G3Player = p3.IDPlayer " +
                    "LEFT JOIN Players p4 on Gm.C2G1Player = p4.IDPlayer " +
                    "LEFT JOIN Players p5 on Gm.C2G2Player = p5.IDPlayer " +
                    "LEFT JOIN Players p6 on Gm.C2G3Player = p6.IDPlayer " +
                    $"WHERE st.StadiumName = '{stadium}' ;", sqlConn);
                dT = new DataTable("G2");
                Data.Fill(dT);
                dT.Columns.Add("Дата");
                string[] dates1 = new string[dT.Rows.Count];
                DateTime[] dates2 = new DateTime[dT.Rows.Count];
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    dates2[i] = Convert.ToDateTime(dT1.Rows[i][1]);
                    dates1[i] = dates2[i].ToString("MM/dd/yyyy");
                    dT.Rows[i][11] = dates1[i];
                }
                dataGrid.ItemsSource = dT.DefaultView;
            }
            sqlConn.Close();
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            string date = CBDate.SelectedValue.ToString();
            try
            {
                Data = new SqlDataAdapter("SELECT C1.ClubName as [Перший клуб], Club1Goals as [Голи першого клубу], Club2Goals as [Голи другого клубу], " +
                    "C2.ClubName as [Другий клуб], st.StadiumName as [Стадіон], " +
                    "p1.PlayerSurname as [1-ий гол першого клубу], p2.PlayerSurname as [2-ий гол першого клубу], p3.PlayerSurname as [3-ій гол першого клубу], " +
                    "p4.PlayerSurname as [1-ий гол другого клубу], p5.PlayerSurname as [2-ий гол другого клубу], p6.PlayerSurname as [3-ій гол другого клубу] " +
                    "FROM Games Gm INNER JOIN Clubs C1 ON Gm.IDClub1 = C1.IDClub " +
                    "INNER JOIN Clubs C2 ON Gm.IDClub2 = C2.IDClub " +
                    "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                    "LEFT JOIN Players p1 on Gm.C1G1Player = p1.IDPlayer " +
                    "LEFT JOIN Players p2 on Gm.C1G2Player = p2.IDPlayer " +
                    "LEFT JOIN Players p3 on Gm.C1G3Player = p3.IDPlayer " +
                    "LEFT JOIN Players p4 on Gm.C2G1Player = p4.IDPlayer " +
                    "LEFT JOIN Players p5 on Gm.C2G2Player = p5.IDPlayer " +
                    "LEFT JOIN Players p6 on Gm.C2G3Player = p6.IDPlayer " +
                    $"WHERE GameDate = '{date}' ;", sqlConn);
                dT = new DataTable("G3");
                Data.Fill(dT);
                dT.Columns.Add("Дата");
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    dT.Rows[i][11] = date;
                }
                dataGrid.ItemsSource = dT.DefaultView;
            }
            catch
            {
                MessageBox.Show("Ви не обрали дату!");
            }
            sqlConn.Close();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            Data = new SqlDataAdapter("SELECT GameDate FROM Games ;", sqlConn);
            DataTable dT1 = new DataTable("G4");
            Data.Fill(dT1);
            Data = new SqlDataAdapter("SELECT C1.ClubName as [Перший клуб], Club1Goals as [Голи першого клубу], Club2Goals as [Голи другого клубу], " +
                "C2.ClubName as [Другий клуб], st.StadiumName as [Стадіон], " +
                "p1.PlayerSurname as [1-ий гол першого клубу], p2.PlayerSurname as [2-ий гол першого клубу], p3.PlayerSurname as [3-ій гол першого клубу], " +
                "p4.PlayerSurname as [1-ий гол другого клубу], p5.PlayerSurname as [2-ий гол другого клубу], p6.PlayerSurname as [3-ій гол другого клубу] " +
                "FROM Games Gm INNER JOIN Clubs C1 ON Gm.IDClub1 = C1.IDClub " +
                "INNER JOIN Clubs C2 ON Gm.IDClub2 = C2.IDClub " +
                "INNER JOIN Stadiums st ON Gm.IDStadium = st.IDStadium " +
                "LEFT JOIN Players p1 on Gm.C1G1Player = p1.IDPlayer " +
                "LEFT JOIN Players p2 on Gm.C1G2Player = p2.IDPlayer " +
                "LEFT JOIN Players p3 on Gm.C1G3Player = p3.IDPlayer " +
                "LEFT JOIN Players p4 on Gm.C2G1Player = p4.IDPlayer " +
                "LEFT JOIN Players p5 on Gm.C2G2Player = p5.IDPlayer " +
                "LEFT JOIN Players p6 on Gm.C2G3Player = p6.IDPlayer ;", sqlConn);
            dT = new DataTable("G5");
            Data.Fill(dT);
            dT.Columns.Add("Дата");
            string[] dates1 = new string[dT.Rows.Count];
            DateTime[] dates2 = new DateTime[dT.Rows.Count];
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                dates2[i] = Convert.ToDateTime(dT1.Rows[i][0]);
                dates1[i] = dates2[i].ToString("MM/dd/yyyy");
                dT.Rows[i][11] = dates1[i];
            }
            dataGrid.ItemsSource = dT.DefaultView;
            sqlConn.Close();
        }
    }
}
