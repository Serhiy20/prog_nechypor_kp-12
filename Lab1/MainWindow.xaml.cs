using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
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
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToWin2_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow sw;
            sw = new SecondWindow();
            Hide();
            sw.Show();
        }

        private void ToWin3_Click(object sender, RoutedEventArgs e)
        {
            ThirdWindow tw;
            tw = new ThirdWindow();
            Hide();
            tw.Show();
        }

        private void ToWin4_Click(object sender, RoutedEventArgs e)
        {
            FourthWindow fow;
            fow = new FourthWindow();
            Hide();
            fow.Show();
        }

        private void ToWin5_Click(object sender, RoutedEventArgs e)
        {
            FifthWindow fiw;
            fiw = new FifthWindow();
            Hide();
            fiw.Show();
        }
    }
}
