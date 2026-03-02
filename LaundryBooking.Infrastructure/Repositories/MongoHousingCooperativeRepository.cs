using LaundryBooking.Infrastructure.Data;
using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LaundryBooking.Infrastructure.Repositories
{
    public class MongoHousingCooperativeRepository : IHousingCooperativeRepository
    {
        private readonly IMongoCollection<HousingCooperative> _housingCooperatives; // Kan bara bli initierad en gång, i konstruktorn

        public MongoHousingCooperativeRepository(string connectionString)
        {
            _housingCooperatives = MongoDbContext.GetHousingCooperativeCollection(connectionString);
        }

        public async Task<List<HousingCooperative>> GetAllHousingCooperativesAsync()
        {
            List<HousingCooperative> housingCooperatives = await _housingCooperatives.AsQueryable().ToListAsync();
            return housingCooperatives;
        }

        public async Task<HousingCooperative?> GetHousingCooperativeByIdAsync(string id)
        {
            var housingCooperatives = await _housingCooperatives.Find(h => h.Id == id).FirstOrDefaultAsync();
            return housingCooperatives;
        }

        public async Task CreateHousingCooperativeAsync(HousingCooperative housingCooperative)
        {
            await _housingCooperatives.InsertOneAsync(housingCooperative);
        }

        public async Task DeleteHousingCooperativeAsync(string id)
        {
            await _housingCooperatives.DeleteOneAsync(h => h.Id == id);
        }
    }
}
