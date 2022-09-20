using System.Collections.Generic;

namespace F1Tools.Models
{
    public class FH5Unit
    {
        public int Size;
        public string Type;
        public string Name;

        public FH5Unit(int size, string type, string name)
        {
            Size = size;
            Type = type;
            Name = name;
        }
    }


    public class FH5Format : List<FH5Unit>
    {

        public static FH5Format FH5FormatData = new FH5Format
        {
            new FH5Unit(4,"Int32","IsRaceOn"),// = 1 when race is on. = 0 when in menus/race stopped
            new FH5Unit(4,"Int32","TimestampMS"),//Can overflow to 0 eventually
            new FH5Unit(4,"float","EngineMaxRpm"),
            new FH5Unit(4,"float","EngineIdleRpm"),
            new FH5Unit(4,"float","CurrentEngineRpm"),
            new FH5Unit(4,"float","AccelerationX"),//In the car's local space; X = right, Y = up, Z = forward
            new FH5Unit(4,"float","AccelerationY"),
            new FH5Unit(4,"float","AccelerationZ"),
            new FH5Unit(4,"float","VelocityX"),
            new FH5Unit(4,"float","VelocityY"),
            new FH5Unit(4,"float","VelocityZ"),
            new FH5Unit(4,"float","AngularVelocityX"),
            new FH5Unit(4,"float","AngularVelocityY"),
            new FH5Unit(4,"float","AngularVelocityZ"),
            new FH5Unit(4,"float","Yaw"),
            new FH5Unit(4,"float","Pitch"),
            new FH5Unit(4,"float","Roll"),
            new FH5Unit(4,"float","NormalizedSuspensionTravelFrontLeft"),// Suspension travel normalized: 0.0f = max stretch; 1.0 = max compression
            new FH5Unit(4,"float","NormalizedSuspensionTravelFrontRight"),
            new FH5Unit(4,"float","NormalizedSuspensionTravelRearLeft"),
            new FH5Unit(4,"float","NormalizedSuspensionTravelRearRight"),
            new FH5Unit(4,"float","TireSlipRatioFrontLeft"),// Tire normalized slip ratio, = 0 means 100% grip and |ratio| > 1.0 means loss of grip. THis is longitudinal slip or just spin
            new FH5Unit(4,"float","TireSlipRatioFrontRight"),
            new FH5Unit(4,"float","TireSlipRatioRearLeft"),
            new FH5Unit(4,"float","TireSlipRatioRearRight"),
            new FH5Unit(4,"float","WheelRotationSpeedFrontLeft"),// Wheel rotation speed radians/sec.
            new FH5Unit(4,"float","WheelRotationSpeedFrontRight"),
            new FH5Unit(4,"float","WheelRotationSpeedRearLeft"),
            new FH5Unit(4,"float","WheelRotationSpeedRearRight"),
            new FH5Unit(4,"bool","WheelOnRumbleStripFrontLeft"),// = 1 when wheel is on rumble strip, = 0 when off.
            new FH5Unit(4,"bool","WheelOnRumbleStripFrontRight"),
            new FH5Unit(4,"bool","WheelOnRumbleStripRearLeft"),
            new FH5Unit(4,"bool","WheelOnRumbleStripRearRight"),
            new FH5Unit(4,"bool","WheelInPuddleDepthFrontLeft"),// = from 0 to 1, where 1 is the deepest puddle
            new FH5Unit(4,"bool","WheelInPuddleDepthFrontRight"),
            new FH5Unit(4,"bool","WheelInPuddleDepthRearLeft"),
            new FH5Unit(4,"bool","WheelInPuddleDepthRearRight"),
            new FH5Unit(4,"float","SurfaceRumbleFrontLeft"),// Non-dimensional surface rumble values passed to controller force feedback
            new FH5Unit(4,"float","SurfaceRumbleFrontRight"),
            new FH5Unit(4,"float","SurfaceRumbleRearLeft"),
            new FH5Unit(4,"float","SurfaceRumbleRearRight"),
            new FH5Unit(4,"float","TireSlipAngleFrontLeft"),// Tire normalized slip angle, = 0 means 100% grip and |angle| > 1.0 means loss of grip. This is lateral tire slip angle (not the same as wheel angle)
            new FH5Unit(4,"float","TireSlipAngleFrontRight"),
            new FH5Unit(4,"float","TireSlipAngleRearLeft"),
            new FH5Unit(4,"float","TireSlipAngleRearRight"),
            new FH5Unit(4,"float","TireCombinedSlipFrontLeft"),// Tire normalized combined slip, = 0 means 100% grip and |slip| > 1.0 means loss of grip. This is a combination of TireSlipRatioFrontLeft and TireSlipAngleFrontLeft.
            new FH5Unit(4,"float","TireCombinedSlipFrontRight"),
            new FH5Unit(4,"float","TireCombinedSlipRearLeft"),
            new FH5Unit(4,"float","TireCombinedSlipRearRight"),
            new FH5Unit(4,"float","SuspensionTravelMetersFrontLeft"),// Actual suspension travel in meters
            new FH5Unit(4,"float","SuspensionTravelMetersFrontRight"),
            new FH5Unit(4,"float","SuspensionTravelMetersRearLeft"),
            new FH5Unit(4,"float","SuspensionTravelMetersRearRight"),
            new FH5Unit(4,"Int32","CarOrdinal"),//Unique ID of the car make/model
            new FH5Unit(4,"Int32","CarClass"),//Between 0 (D -- worst cars) and 7 (X class -- best cars) inclusive
            new FH5Unit(4,"Int32","CarPerformanceIndex"),//Between 100 (slowest car) and 999 (fastest car) inclusive
            new FH5Unit(4,"Int32","DrivetrainType"),//Corresponds to EDrivetrainType; 0 = FWD, 1 = RWD, 2 = AWD
            new FH5Unit(4,"Int32","NumCylinders"),//Number of cylinders in the engine
            new FH5Unit(4,"Int32","CarCategory"),
            new FH5Unit(4,"Int32","unknown"),// > 0 when crashing into objects
            new FH5Unit(4,"Int32","unknown"),// > 0 when crashing into objects
            new FH5Unit(4,"float","PositionX"),
            new FH5Unit(4,"float","PositionY"),
            new FH5Unit(4,"float","PositionZ"),
            new FH5Unit(4,"float","Speed"),// in meters per second
            new FH5Unit(4,"float","Power"),// in watts
            new FH5Unit(4,"float","Torque"),// newton meter
            new FH5Unit(4,"float","TireTemperatureFrontLeft"),// °F
            new FH5Unit(4,"float","TireTemperatureFrontRight"),
            new FH5Unit(4,"float","TireTemperatureRearLeft"),
            new FH5Unit(4,"float","TireTemperatureRearRight"),
            new FH5Unit(4,"float","Boost"),
            new FH5Unit(4,"float","Fuel"),
            new FH5Unit(4,"float","DistanceTraveled"),
            new FH5Unit(4,"float","BestLap"),
            new FH5Unit(4,"float","LastLap"),
            new FH5Unit(4,"float","CurrentLap"),
            new FH5Unit(4,"float","CurrentRaceTime"),
            new FH5Unit(2,"UInt16","LapNumber"),
            new FH5Unit(1,"uint8","RacePosition"),
            new FH5Unit(1,"uint8","Throttle"),// /255
            new FH5Unit(1,"uint8","Brake"),// /255
            new FH5Unit(1,"uint8","Clutch"),// /25// /2555
            new FH5Unit(1,"uint8","HandBrake"),
            new FH5Unit(1,"uint8","Gear"),
            new FH5Unit(1,"uint8","Steer"),// /255
            new FH5Unit(1,"uint8","NormalizedDrivingLine"),
            new FH5Unit(1,"uint8","NormalizedAIBrakeDifference")
        };
    }
}
