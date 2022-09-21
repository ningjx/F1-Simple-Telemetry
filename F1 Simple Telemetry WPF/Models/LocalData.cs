namespace F1Tools.Models
{
    public class LocalData
    {
        public bool? DrsFailure;
        public bool? DrsAllowed;
        public bool? DrsActive;

        public float? Throttle;
        public float? Brake;
        public float? Clutch;
        public float? HandBrake;
        public float? SpeedKph;
        public float? EngineRpm;
        public int? Gear;
        public GameVersion? GameVersion;
    }
}
