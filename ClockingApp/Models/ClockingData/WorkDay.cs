using System;
using MongoDB.Bson.Serialization.Attributes;
using ClockingApp.CustomAttributes;
namespace ClockingApp.Models.ClockingData
{
    public class WorkDay
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
        public bool IsWorkActive => (EndDate == null);
        public bool IsWorkFinished => (EndDate != null);
        [DoublePrecision(2)]
        public double Duration => IsWorkActive ? (DateTime.Now - StartDate).TotalHours : IsWorkFinished
            ? ((EndDate.Value) - StartDate).TotalHours : 0;
        public string Duration_formatted => Duration >= 1 ? String.Format("{0}{1}", this.Duration.ToString("#.#"), "h") :
            String.Format("{0}{1}", (this.Duration * 60).ToString("##"), "m");

        public WorkDay(DateTime startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public void SetSpecificTimeZone(TimeZoneInfo specificTimeZone)
        {
            this.TimeZoneSpecific = specificTimeZone;
        }
    }
}