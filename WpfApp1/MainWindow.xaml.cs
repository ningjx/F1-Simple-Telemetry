using Codemasters.F1_2020;
using System.Windows;


namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataReciver.ReciveEvent += DataReciver_ReciveEvent;
        }

        private void DataReciver_ReciveEvent(Packet packet)
        {
            if (packet.PacketType == PacketType.CarTelemetry)
            {
                var curPack = packet as TelemetryPacket;
                var data = curPack.FieldTelemetryData[0];

                f1.SetBreak(data.Brake);
                f1.SetThr(data.Throttle);
                f1.SetSpeed(data.SpeedKph);
                f1.SetRPM(data.EngineRpm);
                f1.SetGear(data.Gear);
                f1.SetDRS(data.DrsActive);
            }
            if (packet.PacketType == PacketType.CarStatus)
            {
                var curPack = packet as CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                f1.DRSEnable(data.DrsAllowed);
                f1.DRSNegative(data.DrsFailure);
            }
        }
    }
}
