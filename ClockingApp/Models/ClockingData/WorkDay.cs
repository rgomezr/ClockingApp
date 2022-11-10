﻿using System;
namespace ClockingApp.Models.ClockingData
{
    public class WorkDay
    {
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndDate { get; set; } = null!;

        public WorkDay(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
