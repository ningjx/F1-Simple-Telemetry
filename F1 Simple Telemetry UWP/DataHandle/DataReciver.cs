using Windows.Networking.Sockets;
using static F1Tools.TypeFactory;

namespace F1Tools
{
    public static class DataReciver
    {
        private static int _port = 20777;
        private static GameVersion _version = GameVersion.Unkonwm;

        static DataReciver()
        {
            var UDPListener = new DatagramSocket();
            UDPListener.BindServiceNameAsync("666").GetResults();
            UDPListener.MessageReceived += ServerDatagramSocket_MessageReceived;
        }

        private static void ServerDatagramSocket_MessageReceived(DatagramSocket socket, DatagramSocketMessageReceivedEventArgs args)
        {
            using (var reader = args.GetDataReader())
            {
                var buf = new byte[reader.UnconsumedBufferLength];
                reader.ReadBytes(buf);
                if (buf.Length > 0)
                {
                    var packet = TypeFactory.GetPacket(buf, _version, out _version);
                    ReciveEvent?.Invoke(packet);
                }
            }
        }

        public static GameVersion Version => _version;

        public delegate void ReciverHandler(object packet);
        public static event ReciverHandler ReciveEvent;
    }
}
