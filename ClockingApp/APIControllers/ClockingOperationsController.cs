﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ClockingApp.Controllers;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;
using ClockingApp.Models.API;
using ClockingApp.Settings;
using Microsoft.AspNetCore.Mvc;

namespace ClockingApp.APIControllers
{
    [Route("api/[action]")]
    public class ClockingOperationsController : Controller
    {
        private readonly ClockingService _clockingService;
        private readonly IUserSettings _userSettings;
        private readonly IClockingSettings _clockingSettings;

        public ClockingOperationsController(ClockingService clockingService, IUserSettings userSettings, IClockingSettings clockingSettings)
        {
            _clockingService = clockingService;
            _userSettings = userSettings;
            _clockingSettings = clockingSettings;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<string> GetTodaysClocking()
        {
            DateTime today = DateTime.Now.Date;
            Clocking todaysClocking = await _clockingService._clockingRepo
                                        .FindOneAsync(clocking => clocking.Username.Equals(_userSettings.Username) && clocking.ClockingDate == today);
            todaysClocking?.SetClockingSettings(_clockingSettings);
            string clockingJson = Newtonsoft.Json.JsonConvert.SerializeObject(todaysClocking);
            return clockingJson;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<string> StartWork()
        {
            (bool isSuccess, string exception) resultTuple = (false, "There's already a Clocking for today");
            if (!await _clockingService._clockingRepo.IsClockingForToday())
            {
                DateTime currentDate = DateTime.Now;
                WorkDay workDay = new(currentDate, null);
                Clocking clocking = new(_userSettings.Username, currentDate.Year, ISOWeek.GetWeekOfYear(currentDate), currentDate.Date, workDay, null);
                resultTuple = await _clockingService._clockingRepo.InsertOneAsync(clocking);
            }
            ApiResponse apiResponse = new(resultTuple.isSuccess, resultTuple.exception);
            string resultJson = Newtonsoft.Json.JsonConvert.SerializeObject(apiResponse);
            return resultJson;
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

