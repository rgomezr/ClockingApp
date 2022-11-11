using ClockingApp.CustomAttributes;
using ClockingApp.Models.MongoAbstraction;

namespace ClockingApp.Models.ClockingData
{
    [BsonCollection("clockings")]
    public class Clocking : Document
    {
        public string Username { get; set; } = null!;
        public DateTime? ClockingDate { get; set; } = null!;
        public WorkDay WorkDay { get; set; } = null!;
        public BreakDay? BreakDay { get; set; } = null!;

        public Clocking(string username, DateTime clockingDate, WorkDay workDay, BreakDay? breakDay)
        {
            Username = username;
            ClockingDate = clockingDate;
            WorkDay = workDay;
            BreakDay = breakDay;
        }
    }
}

