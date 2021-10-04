using Codemasters.F1_2020;

namespace F1Tools
{
    public class TypeFactory
    {
        public static Packet GetPacket(byte[] bytes)
        {
            var type = CodemastersToolkit.GetPacketType(bytes);

            Packet pack = null;
            switch (type)
            {
                case PacketType.CarStatus:
                    pack = new CarStatusPacket();
                    break;
                case PacketType.CarTelemetry:
                    pack = new TelemetryPacket();
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
                case PacketType.Participants:
                    pack = new ParticipantPacket();
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
    }
}
