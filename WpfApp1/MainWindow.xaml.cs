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
        private int gear = 0;
        Timer Timer = new Timer(10);
        float a = 0, b = 0; int c = 0, d = 0, e = 0;

        public MainWindow()
        {
            InitializeComponent();
            //f1.img_cr.Source = new BitmapImage(new Uri("/Resources/cr.png", UriKind.Relative));
            //f1.img_lb.Source = new BitmapImage(new Uri("/Resources/lable.png", UriKind.Relative));
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;
            Timer.Start();
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
            f1.SetRPM(d);
            
            a = a > 1 ? 0 : a + 0.01F;
            b = b > 1 ? 0 : b + 0.01F;
            c = c > 360 ? 0 : c + 1;
            d = d > 16000 ? 0 : d + 10;
        }

        private void delegatedosomething()
        {
            f1.Dispatcher.Invoke(new RefleshUI(setvalue));
        }

        private void MainWindows_Keydown(object sender, KeyEventArgs e)
        {
            //判断用户的按键是否为Alt+F4
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Left))
            {
                f1.SetGear(--gear);
            }
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Right))
            {
                f1.SetGear(++gear);
            }

        }
    }
}
