using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;
using MongoDB.Bson;

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

        [HttpPost]
        public async Task<ActionResult> StartWork([FromBody] string username)
        {
            DateTime currentDate = DateTime.Now;
            WorkDay workDay = new WorkDay(currentDate, null);
            Clocking clocking = new Clocking(username, ISOWeek.GetWeekOfYear(currentDate), currentDate.Date, workDay, null);
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
                } catch (Exception ex)
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

        public async Task<ActionResult> GetAllClockingsForUserAndWeek([FromQuery] string username, int weekNumber)
        {
            IList<Clocking> weekClockings = (await _clockingService._clockingRepo.FindAllAsync(clocking => clocking.Username.Equals(username) &&
                                            clocking.ClockingWeek.Equals(weekNumber))).ToList();
            return View("ClockingsForUserAndWeek", weekClockings);
        }

        private async Task<Clocking> RetrieveClockingById (string clockingId)
        {
            return await _clockingService._clockingRepo.FindByIdAsync(clockingId);
        }
        

    }
}

