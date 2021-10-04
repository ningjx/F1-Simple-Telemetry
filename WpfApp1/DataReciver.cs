using Codemasters.F1_2020;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WpfApp1
{
    public static class DataReciver
    {
        private static UdpClient UDP;
        private static IPEndPoint IP;
        private static Packet Packet;
        private static Thread RecThread;
        private static int _port = 666;

        static DataReciver()
        {
            UDP = new UdpClient(_port);
            RecThread = new Thread(Handle);
            RecThread.Start();
        }

        private static void Run()
        {
            RecThread.Start();
        }

        public static int Port
        {
            get { return _port; }
            set
            {

                Stop();
                _port = Port;
                UDP = new UdpClient(Port);
                Run();
            }
        }

        public static void Dispose()
        {
            Stop();
            UDP.Close();
            UDP.Dispose();
        }

        private static void Stop()
        {
            RecThread.Abort();
        }

        private static void Handle()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(0);
                    if (UDP.Available <= 0)
                        continue;

                    var bytes = UDP.Receive(ref IP);
                    if (bytes.Length > 0)
                    {
                        Packet = TypeFactory.GetPacket(bytes);
                        ReciveEvent?.Invoke(Packet);
                    }
                }
            }
            catch { }
        }

        public delegate void ReciverHandler(Packet packet);
        public static event ReciverHandler ReciveEvent;
    }
}
