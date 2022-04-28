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
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для ThirdWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        public ThirdWindow()
        {
            InitializeComponent();
            initControls();
        }
        public int N = 50;
        private void initControls()
        {
            Title = "ThirdWindow";
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
                Content = "Вікно 3",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(350, 20, 0, 0),
                Height = 50,
                Width = 100,
                FontSize = 25
            };
            myGrid.Children.Add(lbl);

            for(int i = 0; i < 5; i++)
                for(int j = 0; j < 5; j++)
                {
                    ComboBox cb = new ComboBox
                    {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Margin = new Thickness(120 + (i * N), 80 + (j * N), 0, 0),
                        Height = 50,
                        Width = 50
                    };
                cb.Items.Add("X"); cb.Items.Add("O");
                myGrid.Children.Add(cb);
                }           
            ThiWindow.Content = myGrid;
            ThiWindow.Show();
        }
        private void ToWin1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }
    }
}
