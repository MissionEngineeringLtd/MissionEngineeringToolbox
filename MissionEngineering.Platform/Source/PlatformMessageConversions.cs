using MissionEngineering.Simulation.Messages;

namespace MissionEngineering.Platform;

public static class PlatformMessageConversions
{
    public static PlatformStateMessage ConvertToPlatformStateMessage(PlatformState platformState)
    {
        var header = new SimulationMessageHeader
        {
            WallClockDateTime = DateTime.UtcNow,
            SimulationDateTime = platformState.TimeStamp.SimulationDateTime,
            SimulationTime_s = platformState.TimeStamp.SimulationTime_s,
            SourceId = platformState.PlatformId,
            SourceName = platformState.PlatformName,
            MessageId = 0,
            MessageDescription = "Platform State Message",
        };

        var platformStateMessage = new PlatformStateMessage
        {
            Header = header,
            PlatformId = platformState.PlatformId,
            PlatformName = platformState.PlatformName,
            IsPrediction = platformState.IsPrediction,
            LastUpdateTime_s = platformState.LastUpdateTime_s,
            PredictionTime_s = platformState.PredictionTime_s,
            PredictionTimeDelta_s = platformState.PredictionTimeDelta_s,
            Latitude_deg = platformState.PositionLLA.Latitude_deg,
            Longitude_deg = platformState.PositionLLA.Longitude_deg,
            Altitude_m = platformState.PositionLLA.Altitude_m,
            PositionNorth_m = platformState.PositionNED.PositionNorth_m,
            PositionEast_m = platformState.PositionNED.PositionEast_m,
            PositionDown_m = platformState.PositionNED.PositionDown_m,
            VelocityNorth_ms = platformState.VelocityNED.VelocityNorth_ms,
            VelocityEast_ms = platformState.VelocityNED.VelocityEast_ms,
            VelocityDown_ms = platformState.VelocityNED.VelocityDown_ms,
            AccelerationNorth_ms2 = platformState.AccelerationNED.AccelerationNorth_ms2,
            AccelerationEast_ms2 = platformState.AccelerationNED.AccelerationEast_ms2,
            AccelerationDown_ms2 = platformState.AccelerationNED.AccelerationDown_ms2,
            AccelerationAxial_ms2 = platformState.AccelerationTBA.AccelerationAxial_ms2,
            AccelerationLateral_ms2 = platformState.AccelerationTBA.AccelerationLateral_ms2,
            AccelerationVertical_ms2 = platformState.AccelerationTBA.AccelerationVertical_ms2,
            HeadingAngle_deg = platformState.Attitude.HeadingAngle_deg,
            PitchAngle_deg = platformState.Attitude.PitchAngle_deg,
            BankAngle_deg = platformState.Attitude.BankAngle_deg,
            HeadingRate_deg = platformState.AttitudeRate.HeadingRate_degs,
            PitchRate_deg = platformState.AttitudeRate.PitchRate_degs,
            BankRate_deg = platformState.AttitudeRate.BankRate_degs,
            IsActive = platformState.IsActive,
            IsDestroyed = platformState.IsDestroyed,
        };

        return platformStateMessage;
    }

    public static PlatformStateRelativeMessage ConvertToPlatformStateRelativeMessage(PlatformStateRelative platformStateRelative)
    {
        var header = new SimulationMessageHeader
        {
            WallClockDateTime = DateTime.UtcNow,
            SimulationDateTime = platformStateRelative.TimeStamp.SimulationDateTime,
            SimulationTime_s = platformStateRelative.TimeStamp.SimulationTime_s,
            SourceId = platformStateRelative.PlatformIdOrigin,
            SourceName = platformStateRelative.PlatformNameOrigin,
            MessageId = 0,
            MessageDescription = "Relative Platform State Message",
        };

        var platformStateRelativeMessage = new PlatformStateRelativeMessage
        {
            Header = header,
            PlatformIdOrigin = platformStateRelative.PlatformIdOrigin,
            PlatformNameOrigin = platformStateRelative.PlatformNameOrigin,
            PlatformIdTarget = platformStateRelative.PlatformIdTarget,
            PlatformNameTarget = platformStateRelative.PlatformNameTarget,
            RelativePositionNorth_m = platformStateRelative.RelativePositionNED.PositionNorth_m,
            RelativePositionEast_m = platformStateRelative.RelativePositionNED.PositionEast_m,
            RelativePositionDown_m = platformStateRelative.RelativePositionNED.PositionDown_m,
            RelativeVelocityNorth_ms = platformStateRelative.RelativeVelocityNED.VelocityNorth_ms,
            RelativeVelocityEast_ms = platformStateRelative.RelativeVelocityNED.VelocityEast_ms,
            RelativeVelocityDown_ms = platformStateRelative.RelativeVelocityNED.VelocityDown_ms,
            RelativePositionLOSX_m = platformStateRelative.RelativePositionLOS.PositionNorth_m,
            RelativePositionLOSY_m = platformStateRelative.RelativePositionLOS.PositionEast_m,
            RelativePositionLOSZ_m = platformStateRelative.RelativePositionLOS.PositionDown_m,
            RelativeVelocityLOSX = platformStateRelative.RelativeVelocityLOS.VelocityNorth_ms,
            RelativeVelocityLOSY = platformStateRelative.RelativeVelocityLOS.VelocityEast_ms,
            RelativeVelocityLOSZ = platformStateRelative.RelativeVelocityLOS.VelocityDown_ms,
            Range_m = platformStateRelative.RelativePolarsNED.Range_m,
            RangeRate_ms = platformStateRelative.RelativePolarsNED.RangeRate_ms,
            AzimuthAngle_deg = platformStateRelative.RelativePolarsNED.AzimuthAngle_deg,
            ElevationAngle_deg = platformStateRelative.RelativePolarsNED.ElevationAngle_deg,
            AzimuthRate_degs = platformStateRelative.RelativePolarsNED.AzimuthRate_degs,
            ElevationRate_degs = platformStateRelative.RelativePolarsNED.ElevationRate_degs,
            AspectAngleAzimuth_deg = platformStateRelative.AspectAngleAzimuth_deg,
            AspectAngleElevation_deg = platformStateRelative.AspectAngleElevation_deg
        };

        return platformStateRelativeMessage;
    }
}