using System;
namespace ClockingApp.Models.ClockingData
{
    public class BreakDay
    {
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndDate { get; set; } = null!;
        public bool IsBreakActive => StartDate != null && EndDate == null;
        public bool IsBreakFinished => StartDate != null && EndDate != null;
        public double Duration => IsBreakActive ? (DateTime.Now - ((DateTime)StartDate)).TotalHours : IsBreakFinished
            ? (((DateTime)EndDate) - ((DateTime)StartDate)).TotalHours : 0;

        public BreakDay(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

