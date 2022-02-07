using System;
using System.Data;
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
    /// Логика взаимодействия для FourthWindow.xaml
    /// </summary>
    public partial class FourthWindow : Window
    {
        public FourthWindow()
        {
            InitializeComponent();
            foreach(UIElement elem in Grid1.Children)
            {
                if(elem is Button)
                {
                    ((Button)elem).Click += Button_Click;
                }
            }
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
            string s = (string)((Button)e.OriginalSource).Content;//отримання напису на кнопці
            if (s == "C")
                TB.Text = "";
            else if (s == "=")
            {
                //DataTable - бібліотека для виконання математичних дій
                string res = new DataTable().Compute(TB.Text, null).ToString();
                //Compute - метод для того щоб виконати математичну операцію
                TB.Text = res;
            }
            else
                TB.Text += s;
        }
    }
}
