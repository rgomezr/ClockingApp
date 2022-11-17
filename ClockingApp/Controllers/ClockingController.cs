using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;

namespace ClockingApp.Controllers
{
	public class ClockingController : Controller
	{
		private readonly ClockingService _clockingService;

		public ClockingController(ClockingService clockingService)
		{
			_clockingService = clockingService;
		}

        public async Task<IActionResult> GetClockingsForUsername(string username)
        {
            IList<Clocking> clockings = (await _clockingService._clockingRepo.FindAllAsync(clocking => clocking.Username.Equals(username))).ToList();
            return View(clockings);
        }

		public async Task<ActionResult> StartWork (string username)
		{
			DateTime currentDate = DateTime.Now;
			WorkDay workDay = new WorkDay(currentDate, null);
			Clocking clocking = new Clocking(username, ISOWeek.GetWeekOfYear(currentDate), currentDate.Date, workDay, null);
			await _clockingService._clockingRepo.InsertOneAsync(clocking);
			return Json(Url.Action("Index", "Home"));


        }


    }
}

