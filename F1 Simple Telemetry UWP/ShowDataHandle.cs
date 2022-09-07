﻿using static F1Tools.TypeFactory;

namespace F1Tools
{
    public static class ShowDataHandle
    {
        public static void F1Handle(F1Control f1, dynamic packet)
        {
            if (packet == null)
                return;

            switch (DataReciver.Version)
            {
                case GameVersion.F1_2019:
                    Handle19(f1, packet);
                    break;
                case GameVersion.F1_2020:
                    Handle20(f1, packet);
                    break;
                case GameVersion.F1_2021:
                    Handle21(f1, packet);
                    break;
                default:
                    break;
            }
        }

        private static void Handle19(F1Control f1, Codemasters.F1_2019.Packet packet)
        {
            if (packet.PacketType == Codemasters.F1_2019.PacketType.CarTelemetry)
            {
                var curPack = packet as Codemasters.F1_2019.TelemetryPacket;
                var data = curPack.FieldTelemetryData[curPack.PlayerCarIndex];

                f1.SetBrake(data.Brake);
                f1.SetThrottle(data.Throttle);
                f1.SetSpeed(data.SpeedKph);
                f1.SetRPM(data.EngineRpm);
                f1.SetGear(data.Gear);
                f1.SetDRS(data.DrsActive);
            }
            else if (packet.PacketType == Codemasters.F1_2019.PacketType.CarStatus)
            {
                var curPack = packet as Codemasters.F1_2019.CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                f1.DRSEnable(data.DrsAllowed);
                //f1.DRSNegative(data.DrsFailure);
            }
        }

        private static void Handle20(F1Control f1, Codemasters.F1_2020.Packet packet)
        {
            if (packet.PacketType == Codemasters.F1_2020.PacketType.CarTelemetry)
            {
                var curPack = packet as Codemasters.F1_2020.TelemetryPacket;
                var data = curPack.FieldTelemetryData[curPack.PlayerCarIndex];

                f1.SetBrake(data.Brake);
                f1.SetThrottle(data.Throttle);
                f1.SetSpeed(data.SpeedKph);
                f1.SetRPM(data.EngineRpm);
                f1.SetGear(data.Gear);
                f1.SetDRS(data.DrsActive);
            }
            else if (packet.PacketType == Codemasters.F1_2020.PacketType.CarStatus)
            {
                var curPack = packet as Codemasters.F1_2020.CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                f1.DRSEnable(data.DrsAllowed);
                f1.DRSNegative(data.DrsFailure);
            }
        }

        private static void Handle21(F1Control f1, Codemasters.F1_2021.Packet packet)
        {
            if (packet.PacketType == Codemasters.F1_2021.PacketType.CarTelemetry)
            {
                var curPack = packet as Codemasters.F1_2021.TelemetryPacket;
                var data = curPack.FieldTelemetryData[curPack.PlayerCarIndex];

                f1.SetBrake(data.Brake);
                f1.SetThrottle(data.Throttle);
                f1.SetSpeed(data.SpeedKph);
                f1.SetRPM(data.EngineRpm);
                f1.SetGear(data.Gear);
                f1.SetDRS(data.DrsActive);
            }
            else if (packet.PacketType == Codemasters.F1_2021.PacketType.CarStatus)
            {
                var curPack = packet as Codemasters.F1_2021.CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                f1.DRSEnable(data.DrsAllowed);
                //f1.DRSNegative(data.DrsFailure);
            }
        }
    }
}
