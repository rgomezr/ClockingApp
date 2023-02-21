using System;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.Models.ClockingData;
using ClockingApp.Settings;

namespace ClockingApp.ViewComponents
{
    [ViewComponent]
    public class WeeklyClockingViewComponent : ViewComponent
    {
        private readonly IClockingSettings _clockingSettings;

        public WeeklyClockingViewComponent(IClockingSettings clockingSettings)
        {
            this._clockingSettings = clockingSettings;
        }

        public async Task<IViewComponentResult> InvokeAsync(WeeklyClockingInfo weeklyClockingInfo)
        {
            weeklyClockingInfo.DefaultWorkedHours = await CalculateExpectedWorkingHours(weeklyClockingInfo);
            return View(weeklyClockingInfo);
        }

        /// <summary>
        /// Calculates default Working Hours for the clockings that
        /// have been registered for corresponding week.
        /// </summary>
        /// <param name="weeklyClockingInfo"></param>
        /// <returns>Double representing hours meant to have worked</returns>
        public async Task<double> CalculateExpectedWorkingHours(WeeklyClockingInfo weeklyClockingInfo)
        {
            //TODO: Run below as Async Task.Run(...)
            if (weeklyClockingInfo.WeeklyClockings != null)
            {
                double weeklyDefaultHours = 0;
                foreach (Clocking clocking in weeklyClockingInfo.WeeklyClockings)
                {
                    weeklyDefaultHours += _clockingSettings.GetDayOfWeekHours(clocking.ClockingDate.DayOfWeek.ToString());
                }
                return weeklyDefaultHours;
            } else
            {
                return 0;
            }
        }
    }
}

