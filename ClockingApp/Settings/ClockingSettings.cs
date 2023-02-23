using ClockingApp.Models.CustomSettings;

namespace ClockingApp.Settings
{
    public class ClockingSettings : IClockingSettings
    {
        public string PaidBreakTime { get; set; }
        public string OvertimeThresholdHours { get; set; }
        public WeekDayHours[] WeeklyDefaultHours { get; set; }

        public double GetDayOfWeekHours(string dayOfWeek)
        {
            WeekDayHours? weekDayHours = this.WeeklyDefaultHours
                                        .Where(weeklyDay => weeklyDay.WeekDay == dayOfWeek)
                                        .FirstOrDefault();
            if (weekDayHours != null)
            {
                return Convert.ToDouble(weekDayHours.DayHours);
            } else
            {
                return 0;
            }
        }

    }
}