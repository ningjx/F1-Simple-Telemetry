using System;

namespace F1Tools
{
    public static class F1
    {


        public static bool GetPacket2019(byte[] bytes, out Codemasters.F1_2019.Packet packet)
        {
            packet = null;
            try
            {
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

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool GetPacket2020(byte[] bytes, out Codemasters.F1_2020.Packet packet)
        {
            packet = null;
            try
            {
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool GetPacket2021(byte[] bytes, out Codemasters.F1_2021.Packet packet)
        {
            packet = null;
            try
            {
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

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
