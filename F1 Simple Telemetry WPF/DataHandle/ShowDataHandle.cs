using F1Tools.Models;

namespace F1Tools
{
    public static class ShowDataHandle
    {
        public static void F1Handle(F1Instrument f1, LocalData data)
        {
            if (data == null)
                return;
            if (data.Brake.HasValue)
                f1.SetBrake(data.Brake.Value);
            if (data.Throttle.HasValue)
                f1.SetThrottle(data.Throttle.Value);
            if (data.SpeedKph.HasValue)
                f1.SetSpeed((int)data.SpeedKph.Value);
            if (data.EngineRpm.HasValue)
                f1.SetRPM((int)data.EngineRpm.Value);
            if (data.Gear.HasValue)
                f1.SetGear(data.Gear.Value);
            if (data.DrsActive.HasValue)
                f1.SetDRS(data.DrsActive.Value);
            if (data.DrsAllowed.HasValue)
                f1.DRSEnable(data.DrsAllowed.Value);
            if (data.DrsFailure.HasValue)
                f1.DRSNegative(data.DrsFailure.Value);
        }
    }
}
