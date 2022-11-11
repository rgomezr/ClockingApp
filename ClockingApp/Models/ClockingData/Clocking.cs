using ClockingApp.CustomAttributes;
using ClockingApp.Models.MongoAbstraction;

namespace ClockingApp.Models.ClockingData
{
    [BsonCollection("clockings")]
    public class Clocking : Document
    {
        public string Username { get; set; } = null!;
        public int ClockingWeek { get; set; }
        public DateTime ClockingDate { get; set; }
        public WorkDay WorkDay { get; set; } = null!;
        public BreakDay? BreakDay { get; set; } = null!;

        public Clocking(string username, int clockingWeek, DateTime clockingDate, WorkDay workDay, BreakDay? breakDay)
        {
            Username = username;
            ClockingWeek = clockingWeek;
            ClockingDate = clockingDate;
            WorkDay = workDay;
            BreakDay = breakDay;
        }
    }
}

