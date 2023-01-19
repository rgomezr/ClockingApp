using System;
using System.Globalization;
using ClockingApp.Models.ClockingData;
namespace ClockingApp.Models.CustomViewModels
{
    public class WeeksDailyClocking
    {
        public static DateTime CurrentDate => DateTime.Now.Date;
        public static int CurrentWeek => ISOWeek.GetWeekOfYear(CurrentDate);
        public List<Clocking> WeeksClocking { get; set; } = null!;
        public Clocking? DayClocking => WeeksClocking.Where(clocking => clocking.ClockingDate.Equals(CurrentDate)).FirstOrDefault();

        public WeeksDailyClocking(List<Clocking> weeksClocking)
        {
            WeeksClocking = weeksClocking;
        }
    }
}

