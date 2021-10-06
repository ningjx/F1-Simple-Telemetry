using Codemasters.F1_2019;
using Codemasters.F1_2020;
using Codemasters.F1_2021;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static F1Tools.TypeFactory;

namespace F1Tools
{
    public static class DataReciver
    {
        private static UdpClient UDP;
        private static IPEndPoint IP;
        public static Thread RecThread;
        private static int _port = 666;
        private static GameVersion _version = GameVersion.F1_2020;

        static DataReciver()
        {
            UDP = new UdpClient(_port);
            RecThread = new Thread(Handle);
            RecThread.Start();
        }

        public static int Port
        {
            get => _port;
            set
            {
                Stop();
                _port = Port;
                UDP = new UdpClient(Port);
                Run();
            }
        }

        public static GameVersion Version
        {
            get => _version;
            set
            {
                _version = value;
                if (RecThread.ThreadState == ThreadState.Stopped)
                    Run();
            }
        }

        private static void Stop()
        {
            RecThread.Abort();
        }

        private static void Run()
        {
            if (RecThread.ThreadState != (ThreadState.Stopped | ThreadState.Aborted))
                RecThread.Abort();

            RecThread = new Thread(Handle);
            RecThread.Start();
        }

        public static void Dispose()
        {
            Stop();
            UDP.Close();
            UDP.Dispose();
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
                        var packet = TypeFactory.GetPacket(bytes, _version);
                        ReciveEvent?.Invoke(packet);
                    }
                }
            }
            catch { }
        }

        public delegate void ReciverHandler(object packet);
        public static event ReciverHandler ReciveEvent;
    }
}
