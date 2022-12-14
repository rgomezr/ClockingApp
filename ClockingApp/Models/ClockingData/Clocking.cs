using ClockingApp.CustomAttributes;
using ClockingApp.Models.MongoAbstraction;
using MongoDB.Bson.Serialization.Attributes;

namespace ClockingApp.Models.ClockingData
{
    [BsonCollection("clockings")]
    public class Clocking : Document
    {
        public string Username { get; set; }
        public int ClockingWeek { get; set; }
        [BsonDateTimeOptions(DateOnly = true, Kind = DateTimeKind.Local)]
        public DateTime ClockingDate { get; set; }
        public WorkDay WorkDay { get; set; } = null!;
        public List<BreakDay>? Breaks { get; set; } = null!;

        public Clocking(string username, int clockingWeek, DateTime clockingDate, WorkDay workDay, List<BreakDay>? breaks)
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
            return Breaks != null ? Breaks.Any(_break => _break.IsBreakActive) : false;
        }
        /// <summary>
        /// Adds parametrised BreakDay to this.Breaks.
        /// If empty, list will be initialised with parameter on it
        /// </summary>
        /// <param name="breakDay"></param>
        public void AddToBreakList(BreakDay breakDay)
        {
            if (Breaks != null)
            {
                Breaks.Add(breakDay);
            }
            else
            {
                Breaks = new List<BreakDay> { breakDay };
            }
        }
        public void FinishActiveBreak()
        {
            if (Breaks != null)
            {
                DateTime currentDate = DateTime.Now;
                Breaks.Find(_break => _break.IsBreakActive).EndDate = currentDate;
            }
        }
    }
}

