using MongoDB.Driver;

namespace Hypesoft.Infrastructure.Data
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            Database = client.GetDatabase(databaseName);
        }
    }
}