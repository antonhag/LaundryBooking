using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Interfaces;

public interface ILoginFacade
{
    Task<bool> ValidateApartmentAsync(string apartmentNumber, string housingCooperativeId);  
    Task<List<HousingCooperative>> GetHousingCooperativesAsync();         
}