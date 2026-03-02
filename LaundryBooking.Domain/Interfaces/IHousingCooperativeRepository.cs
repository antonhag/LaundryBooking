using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Domain.Interfaces
{
    public interface IHousingCooperativeRepository
    {
        Task<List<HousingCooperative>> GetAllHousingCooperativesAsync();
        Task<HousingCooperative?> GetHousingCooperativeByIdAsync(string id);
        Task CreateHousingCooperativeAsync(HousingCooperative housingCooperative);
        Task DeleteHousingCooperativeAsync(string id);
    }
}
