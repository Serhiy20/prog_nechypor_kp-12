using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;

namespace Prac3
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY;Initial Catalog=Prac3;Integrated Security=True");
        SqlCommand Com;
        SqlDataAdapter Data;
        DataTable dT = new DataTable();
        static int LenTable;
        public AdminWindow()
        {
            InitializeComponent();
            change_pass.Text = ""; change_pass.IsEnabled = false;
            change_newpass.Text = ""; change_newpass.IsEnabled = false;
            change_newpass2.Text = ""; change_newpass2.IsEnabled = false;
            adduser_login.Text = ""; adduser_login.IsEnabled = false;
            dataGrid.IsEnabled = false; Users_logins.IsEnabled = false;
            ChangeActive.IsEnabled = false; ChangeRestriction.IsEnabled = false;
            Prev.IsEnabled = false; Next.IsEnabled = false; Exit.IsEnabled = false;
            Update.IsEnabled = false;
            Add_user.IsEnabled = false;
            CorrectStatus.IsEnabled = false;
            CorrectRestriction.IsEnabled = false;
            UpdateDataTable();
            Users_logins.ItemsSource = GetItems();
            Users_logins.SelectedIndex = 0;
            index = -1;
        }
        private void UpdateDataTable()
        {
            sqlConn.Open();
            if (sqlConn.State == ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                dataGrid.ItemsSource = dT.DefaultView;
                LenTable = dT.Rows.Count;
            }
            sqlConn.Close();
        }
        private String[] GetItems()
        {
            String[] Items = { "" };
            sqlConn.Open();
            if (sqlConn.State == ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT * FROM MainTable", sqlConn);
                dT = new DataTable("T");
                Data.Fill(dT);
                Items = new String[dT.Rows.Count];
                for (int i = 0; i < dT.Rows.Count; i++)
                {
                    Items[i] = dT.Rows[i][2].ToString();
                }
            }
            sqlConn.Close();
            return Items;
        }
        string pswadm;
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            String strQ;
            String realpass = change_pass.Text.ToString();
            String newpass = change_newpass.Text.ToString();
            String newpass2 = change_newpass2.Text.ToString();
            if ((realpass == pswadm) && (newpass != "") && (newpass == newpass2))
            {
                sqlConn.Open();
                if (sqlConn.State == ConnectionState.Open)
                {
                    strQ = "UPDATE MainTable SET Password ='" + newpass + "' WHERE Login = 'admin'; ";
                    Com = new SqlCommand(strQ, sqlConn);
                    Com.ExecuteNonQuery();
                    MessageBox.Show("Пароль змінено");
                    change_newpass2.Text = "";
                    change_newpass.Text = "";
                    change_pass.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Пароль не введено або повторно введений пароль неправильний!");
            }
            sqlConn.Close();
        }
        int buf = 1;
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            if (index > 0 && dT.Rows.Count > 0)
            {
                index--;
                selected_name.Content = dT.Rows[index][0].ToString();
                selected_surname.Content = dT.Rows[index][1].ToString();
                selected_login.Content = dT.Rows[index][2].ToString();
                selected_status.Content = dT.Rows[index][3].ToString();
                selected_restriction.Content = dT.Rows[index][4].ToString();
            }
            sqlConn.Close();
        }
        int index = -1;
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            Data = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", sqlConn);
            dT = new DataTable("Користувачі системи");
            Data.Fill(dT);
            dataGrid.ItemsSource = dT.DefaultView;
            LenTable = dT.Rows.Count;
            if (index < LenTable - 1)
            {
                index++;
                selected_name.Content = dT.Rows[index][0].ToString();
                selected_surname.Content = dT.Rows[index][1].ToString();
                selected_login.Content = dT.Rows[index][2].ToString();
                selected_status.Content = dT.Rows[index][3].ToString();
                selected_restriction.Content = dT.Rows[index][4].ToString();
            }
            sqlConn.Close();
        }
        private void UsersLogins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String Text = Users_logins.SelectedItem.ToString();
            sqlConn.Open();
            Data = new SqlDataAdapter("SELECT  Login,Status,Restriction From MainTable;", sqlConn);
            dT = new DataTable("Користувачі системи");
            Data.Fill(dT);
            for (int i = 0; i < dT.Rows.Count; i++)
            {
                String tempStr = dT.Rows[i][0].ToString();
                if (Text == tempStr)
                {
                    if (dT.Rows[i][1].ToString() == checkingt)
                        ChangeActive.IsChecked = true;
                    else if (dT.Rows[i][1].ToString() == checkingf)
                        ChangeActive.IsChecked = false;
                    if (dT.Rows[i][2].ToString() == checkingt)
                        ChangeRestriction.IsChecked = true;
                    else if (dT.Rows[i][2].ToString() == checkingf)
                        ChangeRestriction.IsChecked = false;
                }
            }
            sqlConn.Close();
        }
        string checkingt = "True";
        string checkingf = "False";
        private void CorrectStatus_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            String strQ;
            bool UserStatus = (bool)ChangeActive.IsChecked;
            if (sqlConn.State == ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Status ='" + UserStatus + "' WHERE Login='" + Users_logins.SelectedValue.ToString() + "';";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateDataTable();
        }
        private void CorrectRestriction_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            String strQ;
            bool UserRestriction = (bool)ChangeRestriction.IsChecked;
            if (sqlConn.State == ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Restriction ='" + UserRestriction + "' WHERE Login = '" + Users_logins.SelectedValue.ToString() + "'; ";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateDataTable();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            change_pass.Text = ""; change_pass.IsEnabled = false;
            change_newpass.Text = ""; change_newpass.IsEnabled = false;
            change_newpass2.Text = ""; change_newpass2.IsEnabled = false;
            adduser_login.Text = ""; adduser_login.IsEnabled = false;
            dataGrid.IsEnabled = false; Users_logins.IsEnabled = false;
            ChangeActive.IsEnabled = false; ChangeRestriction.IsEnabled = false;
            Prev.IsEnabled = false; Next.IsEnabled = false; Exit.IsEnabled = false;
            Update.IsEnabled = false;
            Add_user.IsEnabled = false;
            CorrectStatus.IsEnabled = false;
            CorrectRestriction.IsEnabled = false;
            admin_pass.Text = "";
            buf = 1;
        }
        private void Add_user_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            String strQ;
            String UserLogin = adduser_login.Text;
            adduser_login.Text = "";
            if (sqlConn.State == ConnectionState.Open)
            {
                strQ = "INSERT INTO MainTable (Name, Surname, Login, Status,Restriction) values('', '', '" + UserLogin + "', 1, 0); ";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
                MessageBox.Show("Користувача додано!");
            }
            sqlConn.Close();
            UpdateDataTable();
        }
        int count = 3;
        string psw;
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            var passAdm = admin_pass.Text;
            admin_pass.Text = "";
            Data = new SqlDataAdapter();
            if (sqlConn.State == ConnectionState.Open)
            {
                String strQ = "SELECT * FROM MainTable WHERE Login= 'admin' ;";
                Data = new SqlDataAdapter(strQ, sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                if (dT.Rows[0][3].ToString() == passAdm)
                {
                    change_pass.Text = ""; change_pass.IsEnabled = true;
                    change_newpass.Text = ""; change_newpass.IsEnabled = true;
                    change_newpass2.Text = ""; change_newpass2.IsEnabled = true;
                    adduser_login.Text = ""; adduser_login.IsEnabled = true;
                    Users_logins.IsEnabled = true; Exit.IsEnabled = true;
                    ChangeActive.IsEnabled = true; ChangeRestriction.IsEnabled = true;
                    Prev.IsEnabled = true; Next.IsEnabled = true;
                    Update.IsEnabled = true;
                    Add_user.IsEnabled = true;
                    CorrectStatus.IsEnabled = true;
                    CorrectRestriction.IsEnabled = true;
                    count = 3;
                    psw = dT.Rows[0][3].ToString();
                    pswadm = passAdm;
                    buf = 0;
                }
                else
                {
                    count--;
                    if (count == 0)
                        Application.Current.Shutdown();
                    String s = "Неправильний пароль. " + "Залишилось спроб: " + count.ToString();
                    MessageBox.Show(s);
                }
            }
            sqlConn.Close();
        }
        private void ToWin1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
