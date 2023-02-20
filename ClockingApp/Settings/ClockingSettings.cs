using ClockingApp.Models.CustomSettings;

namespace ClockingApp.Settings
{
    public class ClockingSettings : IClockingSettings
    {
        public string PaidBreakTime { get; set; }
        public string OvertimeThresholdHours { get; set; }
        public WeekDayHours[] WeeklyDefaultHours { get; set; }

    }
}