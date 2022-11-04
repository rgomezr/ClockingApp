using ClockingApp.Models.ClockingData;
using MongoDB.Driver;

namespace ClockingApp.Controllers
{
    public class DatabaseController
    {

        private readonly IMongoClient _client;

        public DatabaseController(IMongoClient mongoClient)
        {
            _client = mongoClient;
        }

        public string GetDatabases()
        {
            List<string> databases = _client.ListDatabaseNames().ToList();
            return String.Join(",", databases);
        }

        public string InsertClocking()
        {
            try { 
            var clockingsCollection = _client.GetDatabase("clockingsDB").GetCollection<Clocking>("clockings");

            Clocking clockingTest = new Clocking("gomezr", new WorkDay(DateTime.Now, DateTime.Now.AddHours(2)), null);

            clockingsCollection.InsertOne(clockingTest);
            } catch (Exception exception)
            {
                return exception.Message;
            }
            return "Success";
        }

    }
}

