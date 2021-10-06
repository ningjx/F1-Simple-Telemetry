using Codemasters.F1_2019;
using Codemasters.F1_2020;
using Codemasters.F1_2021;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using static F1Tools.TypeFactory;
using Timer = System.Timers.Timer;

namespace F1Tools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer Timer;

        public MainWindow()
        {
            InitializeComponent();
            game_v.Items.Add("F1 2019");
            game_v.Items.Add("F1 2020");
            game_v.Items.Add("F1 2021");
            game_v.SelectedItem = "F1 2020";

            DataReciver.Version = TypeFactory.GameVersion.F1_2020;
            DataReciver.ReciveEvent += DataReciver_ReciveEvent;
            var ip = Helper.GetLocalIP();
            lb_ip.Content = $"当前IP：{ip}";
            lb_port.Content = $"当前端口：{DataReciver.Port}";

            Timer = new Timer(3000)
            {
                AutoReset = false
            };
            Timer.Elapsed += Timer_Elapsed;

            Dispatcher.Invoke(() =>
            {
                Task.Run(() =>
                {
                    if (Helper.CheckUpdate())
                    {
                        tip.Visibility = Visibility.Visible;
                    }
                });
            });
        }

        private delegate void WindowDelegate();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            gr_close.Dispatcher.Invoke(new WindowDelegate(HideCloseIcon));
        }

        private void HideCloseIcon()
        {
            gr_close.Visibility = Visibility.Hidden;
        }

        private void HideIP()
        {
            sp_ip.Visibility = Visibility.Hidden;
        }

        #region F1 Data Processor
        private delegate void F1InstrumentDelegate(F1Instrument f1, object packet);

        private void DataReciver_ReciveEvent(object packet)
        {
            f1.Dispatcher.Invoke(new F1InstrumentDelegate(ShowDataHandle.F1Handle), f1, packet);
            if (sp_ip.Visibility != Visibility.Hidden)
                Dispatcher.Invoke(new WindowDelegate(HideIP));
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataReciver.Dispose();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            gr_close.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Timer.Enabled = true;
            Timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                if (e.Delta > 0)
                {
                    view_box.Height += 10;
                    view_box.Width += 10;
                }
                else
                {
                    view_box.Height -= 10;
                    view_box.Width -= 10;

                }
                Width = view_box.Width * 1.1;
                Height = view_box.Height * 1.1;
                gr_bac.Height = view_box.Height * 1.1;
                gr_bac.Width = view_box.Height * 1.1;
            }
            else
            {
                f1.img_bc.Opacity = e.Delta > 0 ? f1.img_bc.Opacity += 0.1 : f1.img_bc.Opacity -= 0.1;
                f1.img_bc.Opacity = f1.img_bc.Opacity > 1 ? 1 : f1.img_bc.Opacity;
                f1.img_bc.Opacity = f1.img_bc.Opacity <= 0.01 ? 0.01 : f1.img_bc.Opacity;
            }
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            var window = sender as Window;
            window.Topmost = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tip.Visibility = Visibility.Hidden;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/ningjx/F1-2020-Telemetering-Tools/releases");
        }

        private void game_v_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            switch (game_v.SelectedItem as string)
            {
                case "F1 2019":
                    DataReciver.Version = TypeFactory.GameVersion.F1_2019;
                    break;
                case "F1 2020":
                    DataReciver.Version = TypeFactory.GameVersion.F1_2020;
                    break;
                case "F1 2021":
                    DataReciver.Version = TypeFactory.GameVersion.F1_2021;
                    break;
            }

        }
    }
}
