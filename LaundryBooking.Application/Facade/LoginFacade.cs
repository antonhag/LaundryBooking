using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Settings;
using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Facade;

// Facade design pattern för inloggningsflödet, validering av lägenhet och förening
public class LoginFacade : ILoginFacade
{
    private readonly IHousingCooperativeService _housingCooperativeService;
    private readonly GoogleAuthSettings _googleAuthSettings;

    public LoginFacade(IHousingCooperativeService housingCooperativeService, GoogleAuthSettings googleAuthSettings)
    {
        _housingCooperativeService = housingCooperativeService;
        _googleAuthSettings = googleAuthSettings;
    }
    
    public async Task<bool> ValidateApartmentAsync(string apartmentNumber, string housingCooperativeId)
    {
        var cooperative = await _housingCooperativeService.GetHousingCooperativeByIdAsync(housingCooperativeId);

        if (cooperative == null)
        {
            return false;
        }
        
        // Kollar ifall lägenhetsnumret finns i föreningens lista
        return cooperative.ApartmentNumbers.Contains(apartmentNumber);
    }

    public async Task<List<HousingCooperative>> GetHousingCooperativesAsync()
    {
        return await _housingCooperativeService.GetAllHousingCooperativesAsync();
    }
}