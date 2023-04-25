using Microsoft.Extensions.Logging;
using rNascar23.Sdk.Common;
using rNascar23.Sdk.LapTimes.Models;
using rNascar23.Sdk.LapTimes.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rNascar23.Sdk.Service.LapTimes.Adapters
{
    internal class MoversFallersService : IMoversFallersService
    {
        #region fields

        private readonly ILogger<MoversFallersService> _logger;

        #endregion

        #region properties

        public int NLapCount { get; set; } = 10;

        #endregion

        #region ctor

        public MoversFallersService(ILogger<MoversFallersService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region public

        public virtual IList<PositionChange> GetDriverPositionChanges(LapTimeData lapTimeData)
        {
            var positionChanges = new List<PositionChange>();

            try
            {
                var lastFlagStateChangeLap = GetLastFlagStateChange(lapTimeData.LapFlags);

                if (lastFlagStateChangeLap.HasValue)
                {
                    foreach (DriverLaps driverLaps in lapTimeData.Drivers)
                    {
                        var changeSinceFlagChange = lastFlagStateChangeLap.HasValue ?
                            GetDriverPositionChange(lastFlagStateChangeLap.Value, driverLaps.Laps) :
                            0;

                        var changeLast10Laps = lastFlagStateChangeLap.HasValue ?
                           GetDriverPositionChangeLastNLaps(NLapCount, driverLaps.Laps) :
                           0;

                        var positionChange = new PositionChange()
                        {
                            Driver = driverLaps.FullName,
                            ChangeSinceFlagChange = changeSinceFlagChange,
                            ChangeLastNLaps = changeLast10Laps
                        };

                        positionChanges.Add(positionChange);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
                positionChanges = new List<PositionChange>();
            }

            return positionChanges;
        }

        #endregion

        #region protected

        protected virtual int? GetLastFlagStateChange(IList<LapFlag> lapFlags)
        {
            if (lapFlags.Count == 0) return null;

            var orderedLapFlags = lapFlags.OrderByDescending(f => f.LapsCompleted);

            var currentLapFlag = orderedLapFlags.First();

            if (currentLapFlag.FlagState >= FlagColors.White)
            {
                // no event running
                LapFlag lastGreenLap = null;

                foreach (LapFlag lapFlag in orderedLapFlags)
                {
                    if (lapFlag.FlagState == FlagColors.Green)
                    {
                        lastGreenLap = lapFlag;
                        break;
                    }
                }

                if (lastGreenLap == null)
                    return null;

                foreach (LapFlag lapFlag in orderedLapFlags.Where(l => l.LapsCompleted < lastGreenLap.LapsCompleted))
                {
                    if (lapFlag.FlagState != FlagColors.Green)
                    {
                        return lapFlag.LapsCompleted;
                    }
                }

            }
            else if (currentLapFlag.FlagState == FlagColors.Green)
            {
                foreach (LapFlag lapFlag in orderedLapFlags)
                {
                    if (lapFlag.FlagState != FlagColors.Green)
                    {
                        return lapFlag.LapsCompleted;
                    }
                }
            }
            else if (currentLapFlag.FlagState == FlagColors.Yellow || currentLapFlag.FlagState == FlagColors.Yellow)
            {
                // yellow or red flag.
                // return change since last green lap
                foreach (LapFlag lapFlag in orderedLapFlags)
                {
                    if (lapFlag.FlagState != currentLapFlag.FlagState)
                    {
                        return lapFlag.LapsCompleted;
                    }
                }
            }

            return null;
        }

        protected virtual int GetDriverPositionChange(int lastFlagStateChangeLap, IList<LapDetails> laps)
        {
            if (laps.Count == 0)
                return 0;

            var lapsSinceFlagChange = laps.
                Where(l => l.Lap >= lastFlagStateChangeLap).
                OrderBy(l => l.Lap).
                ToList();

            if (lapsSinceFlagChange.Count == 0)
                return 0;

            var startLap = lapsSinceFlagChange.First();
            var endLap = lapsSinceFlagChange.Last();

            return startLap.RunningPosition - endLap.RunningPosition;
        }

        protected virtual int GetDriverPositionChangeLastNLaps(int lapCount, IList<LapDetails> laps)
        {
            if (laps.Count == 0)
                return 0;

            var lastNLaps = laps.
                OrderByDescending(l => l.Lap).
                Take(lapCount).
                ToList();

            if (lastNLaps.Count == 0)
                return 0;

            var startLap = lastNLaps.OrderBy(l => l.Lap).First();
            var endLap = lastNLaps.OrderBy(l => l.Lap).Last();

            return startLap.RunningPosition - endLap.RunningPosition;
        }

        protected virtual void ExceptionHandler(Exception ex)
        {
            ExceptionHandler(ex, String.Empty);
        }
        protected virtual void ExceptionHandler(Exception ex, string message = "")
        {
            string errorMessage = String.IsNullOrEmpty(message) ? ex.Message : message;

            _logger.LogError(ex, errorMessage);
        }

        #endregion
    }
}
