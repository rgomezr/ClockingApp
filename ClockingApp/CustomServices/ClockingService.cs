using System;
using ClockingApp.Repository;
using ClockingApp.Settings;
using MongoDB.Driver;
namespace ClockingApp.CustomServices
{
	public class ClockingService
	{
		public readonly IClockingRepository _clockingRepo;

		public ClockingService(IMongoClient mongoClient, IMongoDBSettings mongoSettings, IUserSettings userSettings)
		{
            _clockingRepo = new ClockingRepository(mongoClient, mongoSettings, userSettings);

		}
	}
}
