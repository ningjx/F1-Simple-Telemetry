using Codemasters.F1_2020;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class DataReciver
    {
        private static UdpClient UDP;
        //private static Task RecTask;
        private static IPEndPoint IP;
        private static Packet Packet;
        private static Thread RecThread;
        private static int Port = 666;

        static DataReciver()
        {
            UDP = new UdpClient(Port);
            //RecTask = new Task(Handle);
            RecThread = new Thread(Handle);
            RecThread.Start();
        }

        private static void Run()
        {
            //RecTask.Start();
            RecThread.Start();
        }

        public static void ChangePort(int port)
        {
            Stop();
            Port = port;
            UDP = new UdpClient(Port);
            Run();
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
