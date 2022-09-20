using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace F1Tools
{
    public static class DataReciver
    {
        private static UdpClient UDP;
        private static IPEndPoint FromIP;
        private static IPEndPoint ListenEndPoint;
        private static GameVersion _version = GameVersion.Unkonwn;
        private static Thread UDPThread;

        static DataReciver()
        {
            ListenEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20777);
            UDP = new UdpClient(ListenEndPoint);
            RestartTask();
        }

        private static void UDPReceive()
        {
            while (true)
            {
                var bytes = UDP.Receive(ref FromIP);
                if (bytes.Length > 0)
                {
                    var data = TypeFactory.GetData(bytes, out _version);
                    if (_version == GameVersion.Unkonwn || data == null)
                        return;
                    ReciveEvent?.Invoke(data);
                }
            }
        }

        public static int Port
        {
            get => ListenEndPoint.Port;
            set
            {
                if (value < IPEndPoint.MinPort || value > IPEndPoint.MaxPort)
                    return;

                if (ListenEndPoint.Port == value)
                    return;

                ListenEndPoint.Port = value;

                DisposeTask();
                UDP.Dispose();

                UDP = new UdpClient(ListenEndPoint);
                RestartTask();
#if DEBUG
                Console.WriteLine($"端口已修改为{value}");
#endif
            }
        }

        private static void RestartTask()
        {
            UDPThread = new Thread(UDPReceive);
            UDPThread.Start();
        }

        private static void DisposeTask()
        {
            UDPThread.Abort();
        }

        public static GameVersion Version => _version;

        public static void Dispose()
        {
            UDPThread.Abort();
            UDP.Close();
            UDP.Dispose();
        }

        public delegate void ReciverHandler(LocalData localData);
        public static event ReciverHandler ReciveEvent;
    }
}
