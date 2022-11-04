using MongoDB.Bson;
namespace ClockingApp.Models.ClockingData
{
    public class Clocking
    {
        public ObjectId _id { get; set; }
        public string Username { get; set; } = null!;
        public WorkDay WorkDay { get; set; } = null!;
        public BreakDay? BreakDay { get; set; } = null!;

        public Clocking(string username, WorkDay workDay, BreakDay? breakDay)
        {
            Username = username;
            WorkDay = workDay;
            BreakDay = breakDay;
        }
    }
}

