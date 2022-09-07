using Codemasters.F1_2019;
using Codemasters.F1_2020;
using Codemasters.F1_2021;

namespace F1Tools
{
    public class TypeFactory
    {
        public static object GetPacket(byte[] bytes, GameVersion version, out GameVersion outVersion)
        {
            if (version != GameVersion.Unkonwm)
            {
                outVersion = version;
                switch (version)
                {
                    case GameVersion.F1_2019:
                        return GetPacket2019(bytes);
                    case GameVersion.F1_2020:
                        return GetPacket2020(bytes);
                    case GameVersion.F1_2021:
                        return GetPacket2021(bytes);
                    default: return null;
                }
            }
            else
            {
                object pack = null;
                try
                {
                    pack = GetPacket2019(bytes);
                    outVersion = GameVersion.F1_2019;
                    return pack;
                }
                catch { }
                try
                {
                    pack = GetPacket2020(bytes);
                    outVersion = GameVersion.F1_2020;
                    return pack;
                }
                catch { }
                try
                {
                    pack = GetPacket2021(bytes);
                    outVersion = GameVersion.F1_2021;
                    return pack;
                }
                catch { }
            }
            outVersion = GameVersion.Unkonwm;
            return null;
        }

        public static Codemasters.F1_2019.Packet GetPacket2019(byte[] bytes)
        {
            var type = Codemasters.F1_2019.CodemastersToolkit.GetPacketType(bytes);

            Codemasters.F1_2019.Packet pack = null;
            switch (type)
            {
                case Codemasters.F1_2019.PacketType.CarStatus:
                    pack = new Codemasters.F1_2019.CarStatusPacket();
                    break;
                case Codemasters.F1_2019.PacketType.CarTelemetry:
                    pack = new Codemasters.F1_2019.TelemetryPacket();
                    break;
                //case PacketType.FinalClassification:
                //    pack = new FinalClassificationPacket();
                //    break;
                //case PacketType.Lap:
                //    pack = new LapPacket();
                //    break;
                //case PacketType.LobbyInfo:
                //    pack = new LobbyInfoPacket();
                //    break;
                //case PacketType.Motion:
                //    pack = new MotionPacket();
                //    break;
                case Codemasters.F1_2019.PacketType.Participants:
                    pack = new Codemasters.F1_2019.ParticipantPacket();
                    break;
                //case PacketType.Session:
                //    pack = new SessionPacket();
                //    break;
                default: break;
            }

            if (pack != null)
                pack.LoadBytes(bytes);

            return pack;
        }

        public static Codemasters.F1_2020.Packet GetPacket2020(byte[] bytes)
        {
            var type = Codemasters.F1_2020.CodemastersToolkit.GetPacketType(bytes);

            Codemasters.F1_2020.Packet pack = null;
            switch (type)
            {
                case Codemasters.F1_2020.PacketType.CarStatus:
                    pack = new Codemasters.F1_2020.CarStatusPacket();
                    break;
                case Codemasters.F1_2020.PacketType.CarTelemetry:
                    pack = new Codemasters.F1_2020.TelemetryPacket();
                    break;
                //case PacketType.FinalClassification:
                //    pack = new FinalClassificationPacket();
                //    break;
                //case PacketType.Lap:
                //    pack = new LapPacket();
                //    break;
                //case PacketType.LobbyInfo:
                //    pack = new LobbyInfoPacket();
                //    break;
                //case PacketType.Motion:
                //    pack = new MotionPacket();
                //    break;
                case Codemasters.F1_2020.PacketType.Participants:
                    pack = new Codemasters.F1_2020.ParticipantPacket();
                    break;
                //case PacketType.Session:
                //    pack = new SessionPacket();
                //    break;
                default: break;
            }

            if (pack != null)
                pack.LoadBytes(bytes);

            return pack;
        }

        public static Codemasters.F1_2021.Packet GetPacket2021(byte[] bytes)
        {
            var type = Codemasters.F1_2021.CodemastersToolkit.GetPacketType(bytes);

            Codemasters.F1_2021.Packet pack = null;
            switch (type)
            {
                case Codemasters.F1_2021.PacketType.CarStatus:
                    pack = new Codemasters.F1_2021.CarStatusPacket();
                    break;
                case Codemasters.F1_2021.PacketType.CarTelemetry:
                    pack = new Codemasters.F1_2021.TelemetryPacket();
                    break;
                //case PacketType.FinalClassification:
                //    pack = new FinalClassificationPacket();
                //    break;
                //case PacketType.Lap:
                //    pack = new LapPacket();
                //    break;
                //case PacketType.LobbyInfo:
                //    pack = new LobbyInfoPacket();
                //    break;
                //case PacketType.Motion:
                //    pack = new MotionPacket();
                //    break;
                case Codemasters.F1_2021.PacketType.Participants:
                    pack = new Codemasters.F1_2021.ParticipantPacket();
                    break;
                //case PacketType.Session:
                //    pack = new SessionPacket();
                //    break;
                default: break;
            }

            if (pack != null)
                pack.LoadBytes(bytes);

            return pack;
        }

        public enum GameVersion
        {
            F1_2019, F1_2020, F1_2021, Unkonwm
        }
    }
}
