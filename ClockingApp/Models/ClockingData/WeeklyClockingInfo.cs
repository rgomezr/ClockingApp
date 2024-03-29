﻿using System;
using ClockingApp.Settings;

namespace ClockingApp.Models.ClockingData
{
	public class WeeklyClockingInfo
	{
        public IList<Clocking>? WeeklyClockings { get; set; }
		public double OvertimeHours { get; set; }
		public bool IsInvoiceView { get; set; } = false;
        public bool HasOvertime => (OvertimeHours > 0);
        public string OvertimeHours_formatted => String.Format("{0}{1}", this.OvertimeHours.ToString("##.##"), "h");
        public bool HasClockings => (WeeklyClockings != null && WeeklyClockings.Any());
		public int ClockingWeek => HasClockings
            ? WeeklyClockings.First().ClockingWeek : 0;
		public int ClockingYear => HasClockings
            ? WeeklyClockings.First().ClockingYear : 0;
        public string ClockingWeek_formatted => HasClockings ? String.Format("WEEK NO. {0}", this.ClockingWeek) : "";
		public string Username => HasClockings ? WeeklyClockings.First().Username : "";
		public double WorkingHours => HasClockings ? WeeklyClockings.Sum(clocking => clocking.WorkDay.Duration) : 0.0;
		public int WorkingDaysCount => HasClockings ? WeeklyClockings.Count() : 0;
		public string WorkingHours_formatted => String.Format("{0}{1}", this.WorkingHours.ToString("##.#"), "h");
		public double PaidWorkingHours => HasClockings ? WeeklyClockings.Sum(clocking => clocking.WorkingHoursPaid) : 0.0;
		public string PaidWorkingHours_formatted => String.Format("{0}{1}", this.PaidWorkingHours.ToString("##.##"), "h");

        /// <summary>
        /// Public Parametrised constructor
        /// </summary>
        /// <param name="weeklyClockings"></param>
        public WeeklyClockingInfo(IList<Clocking> weeklyClockings)
		{
			this.WeeklyClockings = weeklyClockings;
		}
	}
}

