using System;
using System.Diagnostics;
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

namespace Prac1
{
    /// <summary>
    /// Логика взаимодействия для ProtectionModeWindow.xaml
    /// </summary>
    public partial class ProtectionModeWindow : Window
    {
        public ProtectionModeWindow()
        {
            InitializeComponent();
        }
        private void CloseProtectionMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }
        static double[,] data = new double[3, 2];
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            List<String> slova = new List<string>();
            using (StreamReader sr = new StreamReader("Prac1.txt"))
            {
                while (!sr.EndOfStream)
                    slova.Add(sr.ReadLine());
            }
            string[] attempt1 = slova[0].Split(' ');
            string[] attempt2 = slova[1].Split(' ');
            string[] attempt3 = slova[2].Split(' ');
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == 0)
                    {
                        double buf = Convert.ToDouble(attempt1[j]);
                        data[i, j] = buf;
                    }
                    if (i == 1)
                    {
                        double buf = Convert.ToDouble(attempt2[j]);
                        data[i, j] = buf;
                    }
                    if (i == 2)
                    {
                        double buf = Convert.ToDouble(attempt3[j]);
                        data[i, j] = buf;
                    }
                }
            }
        }
        static Stopwatch sW = new Stopwatch();
        static int count = 0, n = 0, m = 0;
        static double aaa = 0.0, ts = 0.0;
        double[,] arr = new double[5, 5];
        static int kupbuf = 0;
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (label != 0)
            {
                if (count == 0)
                {
                    aaa = 0;
                    ts = 0;
                    sW.Start();
                    count++;
                }
                else if (count > 0 && count < 5)
                {
                    ts = sW.ElapsedMilliseconds;
                    if (m == 4)
                    {
                        m = 0;
                        n++;
                        arr[n, m] = (ts - aaa) / 1000;
                        m++;
                    }
                    else
                    {
                        arr[n, m] = (ts - aaa) / 1000;
                        m++;
                    }
                    aaa = ts;
                    count++;
                }
                else if (count == 5)
                {
                    ts = sW.ElapsedMilliseconds;
                    arr[n, m] = (ts - aaa) / 1000;
                    sW.Stop();
                    sW.Reset();
                    count = 0;
                    label--;
                }
            }
        }
        static int label = 5, counter = 0, equaldisp = 0, noequaldisp = 0;
        private void InputField_PreviewKeyUp(object sender, RoutedEventArgs e)
        {
            SymbolCount.Content = InputField.Text.Length;
            CountSproba.Content = $"{label}";
            if (InputField.Text != "футбол" && InputField.Text != "футбо" && InputField.Text != "футб" && InputField.Text != "фут" && InputField.Text != "фу" && InputField.Text != "ф" && InputField.Text != "")
            {
                count = 0;
                m = 0;
                InputField.Text = "";
                sW.Stop();
                sW.Reset();
            }
            if (InputField.Text == "футбол")
            {
                InputField.Text = "";
                kupbuf++;
                if (label != 0)
                {
                    double summ = 0.0, summkv = 0.0;
                    for (int i = 0; i < 5; i++)
                    {
                        summ += arr[counter, i];
                        summkv += Math.Pow(arr[count, i], 2);
                    }
                    double mathsp = summ / 5.0, mathsp2 = summkv / 5.0;
                    double disp2 = Math.Pow(mathsp2 - mathsp, 2);
                    double smax, smin;
                    for (int i = 0; i < 3; i++)
                    {
                        double dispetalon = data[i, 1];
                        if (disp2 > dispetalon)
                        {
                            smax = disp2;
                            smin = dispetalon;
                        }
                        else
                        {
                            smax = dispetalon;
                            smin = disp2;
                        }
                        double Fp = smax / smin;

                        if (Fp > Fisher)
                            noequaldisp++;
                        else
                            equaldisp++;
                    }
                    if (equaldisp > noequaldisp)
                        DispField.Content = "однорідна";
                    else
                        DispField.Content = "неоднорідна";
                    counter++;
                }
            }
        }
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            Analyz();
        }
        static double good = 0.0, errorall = 0.0;
        private void Analyz()
        {
            for (int i = 0; i < 5; i++)
            {
                double summ = 0.0, summkv = 0.0;
                for (int j = 0; j < 5; j++)
                {
                    summ += arr[i, j];
                }
                double mathspy = summ / 5;
                for (int j = 0; j < 5; j++)
                {
                    summkv += Math.Pow(arr[i, j] - mathspy, 2);
                }
                double sy2 = summkv / 4.0;
                int error = 0;
                double buf = 0.0, s = 0.0, tp = 0.0;
                for (int j = 0; j < 3; j++)
                {
                    buf = data[j, 1];
                    s = Math.Sqrt((buf + sy2) * 4 / 9.0);
                    tp = Math.Abs(data[j, 0] - mathspy) / (s * Math.Sqrt(2.0 / 5.0));
                    if (tp > stu)
                    {
                        error++;
                        errorall++;
                    }
                    else
                    {
                        good++;
                    }
                }
                double pp = (3 - error) / 3.0;
                p += pp;
                if (i == 4)
                {
                    StatisticsBlock.Content = $"{p / 5}";
                    if (textbox1.Text == "1")//1 - розробник
                    {
                        p1 = errorall / 15;
                        P1Field.Content = $"{p1}";
                    }
                    else if (textbox1.Text == "0")//0 - користувач
                    {
                        p2 = good / 15;
                        P2Field.Content = $"{p2}";
                    }
                }
            }
        }
        static double Fisher = 6.59, p = 0.0, stu = 2.78, p1 = 0.0, p2 = 0.0;
    }
}
