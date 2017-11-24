using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RUTAS.REST.PASAJE.Models;
namespace RUTAS.REST.PASAJE.DbContext
{
    public class TravelPasajesContext
    {
        private readonly IMongoDatabase _database = null;
        //private readonly IMongoDatabase _databaseV = null;

        public TravelPasajesContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
                //_databaseV = client.GetDatabase(settings.Value.DatabaseV);
            }
        }

        public IMongoCollection<Promotion> Promotions { get { return _database.GetCollection<Promotion>("Promotion"); } }
        public IMongoCollection<Ticket> Tickets { get { return _database.GetCollection<Ticket>("Ticket"); } }
        //public IMongoCollection<Ticket> TicketsV { get { return _databaseV.GetCollection<Ticket>("Ticket"); } }
    }
}
