using NingSoft.F1TelemetryAdapter;
using System;
using System.IO;

namespace F1Tools
{
    public class TypeFactory
    {
        public static LocalData GetData(byte[] bytes, out GameVersion outVersion)
        {
            outVersion = GameVersion.Unkonwn;
            try
            {
                var ver = BitConverter.ToUInt16(bytes, 0);

                switch (ver)
                {
                    case 2018:
                    case 2019:
                    case 2020:
                    case 2021:
                    case 2022:
                        outVersion = (GameVersion)ver;
                        return F1Adapter.GetF1Packet(bytes).AsLocalData();

                    default:
                        outVersion = GameVersion.Horizon5;
                        return FH5.GetFh5Data(bytes).AsLocalData();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                File.WriteAllText(@"D:\errlog.txt", string.Join(",", bytes));
#endif
                return null;
            }
        }
    }
}
