using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace Prac3
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        SqlConnection sqlConn = new SqlConnection(@"Data Source=SERHIY; Initial Catalog=Prac3; Integrated Security=True");
        SqlCommand Com;
        SqlDataAdapter Data;
        public UserWindow()
        {
            InitializeComponent();
            Update.IsEnabled = false;
            LogOut.IsEnabled = false;
            change_name.IsEnabled = false;
            change_surname.IsEnabled = false;
            change_password.IsEnabled = false;
            change_password2.IsEnabled = false;
        }
        DataTable dT = new DataTable();
        int count = 3;
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            var login = auth_login.Text;
            var pass = auth_password.Text;
            Data = new SqlDataAdapter();
            if(login != null)
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    String strQ = "SELECT * FROM MainTable WHERE Login='" + login + "';";
                    Data = new SqlDataAdapter(strQ, sqlConn);
                    dT = new DataTable("Користувачi системи");
                    Data.Fill(dT);
                    if (dT.Rows.Count == 0)
                        MessageBox.Show("Такого користувача на знайдено");
                    else
                    {
                        bool status = Convert.ToBoolean(dT.Rows[0][4]);
                        if(!status)
                            MessageBox.Show("Вас заблоковано Адмiнiстратором!");
                        else
                        {
                            if ((dT.Rows[0][2].ToString() == login) && (dT.Rows[0][3].ToString() == pass))
                            {
                                change_name.Text = dT.Rows[0][0].ToString();
                                change_surname.Text = dT.Rows[0][1].ToString();
                                change_password.Text = "";
                                change_password2.Text = "";
                                change_name.IsEnabled = true;
                                change_surname.IsEnabled = true;
                                change_password.IsEnabled = true;
                                change_password2.IsEnabled = true;
                                Update.IsEnabled = true;
                                LogOut.IsEnabled = true;
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
                    }
                }
            }
            else
            {
                MessageBox.Show("Уведiть логiн");
            }
            sqlConn.Close();
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            change_name.Text = "";
            change_surname.Text = "";
            change_password.Text = "";
            change_password2.Text = "";
            change_name.IsEnabled = false;
            change_surname.IsEnabled = false;
            change_password.IsEnabled = false;
            change_password2.IsEnabled = false;
            Update.IsEnabled = false;
            LogOut.IsEnabled = false;
            auth_login.Text = "";
            auth_password.Text = "";
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var login = auth_login.Text;
            sqlConn.Open();
            String name = change_name.Text;
            String surname = change_surname.Text;
            String pass = change_password.Text;
            String pass2 = change_password2.Text;
            if (sqlConn.State == ConnectionState.Open)
            {
                String strQ;
                if ((pass == pass2) && (pass != ""))
                {
                    Boolean flag = RestrictionFunc(pass);
                    if (Convert.ToBoolean(dT.Rows[0][5]) == true)
                    {
                        if (flag == true)
                        {
                            strQ = "UPDATE MainTable SET Name='" + name + "', ";
                            strQ += "Surname='" + surname + "', ";
                            strQ += "Password='" + pass + "' ";
                            strQ += "WHERE Login='" + login + "';";
                            Com = new SqlCommand(strQ, sqlConn);
                            Com.ExecuteNonQuery();
                            change_password.Text = "";
                            change_password2.Text = "";
                            MessageBox.Show("Дані успішно змінено!");
                        }
                        else
                            MessageBox.Show("У паролі немає літер верхнього та нижнього регістрів, а також арифметичних операцій! Спробуйте знову!");
                    }
                    else
                    {
                        strQ = "UPDATE MainTable SET Name='" + name + "', ";
                        strQ += "Surname='" + surname + "', ";
                        strQ += "Password='" + pass + "' ";
                        strQ += "WHERE Login='" + login + "';";
                        Com = new SqlCommand(strQ, sqlConn);
                        Com.ExecuteNonQuery();
                        MessageBox.Show("Дані успішно змінено!");
                        change_password.Text = "";
                        change_password2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Новий пароль не введено новий пароль повторно введено некоректно!");
                }
            }
            sqlConn.Close();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            sqlConn.Open();
            String name = reg_name.Text;
            String surname = reg_surname.Text;
            String login = reg_login.Text;
            String pass = reg_password.Text;
            String pass2 = reg_password2.Text;
            String strQ;
            if (name != null && surname != null)
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    try
                    {
                        if ((pass == pass2) && (login != "") && (pass != ""))
                        {
                            strQ = "INSERT INTO MainTable ";
                            strQ += "VALUES ('" + name + "', '" + surname +
                            "', '" + login + "', '" + pass + "','True', 'False'); ";
                            Com = new SqlCommand(strQ, sqlConn);
                            Com.ExecuteNonQuery();
                            MessageBox.Show("Обліковий запис успішно створено!");
                            reg_name.Text = "";
                            reg_surname.Text = "";
                            reg_login.Text = "";
                            reg_password.Text = "";
                            reg_password2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Обліковий запис не створено. Спробуйте ще раз!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Користувач з таким логіном вже існує у системі!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Уведіть всі дані!");
            }
            sqlConn.Close();
        }
        private void ToWin1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            Hide();
            mw.Show();
        }
        Boolean RestrictionFunc(String Pass)
        {
            Byte Count1, Count2, Count3;
            Byte LenPass = (Byte)Pass.Length;
            Count1 = Count2 = Count3 = 0;
            for (Byte i = 0; i < LenPass; i++)
            {
                if ((Convert.ToInt32(Pass[i]) >= 65) &&

                (Convert.ToInt32(Pass[i]) <= 65 + 25))

                    Count1++;
                if ((Convert.ToInt32(Pass[i]) >= 97) &&

                (Convert.ToInt32(Pass[i]) <= 97 + 25))

                    Count2++;
                if ((Pass[i] == '+') || (Pass[i] == '-') || (Pass[i] == '*') ||

                (Pass[i] == '/'))

                    Count3++;
            }
            return (Count1 * Count2 * Count3 != 0);
        }
    }
}
