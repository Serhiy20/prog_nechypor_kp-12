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

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();
            initControls();
        }
        public TextBox TB1 = new TextBox
        {
            Name = "TB1",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Height = 70, Width = 140,
            Margin = new Thickness(80, 100, 0, 0),
            TextWrapping = TextWrapping.Wrap
        };
        public TextBox TB2 = new TextBox
        {
            Name = "TB2",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Height = 40, Width = 140,
            Margin = new Thickness(80, 260, 0, 0),
            TextWrapping = TextWrapping.Wrap
        };
        private void initControls()
        {
            Title = "SecondWindow";
            ResizeMode = ResizeMode.NoResize;
            Grid myGrid = new Grid();

            Button ToWin1 = new Button
            {
                Name = "ToWin1",
                Content = "До вікна 1",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(653, 320, 0, 0),
                Width = 120, Height = 45, FontSize = 16
            };
            ToWin1.Click += ToWin1_Click;
            myGrid.Children.Add(ToWin1);
            
            Button Read = new Button
            {
                Content = "Прочитати",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(270, 100, 0, 0),
                Width = 100, Height = 40
            };
            Read.Click += Read_Click;
            myGrid.Children.Add(Read);
            
            Button Delete = new Button
            {
                Content = "Видалити",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(270, 260, 0, 0),
                Width = 100, Height = 40
            };
            Delete.Click += Delete_Click;
            myGrid.Children.Add(Delete);
            
            Label lbl1 = new Label
            {
                Content = "Вікно 2",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(350, 20, 0, 0),
                Height = 50, Width = 100,
                FontSize = 25
            };
            myGrid.Children.Add(lbl1);

            Label lbl2 = new Label
            {
                Content = "Введіть код залікової \nкнижки для \nвидалення студента:",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(80, 191, 0, 0),
                Height = 64, Width = 173
            };
            myGrid.Children.Add(lbl2);

            myGrid.Children.Add(TB1);
            myGrid.Children.Add(TB2);
            SecWindow.Content = myGrid;
            SecWindow.Show();
        }
        private void ToWin1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }
        private void Read_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter str = new StreamWriter(@"D:\textfile.txt", true);
            str.WriteLine(TB1.Text);
            TB1.Text = "";
            str.Close();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string s = TB2.Text;
            var str = File.ReadAllLines(@"D:\textfile.txt");
            var str1 = str.Where(line => !line.Contains(s));
            File.WriteAllLines(@"D:\textfile.txt", str1);
            TB2.Text = "";
        }
    }
}
