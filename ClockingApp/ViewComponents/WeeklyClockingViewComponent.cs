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
            weeklyClockingInfo.OvertimeHours = await CalculateOvertime(weeklyClockingInfo);
            return View(weeklyClockingInfo);
        }

        public async Task<double> CalculateOvertime (WeeklyClockingInfo weeklyClockingInfo)
        {
            double defaultWorkedHours = await CalculateExpectedWorkingHours(weeklyClockingInfo);
            double overtimeThreshold = Convert.ToDouble(_clockingSettings.OvertimeThresholdHours);
            double workedHours = weeklyClockingInfo.PaidWorkingHours;

            double hoursDifference = workedHours - defaultWorkedHours;

            return (hoursDifference >= overtimeThreshold) ? hoursDifference : 0;
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

