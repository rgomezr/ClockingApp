using ClockingApp.Models.ClockingData;
using MongoDB.Driver;

namespace ClockingApp.Controllers
{
    public class DatabaseController
    {
        public static string GetDatabases()
        {
            string username = "";
            string password = "";
            MongoClient client = new MongoClient(String.Format("mongodb+srv://{0}:{1}" +
                "@cluster0.fltxvuu.mongodb.net/?retryWrites=true&w=majority", username, password));
            List<string> databases = client.ListDatabaseNames().ToList();
            return String.Join(",", databases);
        }

        public string InsertClocking()
        {
            try { 
            string username = "";
            string password = "";
            MongoClient client = new MongoClient(String.Format("mongodb+srv://{0}:{1}" +
                "@cluster0.fltxvuu.mongodb.net/?retryWrites=true&w=majority", username, password));

            var clockingsCollection = client.GetDatabase("clockingsDB").GetCollection<Clocking>("clockings");

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

