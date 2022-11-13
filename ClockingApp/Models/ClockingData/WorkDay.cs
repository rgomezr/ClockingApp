using System;
namespace ClockingApp.Models.ClockingData
{
    public class WorkDay
    {
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndDate { get; set; } = null!;
        public bool IsWorkActive => StartDate != null && EndDate == null;
        public bool IsWorkFinished => StartDate != null && EndDate != null;
        public double Duration => IsWorkActive ? (DateTime.Now - ((DateTime)StartDate)).TotalHours : IsWorkFinished
            ? (((DateTime)EndDate) - ((DateTime)StartDate)).TotalHours : 0;

        public WorkDay(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

