using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer Timer = new Timer(200);
        float a = 0, b = 0, c = 0, d = 0, e = 0;

        public MainWindow()
        {
            InitializeComponent();
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;
            //Timer.Start();
        }
        public delegate void RefleshUI();
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            delegatedosomething();
        }

        private void setvalue()
        {
            f1.SetBreak(a);
            f1.SetThr(b);
            f1.SetSpeed(c);

            a = a > 1 ? 0 : a + 0.1F;
            b = b > 1 ? 0 : b + 0.1F;
            c = c > 360 ? 0 : c + 10F;
        }

        private void delegatedosomething()
        {
            f1.Dispatcher.Invoke(new RefleshUI(setvalue));
            //  ellipse2.Dispatcher.Invoke(new RefleshUI(UIThreaddosomething), s);
        }
    }
}
