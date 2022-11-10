using System;
namespace ClockingApp.Models.ClockingData
{
    public class BreakDay
    {
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndDate { get; set; } = null!;

        public BreakDay(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

