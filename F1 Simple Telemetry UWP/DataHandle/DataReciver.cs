﻿using NingSoft.CSharpTools;
using System;
using System.Net;
using System.Net.Sockets;
//using static F1Tools.TypeFactory;

namespace F1Tools
{
    public static class DataReciver
    {
        private static UdpClient UDP;
        private static IPEndPoint FromIP;
        private static IPEndPoint ListenEndPoint;
        public static MicroTimer MicroTimer;
        //private static int _port = 20777;
        private static GameVersion _version = GameVersion.Unkonwn;

        static DataReciver()
        {
            ListenEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20777);
            UDP = new UdpClient(ListenEndPoint);
            MicroTimer = new MicroTimer(5, TimerMode.PERIODIC);
            MicroTimer.OnRunning += MicroTimer_OnRunningCallback;
            MicroTimer.Start();
        }

        private static void MicroTimer_OnRunningCallback(ulong ticks)
        {
            if (UDP == null || UDP.Available <= 0)
                return;

            var bytes = UDP.Receive(ref FromIP);
            if (bytes.Length > 0)
            {
                var data = TypeFactory.GetData(bytes, out _version);
                if (_version == GameVersion.Unkonwn || data == null)
                    return;
                ReciveEvent?.Invoke(data);
#if DEBUG
                Console.WriteLine($"{data.Throttle} {data.Brake}");
#endif
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
                UDP.Dispose();
                UDP = new UdpClient(ListenEndPoint);
#if DEBUG
                Console.WriteLine($"端口已修改为{value}");
#endif
            }
        }

        public static GameVersion Version => _version;

        public static void Dispose()
        {
            MicroTimer.Stop();
            UDP.Close();
            UDP.Dispose();
        }

        public delegate void ReciverHandler(LocalData localData);
        public static event ReciverHandler ReciveEvent;
    }
}
