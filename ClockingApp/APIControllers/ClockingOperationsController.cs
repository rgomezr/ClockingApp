﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ClockingApp.Controllers;
using ClockingApp.CustomServices;
using ClockingApp.Models.ClockingData;
using ClockingApp.Settings;
using Microsoft.AspNetCore.Mvc;

namespace ClockingApp.APIControllers
{
    [Route("api/[controller]")]
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

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

