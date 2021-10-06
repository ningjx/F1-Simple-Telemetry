using Codemasters.F1_2020;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace F1Tools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int PlayerIndex = 0;
        private Timer Timer;

        public MainWindow()
        {
            InitializeComponent();

            DataReciver.ReciveEvent += DataReciver_ReciveEvent;
            var ip = Helper.GetLocalIP();
            lb_ip.Content = $"当前IP：{ip}";
            lb_port.Content = $"当前端口：{DataReciver.Port}";

            Timer = new Timer(3000)
            {
                AutoReset = false
            };
            Timer.Elapsed += Timer_Elapsed;

            Dispatcher.Invoke(new WindowDelegate(CheckUpdate));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            gr_close.Dispatcher.Invoke(new WindowDelegate(HideCloseIcon));
        }

        private void CheckUpdate()
        {
            if (Helper.CheckUpdate())
            {
                tip.Visibility = Visibility.Visible;
            }
        }

        private void HideCloseIcon()
        {
            gr_close.Visibility = Visibility.Hidden;
        }

        public delegate void F1Delegate(Packet packet);
        public delegate void WindowDelegate();

        private void DataReciver_ReciveEvent(Packet packet)
        {
            f1.Dispatcher.Invoke(new F1Delegate(F1Handle), packet);
        }

        public void F1Handle(Packet packet)
        {
            if (packet == null)
                return;

            if (packet.PacketType == PacketType.Participants)
            {
                var curPack = packet as ParticipantPacket;
                PlayerIndex = curPack.FieldParticipantData.ToList().FindIndex(t => !t.IsAiControlled);
            }
            else if (packet.PacketType == PacketType.CarTelemetry)
            {
                var curPack = packet as TelemetryPacket;
                var data = curPack.FieldTelemetryData[PlayerIndex];

                f1.SetBreak(data.Brake);
                f1.SetThr(data.Throttle);
                f1.SetSpeed(data.SpeedKph);
                f1.SetRPM(data.EngineRpm);
                f1.SetGear(data.Gear);
                f1.SetDRS(data.DrsActive);
            }
            else if (packet.PacketType == PacketType.CarStatus)
            {
                var curPack = packet as CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                f1.DRSEnable(data.DrsAllowed);
                f1.DRSNegative(data.DrsFailure);
            }

            sp_ip.Visibility = Visibility.Hidden;

        }

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
    }
}
