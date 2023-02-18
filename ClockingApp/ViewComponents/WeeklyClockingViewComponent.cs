using System;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.Models.ClockingData;

namespace ClockingApp.ViewComponents
{
	[ViewComponent]
	public class WeeklyClockingViewComponent : ViewComponent
	{
		public WeeklyClockingViewComponent()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync (WeeklyClockingInfo weeklyClockingInfo)
		{
			return View(weeklyClockingInfo);
		}
	}
}

