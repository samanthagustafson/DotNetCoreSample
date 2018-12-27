using MongoDB.Driver;

namespace MoveToCore.Config
{
    public class DB
    {
        public MongoDatabase Db { get; set; }

        public static DB Instance = new DB();

        private DB()
        {
            var connectionString = "mongodb://10.0.75.1";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            Db = server.GetDatabase("wasos");
        }
    }
}
