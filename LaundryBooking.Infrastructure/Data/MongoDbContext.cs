using LaundryBooking.Domain.Entities;
using MongoDB.Driver;

namespace LaundryBooking.Infrastructure.Data
{
    public class MongoDbContext
    {
        private static MongoClient GetClient(string connectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return new MongoClient(settings);
        }

        public static IMongoCollection<Booking> GetBookingCollection(string connectionString)
        {
            var database = GetClient(connectionString).GetDatabase("LaundryBookingDb");
            return database.GetCollection<Booking>("Bookings");
        }

        public static IMongoCollection<IssueReport> GetIssueReportCollection(string connectionString)
        {
            var database = GetClient(connectionString).GetDatabase("LaundryBookingDb");
            return database.GetCollection<IssueReport>("IssueReports");
        }

        public static IMongoCollection<HousingCooperative> GetHousingCooperativeCollection(string connectionString)
        {
            var database = GetClient(connectionString).GetDatabase("LaundryBookingDb");
            return database.GetCollection<HousingCooperative>("HousingCooperatives");
        }
    }
}
