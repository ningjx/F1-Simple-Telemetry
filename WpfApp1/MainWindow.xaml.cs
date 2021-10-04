using Codemasters.F1_2020;
using System.Linq;
using System.Windows;


namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Index = 0;
        public MainWindow()
        {
            InitializeComponent();
            DataReciver.ReciveEvent += DataReciver_ReciveEvent;
            var ip = Helper.GetLocalIP();
            lb_ip.Content = $"当前IP：{ip}";
            lb_port.Content = $"当前端口：{DataReciver.Port}";
            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataReciver.Dispose();
        }

        public delegate void F1Delegate(Packet packet);

        public void F1Handle(Packet packet)
        {
            if (packet == null)
                return;

            if (packet.PacketType == PacketType.Participants)
            {
                var curPack = packet as ParticipantPacket;
                Index = curPack.FieldParticipantData.ToList().FindIndex(t => !t.IsAiControlled);
            }
            else if (packet.PacketType == PacketType.CarTelemetry)
            {
                var curPack = packet as TelemetryPacket;
                var data = curPack.FieldTelemetryData[Index];

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

        private void DataReciver_ReciveEvent(Packet packet)
        {
            f1.Dispatcher.Invoke(new F1Delegate(F1Handle), packet);
        }
    }
}
