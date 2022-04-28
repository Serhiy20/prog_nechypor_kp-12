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

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для FourthWindow.xaml
    /// </summary>
    public partial class FourthWindow : Window
    {
        public FourthWindow()
        {
            InitializeComponent();
            initControls();
        }
        public int N = 60, M = 40, k = 0;
        public TextBox TB = new TextBox
        {
            Name = "TB",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Height = 40, Width = 240,
            Margin = new Thickness(100, 100, 0, 0),
            TextWrapping = TextWrapping.Wrap,
            FontSize = 22
        };
        private void initControls()
        {
            Title = "FourthWindow";
            ResizeMode = ResizeMode.NoResize;
            Grid myGrid = new Grid();

            Button ToWin1 = new Button
            {
                Name = "ToWin1",
                Content = "До вікна 1",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(653, 320, 0, 0),
                Width = 120,
                Height = 45,
                FontSize = 16
            };
            ToWin1.Click += ToWin1_Click;
            myGrid.Children.Add(ToWin1);

            Label lbl = new Label
            {
                Content = "Вікно 4",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(350, 20, 0, 0),
                Height = 50,
                Width = 100,
                FontSize = 25
            };
            myGrid.Children.Add(lbl);

            for(int i = 4; i > 1; i--)
                for(int j = 0; j < 3; j++)
                {                    
                    Button btn = new Button
                    {
                        Content = $"{1 + k}",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(100 + (j * N), 100 + (i * M), 0, 0),
                        Height = 40,
                        Width = 60,
                        FontSize = 25
                    };
                    k++;
                    myGrid.Children.Add(btn);
                }
            Button emp1 = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(100, 140, 0, 0),
                Height = 40, Width = 60
            };
            myGrid.Children.Add(emp1);

            Button emp2 = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(280, 140, 0, 0),
                Height = 40, Width = 60
            };
            myGrid.Children.Add(emp2);

            Button emp3 = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(100, 300, 0, 0),
                Height = 40, Width = 60
            };
            myGrid.Children.Add(emp3);

            Button zero = new Button
            {
                Content = "0",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(160, 300, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(zero);

            Button dot = new Button
            {
                Content = ".",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(220, 300, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(dot);

            Button eq = new Button
            {
                Content = "=",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(160, 140, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(eq);

            Button C = new Button
            {
                Content = "C",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(220, 140, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(C);

            Button div = new Button
            {
                Content = "/",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(280, 180, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(div);

            Button mul = new Button
            {
                Content = "*",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(280, 220, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(mul);

            Button min = new Button
            {
                Content = "-",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(280, 260, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(min);

            Button plus = new Button
            {
                Content = "+",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(280, 300, 0, 0),
                Height = 40, Width = 60,
                FontSize = 25
            };
            myGrid.Children.Add(plus);

            myGrid.Children.Add(TB);
            FourWindow.Content = myGrid;
            FourWindow.Show();
            foreach (UIElement elem in myGrid.Children)
            {
                if (elem is Button)
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
