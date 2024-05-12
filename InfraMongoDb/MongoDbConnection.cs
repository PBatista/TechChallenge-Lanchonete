using MongoDB.Driver;

namespace InfraMongoDb
{
    public class MongoDbConnection
    {
        public IMongoDatabase ConnectionDBMongo(string connection, string nome_database)
        {
            var client = new MongoClient(connection);
            var database = client.GetDatabase(nome_database);            

            return database;
        }        
    }
}
