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
        [BsonIgnore]
        private TimeZoneInfo? TimeZoneSpecific { get; set; } = null!;
        public string StartDate_formatted => this.TimeZoneSpecific != null
            ? TimeZoneInfo.ConvertTimeFromUtc(this.StartDate.ToUniversalTime(), this.TimeZoneSpecific).ToString("t")
            : this.StartDate.ToString("t");
        public string EndDate_formatted => this.EndDate != null
            ? (this.TimeZoneSpecific != null
                ? TimeZoneInfo.ConvertTimeFromUtc(this.EndDate.Value.ToUniversalTime(), this.TimeZoneSpecific).ToString("t")
                : this.EndDate.Value.ToString("t"))
            : "";
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

        public void SetSpecificTimeZone(TimeZoneInfo specificTimeZone)
        {
            this.TimeZoneSpecific = specificTimeZone;
        }
    }
}

