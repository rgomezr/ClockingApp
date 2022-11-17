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

		public async Task<JsonResult> StartWork (string username)
		{
			DateTime currentDate = DateTime.Now.Date;
			WorkDay workDay = new WorkDay(currentDate, null);
			Clocking clocking = new Clocking(username, ISOWeek.GetWeekOfYear(currentDate), currentDate, workDay, null);
			await _clockingService._clockingRepo.InsertOneAsync(clocking);
			//return JsonResult(Url.Action("Index", "Home"));
			return null;


        }


    }
}

