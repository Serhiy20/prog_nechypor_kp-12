using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
        }

        private void ToWin1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter str = new StreamWriter(@"D:\textfile.txt", true);
            str.WriteLine(TB1.Text);
            str.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string s = TB2.Text;
            var str = File.ReadAllLines(@"D:\textfile.txt");
            var str1 = str.Where(line => !line.Contains(s));
            File.WriteAllLines(@"D:\textfile.txt", str1);
        }
    }
}
