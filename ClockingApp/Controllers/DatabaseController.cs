using System;
using MongoDB.Driver;

namespace ClockingApp.Controllers
{
    public class DatabaseController
    {
        public string GetDatabases()
        {
            using IHost host = Host.CreateDefaultBuilder().Build();

            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

            string username = config.GetValue<string>("mongoDBusername");
            string password = config.GetValue<string>("mongoDBpass");

            MongoClient client = new MongoClient(String.Format("mongodb+srv://{0}:{1}@cluster0.fltxvuu.mongodb.net/?retryWrites=true&w=majority", username, password));
            List<string> databases = client.ListDatabaseNames().ToList();
            return String.Join(",", databases);
        }
    }
}

