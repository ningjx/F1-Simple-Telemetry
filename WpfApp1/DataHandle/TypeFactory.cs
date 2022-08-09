using System;
using System.IO;
using System.Linq;

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
                    case 2019:
                        outVersion = GameVersion.F1_2019;
                        return F1.GetPacket2019(bytes).AsLocalData();

                    case 2020:
                        outVersion = GameVersion.F1_2020;
                        return F1.GetPacket2020(bytes).AsLocalData();

                    case 2021:
                        outVersion = GameVersion.F1_2021;
                        return F1.GetPacket2021(bytes).AsLocalData();

                    case 2022:
                        outVersion = GameVersion.F1_22;
                        return F1.GetPacket2022(bytes).AsLocalData();
                    default:
                        outVersion = GameVersion.Horizon5;
                        return FH5.GetFh5Data(bytes).AsLocalData();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
