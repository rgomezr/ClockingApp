using System;
using MongoDB.Bson.Serialization.Attributes;
using ClockingApp.CustomAttributes;
namespace ClockingApp.Models.ClockingData
{
    public class WorkDay
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? StartDate { get; set; } = null!;
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? EndDate { get; set; } = null!;
        public bool IsWorkActive => StartDate != null && EndDate == null;
        public bool IsWorkFinished => StartDate != null && EndDate != null;
        [DoublePrecision(2)]
        public double Duration => IsWorkActive ? (DateTime.Now - ((DateTime)StartDate)).TotalHours : IsWorkFinished
            ? (((DateTime)EndDate) - ((DateTime)StartDate)).TotalHours : 0;

        public WorkDay(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}

