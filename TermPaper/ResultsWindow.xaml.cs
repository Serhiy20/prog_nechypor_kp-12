using System;
using System.ComponentModel;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace TermPaper
{
    /// <summary>
    /// Логика взаимодействия для ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY;Initial Catalog=Football;Integrated Security=True");
        SqlDataAdapter Data;
        DataTable dT = new DataTable();
        public ResultsWindow()
        {
            InitializeComponent();
            DataGrid1Fill();
            DataGrid2Fill();
        }
        private void DataGrid1Fill()
        {
            int[] scored = new int[20];
            int[] concerned = new int[20];
            int[] points = new int[20];
            int[] games = new int[20];
            int[] wins = new int[20];
            int[] draws = new int[20];
            int[] loses = new int[20];

            Data = new SqlDataAdapter("SELECT IDClub1, Club1Goals, Club2Goals, IDClub2 FROM Games ;", sqlConn);
            dT = new DataTable("G1");
            Data.Fill(dT);
            for (int i = 0; i < 356; i++)
            {
                int team1 = Convert.ToInt32(dT.Rows[i][0]);
                int goals1 = Convert.ToInt32(dT.Rows[i][1]);
                int goals2 = Convert.ToInt32(dT.Rows[i][2]);
                int team2 = Convert.ToInt32(dT.Rows[i][3]);
                games[team1 - 1] += 1;
                games[team2 - 1] += 1;
                scored[team1 - 1] += goals1;
                scored[team2 - 1] += goals2;
                concerned[team1 - 1] += goals2;
                concerned[team2 - 1] += goals1;
                if (goals1 > goals2)
                {
                    points[team1 - 1] += 3;
                    wins[team1 - 1] += 1;
                    loses[team2 - 1] += 1;
                }
                else if (goals2 > goals1)
                {
                    points[team2 - 1] += 3;
                    wins[team2 - 1] += 1;
                    loses[team1 - 1] += 1;
                }
                else
                {
                    points[team1 - 1] += 1;
                    points[team2 - 1] += 1;
                    draws[team1 - 1] += 1;
                    draws[team2 - 1] += 1;
                }
            }
            int[] difference = new int[20];
            for (int i = 0; i < 20; i++)
            {
                difference[i] = scored[i] - concerned[i];
            }

            string[] clubs = new string[20];
            Data = new SqlDataAdapter("SELECT ClubName FROM Clubs ;", sqlConn);
            dT = new DataTable("G2");
            Data.Fill(dT);
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                clubs[i] = dT.Rows[i][0].ToString();
            }
            DataTable dT1 = new DataTable("G3");
            dT1.Columns.Add("Клуб", typeof(string));
            dT1.Columns.Add("І", typeof(Int32));
            dT1.Columns.Add("В", typeof(Int32));
            dT1.Columns.Add("Н", typeof(Int32));
            dT1.Columns.Add("П", typeof(Int32));
            dT1.Columns.Add("МЗ", typeof(Int32));
            dT1.Columns.Add("МП", typeof(Int32));
            dT1.Columns.Add("РМ", typeof(Int32));
            dT1.Columns.Add("О", typeof(Int32));
            for (int i = 0; i < 20; i++)
            {
                dT1.Rows.Add(clubs[i], games[i], wins[i], draws[i], loses[i], scored[i], concerned[i], difference[i], points[i]);
            }
            dataGrid1.ItemsSource = dT1.DefaultView;
            dataGrid1.Items.SortDescriptions.Add(new SortDescription("О", ListSortDirection.Descending));
            dataGrid1.Items.SortDescriptions.Add(new SortDescription("РМ", ListSortDirection.Descending));
        }
        private void DataGrid2Fill()
        {
            int[] goals = new int[340];
            Data = new SqlDataAdapter("SELECT C1G1Player, C1G2Player, C1G3Player, C2G1Player, C2G2Player, C2G3Player FROM Games ;", sqlConn);
            dT = new DataTable("B1");
            Data.Fill(dT);
            for (int i = 0; i < 340; i++)
                for (int j = 0; j < 6; j++)
                {
                    if (dT.Rows[i][j].ToString() != "")
                    {
                        goals[Convert.ToInt32(dT.Rows[i][j]) - 1]++;
                    }
                }
            string[] playername = new string[340];
            string[] playersurname = new string[340];
            Data = new SqlDataAdapter("SELECT PlayerName, PlayerSurname FROM Players ;", sqlConn);
            dT = new DataTable("G2");
            Data.Fill(dT);
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                playername[i] = dT.Rows[i][0].ToString();
                playersurname[i] = dT.Rows[i][1].ToString();
            }
            DataTable dT2 = new DataTable("B2");
            dT2.Columns.Add("Ім'я гравця", typeof(string));
            dT2.Columns.Add("Прізвище гравця", typeof(string));
            dT2.Columns.Add("Кількість голів", typeof(Int32));
            for (int i = 0; i < 340; i++)
            {
                if (goals[i] != 0)
                {
                    dT2.Rows.Add(playername[i], playersurname[i], goals[i]);
                }
            }
            dataGrid2.ItemsSource = dT2.DefaultView;
            dataGrid2.Items.SortDescriptions.Add(new SortDescription("Кількість голів", ListSortDirection.Descending));
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
