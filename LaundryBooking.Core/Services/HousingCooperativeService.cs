using LaundryBooking.Core.Interfaces;
using LaundryBooking.Core.Models;

namespace LaundryBooking.Core.Services
{
    public class HousingCooperativeService
    {
        private readonly IHousingCooperativeRepository _housingCooperativeRepository;

        public HousingCooperativeService(IHousingCooperativeRepository housingCooperativeRepository)
        {
            _housingCooperativeRepository = housingCooperativeRepository;
        }

        public async Task<List<HousingCooperative>> GetAllHousingCooperativesAsync()
        {
            var housingCooperatives = await _housingCooperativeRepository.GetAllHousingCooperativesAsync();
            return housingCooperatives;
        }

        public async Task<HousingCooperative?> GetHousingCooperativeByIdAsync(string id)
        {
            return await _housingCooperativeRepository.GetHousingCooperativeByIdAsync(id);
        }

        public async Task CreateHousingCooperativeAsync(HousingCooperative housingCooperative)
        {
            await _housingCooperativeRepository.CreateHousingCooperativeAsync(housingCooperative);
        }

        public async Task DeleteHousingCooperativeAsync(string id)
        {
            await _housingCooperativeRepository.DeleteHousingCooperativeAsync(id);
        }
    }
}
