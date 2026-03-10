using LaundryBooking.Domain.Entities;
using MongoDB.Driver;

namespace LaundryBooking.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase("LaundryBookingDb");
        }

        public IMongoCollection<Booking> Bookings => _database.GetCollection<Booking>("Bookings");
        public IMongoCollection<IssueReport> IssueReports => _database.GetCollection<IssueReport>("IssueReports");
        public IMongoCollection<HousingCooperative> HousingCooperatives => _database.GetCollection<HousingCooperative>("HousingCooperatives");
        public IMongoCollection<NewsPost> NewsPosts => _database.GetCollection<NewsPost>("NewsPosts");
    }
}
