
using NingSoft.F1TelemetryAdapter;

namespace F1Tools
{
    public static class F1
    {
        public static Codemasters.F1_2019.Packet GetPacket2019(byte[] bytes)
        {
            Codemasters.F1_2019.Packet packet = null;
            var type = Codemasters.F1_2019.CodemastersToolkit.GetPacketType(bytes);

            switch (type)
            {
                case Codemasters.F1_2019.PacketType.CarStatus:
                    packet = new Codemasters.F1_2019.CarStatusPacket();
                    break;
                case Codemasters.F1_2019.PacketType.CarTelemetry:
                    packet = new Codemasters.F1_2019.TelemetryPacket();
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
                //case Codemasters.F1_2019.PacketType.Participants:
                //    packet = new Codemasters.F1_2019.ParticipantPacket();
                //    break;
                //case Codemasters.F1_2019.PacketType.Session:
                //    pack = new SessionPacket();
                //    break;
                default: break;
            }

            if (packet != null)
                packet.LoadBytes(bytes);
            return packet;
        }

        public static Codemasters.F1_2020.Packet GetPacket2020(byte[] bytes)
        {
            Codemasters.F1_2020.Packet packet = null;
            var type = Codemasters.F1_2020.CodemastersToolkit.GetPacketType(bytes);

            switch (type)
            {
                case Codemasters.F1_2020.PacketType.CarStatus:
                    packet = new Codemasters.F1_2020.CarStatusPacket();
                    break;
                case Codemasters.F1_2020.PacketType.CarTelemetry:
                    packet = new Codemasters.F1_2020.TelemetryPacket();
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
                //case Codemasters.F1_2020.PacketType.Participants:
                //    packet = new Codemasters.F1_2020.ParticipantPacket();
                //    break;
                //case PacketType.Session:
                //    pack = new SessionPacket();
                //    break;
                default: break;
            }

            if (packet != null)
                packet.LoadBytes(bytes);
            return packet;

        }

        public static Codemasters.F1_2021.Packet GetPacket2021(byte[] bytes)
        {
            Codemasters.F1_2021.Packet packet = null;

            var type = Codemasters.F1_2021.CodemastersToolkit.GetPacketType(bytes);

            switch (type)
            {
                case Codemasters.F1_2021.PacketType.CarStatus:
                    packet = new Codemasters.F1_2021.CarStatusPacket();
                    break;
                case Codemasters.F1_2021.PacketType.CarTelemetry:
                    packet = new Codemasters.F1_2021.TelemetryPacket();
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
                //case Codemasters.F1_2021.PacketType.Participants:
                //    packet = new Codemasters.F1_2021.ParticipantPacket();
                //    break;
                //case PacketType.Session:
                //    pack = new SessionPacket();
                //    break;
                default: break;
            }

            if (packet != null)
                packet.LoadBytes(bytes);

            return packet;
        }

        public static F1Packet GetPacket2022(byte[] bytes)
        {
            return F1Adapter.GetF1Packet(bytes);
        }
    }
}
