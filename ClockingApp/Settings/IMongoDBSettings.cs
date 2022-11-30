using System;
namespace ClockingApp.Settings
{
    public interface IMongoDBSettings
    {
        string DatabaseName { get; set; }
    }
}

