using ClockingApp.CustomAttributes;
using ClockingApp.Models.MongoAbstraction;

namespace ClockingApp.Models.ClockingData
{
    [BsonCollection("clockings")]
    public class Clocking : Document
    {
        public string Username { get; set; }
        public int ClockingWeek { get; set; }
        public DateTime ClockingDate { get; set; }
        public WorkDay WorkDay { get; set; } = null!;
        public List<BreakDay> Breaks { get; set; } = null!;

        public Clocking(string username, int clockingWeek, DateTime clockingDate, WorkDay workDay, List<BreakDay> breaks)
        {
            Username = username;
            ClockingWeek = clockingWeek;
            ClockingDate = clockingDate;
            WorkDay = workDay;
            Breaks = breaks;
        }
        /// <summary>
        /// Determines whether any break within a clocking day is active or not
        /// </summary>
        /// <returns>true if there is any break active; Otherwise false</returns>
        public bool IsCurrentBreakActive()
        {
            return this.Breaks.Any(_break => _break.IsBreakActive);
        }
    }
}

