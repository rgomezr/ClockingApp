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
            return View(weeklyClockingInfo);
        }

        public void CalculateExpectedWorkingHours(WeeklyClockingInfo weeklyClockingInfo)
        {
        }
    }
}

