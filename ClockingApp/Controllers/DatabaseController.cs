using ClockingApp.Models.ClockingData;
using ClockingApp.CustomServices;

namespace ClockingApp.Controllers
{
    public class DatabaseController
    {
        private readonly ClockingService _clockingService;

        public DatabaseController(ClockingService clockingService)
        {
            _clockingService = clockingService;
        }

        public async Task<string> ReadClockingForUsername(string username)
        {
            Clocking clocking = await _clockingService._clockingRepo.FindOneAsync(clocking => clocking.Username.Equals(username));
            return String.Format("{0} : {1}",clocking._id.ToString());
        }
    }
}

