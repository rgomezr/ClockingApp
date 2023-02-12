using System;
using MongoDB.Bson.Serialization.Attributes;
using ClockingApp.CustomAttributes;
namespace ClockingApp.Models.ClockingData
{
    public class BreakDay
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? EndDate { get; set; } = null!;
        public bool IsBreakActive => EndDate == null;
        public bool IsBreakFinished => EndDate != null;
        public double Duration => IsBreakActive ? (DateTime.Now - StartDate).TotalMinutes : IsBreakFinished
            ? (EndDate.Value - StartDate).TotalMinutes : 0;
        public string Duration_formatted => this.Duration >= 1 ? String.Format("{0}{1}", this.Duration.ToString("##"), "m") : "";

        public BreakDay(DateTime startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

