using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;
using ClockingApp.Settings;

namespace ClockingApp.Controllers
{
    public class ClockingController : Controller
    {
        private readonly ClockingService _clockingService;
        private readonly IUserSettings _userSettings;
        private readonly IClockingSettings _clockingSettings;

        public ClockingController(ClockingService clockingService, IUserSettings userSettings, IClockingSettings clockingSettings)
        {
            _clockingService = clockingService;
            _userSettings = userSettings;
            _clockingSettings = clockingSettings;
        }

        public async Task<IActionResult> GetClockingsForUsername()
        {
            IList<Clocking> clockings = (await _clockingService._clockingRepo
                                        .FindAllAsync(clocking => clocking.Username.Equals(_userSettings.Username)))
                                        .ToList();
            return View(clockings);
        }

        [HttpPost]
        public async Task<ActionResult> StartWork()
        {
            DateTime currentDate = DateTime.Now;
            WorkDay workDay = new WorkDay(currentDate, null);
            Clocking clocking = new Clocking(_userSettings.Username, ISOWeek.GetWeekOfYear(currentDate), currentDate.Date, workDay, null);
            await _clockingService._clockingRepo.InsertOneAsync(clocking);
            return Json(Url.Action("Index", "Home"));


        }

        [HttpPost]
        public async Task<ActionResult> FinishWork([FromBody] string clockingId)
        {
            Clocking clocking = await RetrieveClockingById(clockingId);
            if (clocking != null)
            {
                try
                {
                    DateTime currentDate = DateTime.Now;
                    clocking.WorkDay.EndDate = currentDate;
                    await _clockingService._clockingRepo.FindOneAndReplaceAsync(clocking => clocking.ClockingDate == currentDate.Date, clocking);
                    return Json(Url.Action("Index", "Home"));
                }
                catch (Exception ex)
                {
                    //TODO: Return appropiate Bad Request with exception message back
                    return Json(String.Format("An error occurred while updating clocking: {0}", ex.Message));
                }

            }
            else
            {
                // TODO: Return appropiate Bad Request with message back
                return Json(String.Format("Clocking with id {0} could not be found in db", clockingId));
            }
        }

        [HttpPost]
        public async Task<ActionResult> StartBreak([FromBody] string clockingId)
        {

            Clocking clocking = await RetrieveClockingById(clockingId);
            if (clocking != null)
            {
                try
                {
                    DateTime currentDate = DateTime.Now;
                    BreakDay breakDay = new BreakDay(currentDate, null);
                    clocking.AddToBreakList(breakDay);
                    await _clockingService._clockingRepo.FindOneAndReplaceAsync(clocking => clocking.ClockingDate == currentDate.Date, clocking);
                    return Json(Url.Action("Index", "Home"));
                }
                catch (Exception ex)
                {
                    //TODO: Return appropiate Bad Request with exception message back
                    return Json(String.Format("An error occurred while updating clocking: {0}", ex.Message));
                }

            }
            else
            {
                // TODO: Return appropiate Bad Request with message back
                return Json(String.Format("Clocking with id {0} could not be found in db", clockingId));
            }
        }

        [HttpPost]
        public async Task<ActionResult> FinishBreak([FromBody] string clockingId)
        {
            Clocking clocking = await RetrieveClockingById(clockingId);
            if (clocking != null)
            {
                DateTime currentDate = DateTime.Now.Date;
                clocking.FinishActiveBreak();
                await _clockingService._clockingRepo.FindOneAndReplaceAsync(clocking => clocking.ClockingDate == currentDate.Date, clocking);
                return Json(Url.Action("Index", "Home"));
            }
            else
            {
                // TODO: Return appropiate Bad Request with message back
                return Json(String.Format("Clocking with id {0} could not be found in db", clockingId));
            }
        }

        public async Task<ActionResult> GetAllClockingsForUserAndWeek(DateTime weekDate)
        {
            int weekNumber = ISOWeek.GetWeekOfYear(weekDate);
            IList<Clocking> weekClockings = (await _clockingService._clockingRepo.FindAllAsync(clocking => clocking.Username.Equals(_userSettings.Username) &&
                                                clocking.ClockingWeek.Equals(weekNumber))).ToList();
            foreach (Clocking clocking in weekClockings)
            {
                clocking.SetClockingSettings(_clockingSettings);
            }
            WeeklyClockingInfo weeklyClockingInfo = new WeeklyClockingInfo(weekClockings);
            return View("ClockingsForUserAndWeek", weeklyClockingInfo);
        }

        public async Task<ActionResult> GetClockingInvoiceForWeek(int weekNumber, string gmtTimeZoneId)
        {
            TimeZoneInfo specifiedTimeZone = TimeZoneInfo.FindSystemTimeZoneById(gmtTimeZoneId);
            IList<Clocking> weekClockings = (await _clockingService._clockingRepo.FindAllAsync(clocking => clocking.Username.Equals(_userSettings.Username) &&
                                                clocking.ClockingWeek.Equals(weekNumber))).ToList();
            foreach (Clocking clocking in weekClockings)
            {
                clocking.SetClockingSettings(_clockingSettings);
                clocking.SetTimeZoneForClockingWorkAndBreaks(specifiedTimeZone);
            }
            WeeklyClockingInfo weeklyClockingInfo = new WeeklyClockingInfo(weekClockings);
            return View("ClockingsInvoicePDF", weeklyClockingInfo);
        }

        private async Task<Clocking> RetrieveClockingById(string clockingId)
        {
            return await _clockingService._clockingRepo.FindByIdAsync(clockingId);
        }
    }
}

