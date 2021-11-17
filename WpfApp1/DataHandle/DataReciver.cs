﻿using System.Net;
using System.Net.Sockets;
using static F1Tools.TypeFactory;

namespace F1Tools
{
    public static class DataReciver
    {
        private static UdpClient UDP;
        private static IPEndPoint IP;
        public static MicroTimer MicroTimer;
        private static int _port = 666;
        private static GameVersion _version = GameVersion.Unkonwn;

        static DataReciver()
        {
            UDP = new UdpClient(_port);
            MicroTimer = new MicroTimer(1, 1);
            MicroTimer.OnRunningCallback += MicroTimer_OnRunningCallback;
            MicroTimer.Start();
        }

        private static void MicroTimer_OnRunningCallback(int id, int msg, int user, int param1, int param2)
        {
            if (UDP.Available <= 0)
                return;

            var bytes = UDP.Receive(ref IP);
            if (bytes.Length > 0)
            {
                var packet = GetPacket(bytes, _version, out _version);
                if (_version == GameVersion.Unkonwn)
                    return;
                ReciveEvent?.Invoke(packet);
            }
        }

        public static int Port
        {
            get => _port;
            set
            {
                MicroTimer.Stop();
                _port = Port;
                UDP = new UdpClient(Port);
                MicroTimer.Start();
            }
        }

        public static GameVersion Version => _version;

        public static void Dispose()
        {
            MicroTimer.Dispose();
            UDP.Close();
            UDP.Dispose();
        }

        public delegate void ReciverHandler(object packet);
        public static event ReciverHandler ReciveEvent;
    }
}
