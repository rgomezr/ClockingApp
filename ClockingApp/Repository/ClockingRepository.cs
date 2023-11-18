using System;
using ClockingApp.Models.ClockingData;
using ClockingApp.Settings;
using MongoDB.Driver;
namespace ClockingApp.Repository
{
	public class ClockingRepository : MongoRepositoryBase<Clocking>, IClockingRepository
	{
		private readonly IUserSettings _userSettings;

        public ClockingRepository(IMongoClient mongoClient, IMongoDBSettings mongoSettings, IUserSettings userSettings)
			: base(mongoClient, mongoSettings)
		{
			_userSettings = userSettings;
		}

        public async Task<bool> IsClockingForToday()
        {
			DateTime todaysDate = DateTime.Now.Date;
			Clocking clocking = await this.FindOneAsync(clocking => clocking.Username.Equals(_userSettings.Username)
														&& clocking.ClockingDate == todaysDate);
			return clocking != null;

        }
    }
}

