using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MoveToCore.Config
{
    public class DB
    {
        public MongoDatabase Db { get; set; }

        public static DB Instance = new DB();

        private DB()
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            if (connectionString == null)
            {
                throw new ApplicationException("The env varibale CONNECTION_STRING is not set");
            }

            var client = new MongoClient(new MongoClientSettings
            {
                Servers = connectionString.Split(',').Select(x => x.Split(':')).Select(x => new MongoServerAddress(x[0], int.Parse(x[1]))), 
                GuidRepresentation = GuidRepresentation.CSharpLegacy,
                ReadPreference = ReadPreference.PrimaryPreferred
            });
            var server = client.GetServer();
            Db = server.GetDatabase("wasos");
        }
    }
}
