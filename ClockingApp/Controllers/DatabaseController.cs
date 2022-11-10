using ClockingApp.Models.ClockingData;
using ClockingApp.Repository;
using MongoDB.Driver;

namespace ClockingApp.Controllers
{
    public class DatabaseController
    {

        //private readonly IMongoClient _client;
        /*
        public DatabaseController(IMongoClient mongoClient)
        {
            _client = mongoClient;
        }
        */
        private readonly IMongoRepositoryBase<Clocking> _repository;

        public DatabaseController(IMongoRepositoryBase<Clocking> repository)
        {
            _repository = repository;
        }

        public async Task<string> ReadClockingForUsername(string username)
        {
            Clocking clocking = await _repository.FindOneAsync(clocking => clocking.Username.Equals(username));
            return String.Format("{0} : {1}",clocking._id.ToString(),clocking.CreatedAt.ToString());
        }

        public async Task<string> InsertClocking()
        {
            try
            {
                Clocking clockingTest = new Clocking("gomezr", DateTime.Now, new WorkDay(DateTime.Now, DateTime.Now.AddHours(2)), null);
                await _repository.InsertOneAsync(clockingTest);
            } catch (Exception ex)
            {
                return ex.Message;
            }
            return "ok";

        }

        /*
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

        public string ReadClockingsForUsername(string username)
        {
            var clockingsCollection = _client.GetDatabase("clockingsDB").GetCollection<Clocking>("clockings");
            FilterDefinition<Clocking> filter = Builders<Clocking>.Filter.Eq("Username", username);
            List<Clocking> clockings = clockingsCollection.Find(filter).ToList();
            return String.Join(",", clockings.Select(clocking => clocking.WorkDay.StartDate));
        }

        public string UpdateClockingForUsername(string username)
        {
            var clockingsCollection = _client.GetDatabase("clockingsDB").GetCollection<Clocking>("clockings");
            FilterDefinition<Clocking> filter = Builders<Clocking>.Filter.Eq("Username", username);
            BreakDay break_time = new BreakDay(DateTime.Now, DateTime.Now.AddMinutes(15));
            UpdateDefinition<Clocking> updateFilter = Builders<Clocking>.Update.Set<BreakDay>("BreakDay", break_time);
            var resultTest = clockingsCollection.UpdateOne(filter, updateFilter);

            return resultTest.IsAcknowledged.ToString();
        }

        public string DeleteClockingForUsername(string username)
        {
            DateTime dateToDelete = new DateTime(2022, 11, 06).Date;
            var clockingsCollection = _client.GetDatabase("clockingsDB").GetCollection<Clocking>("clockings");
            FilterDefinition<Clocking> filter_username = Builders<Clocking>.Filter.Eq("Username", username);
            FilterDefinition<Clocking> filter = Builders<Clocking>.Filter.Eq("ClockingDate", dateToDelete);
            filter_username = filter_username & filter;
            var result = clockingsCollection.DeleteOne(filter);

            return result.IsAcknowledged.ToString();
        }
        */
             
    }
}

