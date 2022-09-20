using NingSoft.F1TelemetryAdapter;
using NingSoft.F1TelemetryAdapter.Enums;

namespace F1Tools
{
    internal static class DataAdapter
    {
        public static LocalData AsLocalData(this F1Packet packet)
        {
            if (packet == null)
                return null;

            var result = new LocalData();
            if (packet.Header._PacketType == PacketType.CarTelemetry)
            {
                var curPack = packet as dynamic;
                var data = curPack.CarTelemetryData[curPack.Header.PlayerCarIndex];

                result.Brake = data.Brake;
                result.Throttle = data.Throttle;
                result.SpeedKph = data.Speed;
                result.EngineRpm = data.EngineRPM;
                result.Gear = data.Gear;
                result.DrsActive = data.Drs == 1;
            }
            else if (packet.Header._PacketType == PacketType.CarStatus)
            {
                var curPack = packet as dynamic;
                var data = curPack.CarStatusDatas[curPack.Header.PlayerCarIndex];

                result.DrsAllowed = data.DrsAllowed == 1;
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
