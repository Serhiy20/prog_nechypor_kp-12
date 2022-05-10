using System;
using static System.Math;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Prac2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int Radius = 30;
        static int PointCount = 5;
        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static List<Point> pC = new List<Point>();
        public MainWindow()
        {
            InitializeComponent();
            InitPoints();
            InitPolygon();
        }
        private void InitPoints()//створення міст
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();
            for (int i = 0; i < PointCount; i++)//створення точок
            {
                Point p = new Point();
                p.X = rnd.Next(Radius, (int)(0.75 * MainWin.Width) - 3 * Radius);
                p.Y = rnd.Next(Radius, (int)(0.90 * MainWin.Height - 3 * Radius));
                pC.Add(p);
            }
            for (int i = 0; i < PointCount; i++)//створення міст
            {
                Ellipse el = new Ellipse();
                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }
        }
        private void InitPolygon()//створення шляхів
        {
            myPolygon.Stroke = Brushes.Black;
            myPolygon.StrokeThickness = 2;
        }
        private void PlotPoints()//додавання міст у вікно
        {
            for (int i = 0; i < PointCount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X - Radius / 2);
                Canvas.SetTop(EllipseArray[i], pC[i].Y - Radius / 2);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }
        private void PlotWay(List<Point> Points)//додавання шляхів у вікно
        {
            PointCollection pC = new PointCollection();
            for (int i = 0; i < Points.Count; i++)
            {
                pC.Add(Points[i]);
            }
            myPolygon.Points = pC;
            MyCanvas.Children.Add(myPolygon);
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
            PlotPoints();
            greedy_al();
            PlotWay(pC);
        }
        private void NumElemCB_SelectionChanged(object sender, SelectionChangedEventArgs e)//зміна к-ті міст
        {
            ComboBox CB = (ComboBox)e.Source;
            ListBoxItem item = (ListBoxItem)CB.SelectedItem;
            PointCount = Convert.ToInt32(item.Content);
            InitPoints();
            InitPolygon();
        }
        private void greedy_al()
        {
            for (int i = 0; i < pC.Count - 1; i++)
            {
                Point closestP = pC[i + 1];
                Point firstP = pC[i];
                Point secondP = pC[i + 1];
                double lenght = Length_p(firstP, secondP);
                for (int j = i + 2; j < pC.Count; j++)
                {
                    secondP = pC[j];
                    if (lenght > Length_p(firstP, secondP))
                    {
                        lenght = Length_p(firstP, secondP);
                        closestP = pC[j];
                    }
                }
                Point temp = pC[i + 1];
                int index = pC.IndexOf(closestP);
                pC[i + 1] = closestP;
                pC[index] = temp;
            }
        }
        static double Length_p(Point a, Point b)
        {
            double length = Pow((a.X - b.X), 2) + Pow((a.Y - b.Y), 2);
            length = Sqrt(length);
            return length;
        }
        private void ToWin2_Click(object sender, RoutedEventArgs e)
        {
            SecondWindow sw;
            sw = new SecondWindow();
            Hide();
            sw.Show();
        }
    }
}

