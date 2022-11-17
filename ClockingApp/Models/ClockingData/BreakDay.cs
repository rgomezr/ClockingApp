using System;
using MongoDB.Bson.Serialization.Attributes;
using ClockingApp.CustomAttributes;
namespace ClockingApp.Models.ClockingData
{
    public class BreakDay
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? StartDate { get; set; } = null!;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? EndDate { get; set; } = null!;
        public bool IsBreakActive => StartDate != null && EndDate == null;
        public bool IsBreakFinished => StartDate != null && EndDate != null;
        [DoublePrecision(2)]
        public double Duration => IsBreakActive ? (DateTime.Now - ((DateTime)StartDate)).TotalHours : IsBreakFinished
            ? (((DateTime)EndDate) - ((DateTime)StartDate)).TotalHours : 0;

        public BreakDay(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

