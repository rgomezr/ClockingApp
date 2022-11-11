using System;
using ClockingApp.Models.ClockingData;
using ClockingApp.Settings;
using MongoDB.Driver;
namespace ClockingApp.Repository
{
	public class ClockingRepository : MongoRepositoryBase<Clocking>, IClockingRepository
	{
		public ClockingRepository(IMongoClient mongoClient, IMongoDBSettings mongoSettings)
			: base(mongoClient, mongoSettings)
		{
			
		}
	}
}

