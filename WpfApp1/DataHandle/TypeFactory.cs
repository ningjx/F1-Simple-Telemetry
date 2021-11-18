using System;

namespace F1Tools
{
    public class TypeFactory
    {
        public static LocalData GetData(byte[] bytes, GameVersion version, out GameVersion outVersion)
        {
            if (version != GameVersion.Unkonwn)
            {
                switch (version)
                {
                    case GameVersion.F1_2019:
                        outVersion = F1.GetPacket2019(bytes, out Codemasters.F1_2019.Packet packet19) ? GameVersion.F1_2019 : GameVersion.Unkonwn;
                        return packet19.AsLocalData();
                    case GameVersion.F1_2020:
                        outVersion = F1.GetPacket2020(bytes, out Codemasters.F1_2020.Packet packet20) ? GameVersion.F1_2020 : GameVersion.Unkonwn;
                        return packet20.AsLocalData();
                    case GameVersion.F1_2021:
                        outVersion = F1.GetPacket2021(bytes, out Codemasters.F1_2021.Packet packet21) ? GameVersion.F1_2021 : GameVersion.Unkonwn;
                        return packet21.AsLocalData();
                    case GameVersion.Horizon5:
                        outVersion = FH5.GetFh5Data(bytes, out float[] data) ? GameVersion.Horizon5 : GameVersion.Unkonwn;
                        return data.AsLocalData();
                    default:
                        outVersion = GameVersion.Unkonwn;
                        return null;
                }
            }
            else
            {
                outVersion = Helper.GetGameVersion(bytes);
                return null;
            }
        }
    }
}
