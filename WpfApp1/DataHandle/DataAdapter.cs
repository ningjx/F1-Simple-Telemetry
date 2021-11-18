using System;

namespace F1Tools
{
    internal static class DataAdapter
    {
        public static LocalData AsLocalData(this Codemasters.F1_2019.Packet packet)
        {
            if (packet == null)
                return null;

            var result = new LocalData();
            if (packet.PacketType == Codemasters.F1_2019.PacketType.CarTelemetry)
            {
                var curPack = packet as Codemasters.F1_2019.TelemetryPacket;
                var data = curPack.FieldTelemetryData[curPack.PlayerCarIndex];

                result.Brake = data.Brake;
                result.Throttle = data.Throttle;
                result.SpeedKph = data.SpeedKph;
                result.EngineRpm = data.EngineRpm;
                result.Gear = data.Gear;
                result.DrsActive = data.DrsActive;
            }
            else if (packet.PacketType == Codemasters.F1_2019.PacketType.CarStatus)
            {
                var curPack = packet as Codemasters.F1_2019.CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                result.DrsAllowed = data.DrsAllowed;
            }
            return result;
        }

        public static LocalData AsLocalData(this Codemasters.F1_2020.Packet packet)
        {
            if (packet == null)
                return null;

            var result = new LocalData();
            if (packet.PacketType == Codemasters.F1_2020.PacketType.CarTelemetry)
            {
                var curPack = packet as Codemasters.F1_2020.TelemetryPacket;
                var data = curPack.FieldTelemetryData[curPack.PlayerCarIndex];

                result.Brake = data.Brake;
                result.Throttle = data.Throttle;
                result.SpeedKph = data.SpeedKph;
                result.EngineRpm = data.EngineRpm;
                result.Gear = data.Gear;
                result.DrsActive = data.DrsActive;
            }
            else if (packet.PacketType == Codemasters.F1_2020.PacketType.CarStatus)
            {
                var curPack = packet as Codemasters.F1_2020.CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                result.DrsAllowed = data.DrsAllowed;
                result.DrsFailure = data.DrsFailure;
            }
            return result;
        }

        public static LocalData AsLocalData(this Codemasters.F1_2021.Packet packet)
        {
            if (packet == null)
                return null;

            var result = new LocalData();
            if (packet.PacketType == Codemasters.F1_2021.PacketType.CarTelemetry)
            {
                var curPack = packet as Codemasters.F1_2021.TelemetryPacket;
                var data = curPack.FieldTelemetryData[curPack.PlayerCarIndex];

                result.Brake = data.Brake;
                result.Throttle = data.Throttle;
                result.SpeedKph = data.SpeedKph;
                result.EngineRpm = data.EngineRpm;
                result.Gear = data.Gear;
                result.DrsActive = data.DrsActive;
            }
            else if (packet.PacketType == Codemasters.F1_2021.PacketType.CarStatus)
            {
                var curPack = packet as Codemasters.F1_2021.CarStatusPacket;
                var data = curPack.FieldCarStatusData[0];

                result.DrsAllowed = data.DrsAllowed;
            }
            return result;
        }

        public static LocalData AsLocalData(this float[] fh5Data)
        {
            var result = new LocalData
            {
                Throttle = fh5Data[80] / 255F,
                Brake = fh5Data[81] / 255F,
                Clutch = fh5Data[82] / 255F,
                Gear = (int)fh5Data[84],
                EngineRpm = fh5Data[4],
                SpeedKph = fh5Data[64] * 3.6F,
            };
            if (result.Gear == 0)
                result.Gear = -1;
            else if (result.Gear == 11)
                result.Gear = 0;
            return result;
        }
    }
}
