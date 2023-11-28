using ClockingApp.Models.ClockingData;
namespace ClockingApp.Repository
{
	public interface IClockingRepository : IMongoRepositoryBase<Clocking>
	{
		Task<Clocking> GetClockingForToday();
	}
}

