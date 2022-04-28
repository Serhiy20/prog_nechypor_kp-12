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
    /// Логика взаимодействия для FifthWindow.xaml
    /// </summary>
    public partial class FifthWindow : Window
    {
        public FifthWindow()
        {
            InitializeComponent();
            initControls();
        }
        private void initControls()
        {
            Title = "FifthWindow";
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

            Label lbl1 = new Label
            {
                Content = "Вікно 5",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(350, 20, 0, 0),
                Height = 50,
                Width = 100,
                FontSize = 25
            };
            myGrid.Children.Add(lbl1);

            Label lbl2 = new Label
            {
                Content = "Розробник: Нечипор Сергій Віталійович\nГрупа: КП-12\nРік створення: 2022",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(118, 114, 0, 0),
                Height = 123, Width = 565,
                FontSize = 25
            };
            myGrid.Children.Add(lbl2);

            FifWindow.Content = myGrid;
            FifWindow.Show();
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
