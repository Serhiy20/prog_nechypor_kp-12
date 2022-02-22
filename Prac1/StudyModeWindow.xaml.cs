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
    /// Логика взаимодействия для StudyModeWindow.xaml
    /// </summary>
    public partial class StudyModeWindow : Window
    {
        public StudyModeWindow()
        {
            InitializeComponent();
        }
        double[,]arr = new double[3, 5];
        int[] sprobaperezap = new int[3];
        private void CloseStudyMode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw;
            mw = new MainWindow();
            Hide();
            mw.Show();
        }
        static int kupbuf = 0;
        private void TextBox_PreviewKeyUp(object sender, RoutedEventArgs e)
        {
            SymbolCount.Content = InputField.Text.Length;
            if (InputField.Text == "футбол")
            {
                InputField.Text = "";
                CountSproba.Content = $"{label}";
                kupbuf++;
            }
            if (InputField.Text != "футбол" && InputField.Text != "футбо" && InputField.Text != "футб" && InputField.Text != "фут" && InputField.Text != "фу" && InputField.Text != "ф" && InputField.Text != "")
            {
                count = 0;
                m = 0;
                InputField.Text = "";
                sW.Stop();
                sW.Reset();
            }
        }
        static int n = 0, m = 0;
        static Stopwatch sW = new Stopwatch();
        static int count = 0, perezap = 0, label = 3;
        static double aaa = 0.0, ts = 0.0;
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (label != 0)
            {
                if (perezap == 0)
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
                else
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
                            //n++;
                            arr[sprobaperezap[perezap - 1], m] = (ts - aaa) / 1000;
                            m++;
                        }
                        else
                        {
                            arr[sprobaperezap[perezap - 1], m] = (ts - aaa) / 1000;
                            m++;
                        }
                        aaa = ts;
                        count++;
                    }
                    else if (count == 5)
                    {
                        ts = sW.ElapsedMilliseconds;
                        arr[sprobaperezap[perezap - 1], m] = (ts - aaa) / 1000;
                        sW.Stop();
                        sW.Reset();
                        count = 0;
                        label--;
                        countsp--;
                        perezap--;
                    }
                }
            }
        }
        static double stu = 3.18;
        static double mathsp = 0.0;
        private void Zn_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                {
                    Znach(arr[i, j], i, j);
                }
        }
        private void Znach(double time, int i, int j)
        {
            double summ = 0.0, buf = 0.0, summ2 = 0.0;
            int counts = 0;
            for (int m = 0; m < 5; m++)
            {
                if (m == j)
                {
                    summ += 0;
                }
                else
                {
                    summ += arr[i, m];
                    counts++;
                }
            }
            mathsp = summ / counts;
            for (int m = 0; m < 5; m++)
            {
                if (m == j)
                    summ2 += 0;
                else
                {
                    buf = arr[i, m] - mathsp;
                    summ2 += Math.Pow(buf, 2);
                }
            }
            double s2 = summ2 / (counts - 1);
            double s = 0.0;
            s = Math.Sqrt(s2);
            double tp = 0.0;
            tp = Math.Abs((time - mathsp) / s);
            if (tp > stu)
            {
                if (perezap == 0)
                {
                    q = 0;
                    xx = 0;
                }
                if (q == 0)
                {
                    sprobaperezap[countsp] = i;
                    perezap++;
                    label++;
                    CountSproba.Content = $"{label}";
                    countsp++;
                    xx = i;
                    q++;
                }
                else if (q != 0 && xx != i)
                {
                    sprobaperezap[countsp] = i;
                    perezap++;
                    label++;
                    CountSproba.Content = $"{label}";
                    countsp++;
                    q = 0;
                }
            }
        }
        private void File_Click(object sender, RoutedEventArgs e)
        {
            {
                int j = 0;
                for (int i = 0; i < 3; i++)
                    Etalone(arr[i, j], i);
            }
        }
        private void Etalone(double time, int i)
        {
            double summ = 0.0;
            for (int m = 0; m < 5; m++)
                summ += arr[i, m];

            double msp = summ / 5;
            double summ2 = 0.0;
            for (int m = 0; m < 5; m++)
                summ2 += Math.Pow(arr[i, m] - msp, 2);

            double dp = summ2 / 3;
            try
            {
                using (StreamWriter MyFileG = new StreamWriter("Prac1.txt", true, Encoding.Default))
                {
                    MyFileG.WriteLine($"{msp} {dp}");
                    MyFileG.Close();
                }
            }
            catch (Exception ex)
            {
                InputField.Text = ex.Message;
            }
        }
        static int countsp = 0, q = 0, xx = 0;
    }
}
