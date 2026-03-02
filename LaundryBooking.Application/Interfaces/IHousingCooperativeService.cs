using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Interfaces;

public interface IHousingCooperativeService
{
    Task<List<HousingCooperative>> GetAllHousingCooperativesAsync();
    Task<HousingCooperative?> GetHousingCooperativeByIdAsync(string id);
    Task CreateHousingCooperativeAsync(HousingCooperative housingCooperative);
    Task DeleteHousingCooperativeAsync(string id);
}
