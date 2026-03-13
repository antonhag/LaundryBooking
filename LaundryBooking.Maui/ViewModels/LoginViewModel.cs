using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Application.Settings;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Maui.DataManager;
using LaundryBooking.Maui.Views;

namespace LaundryBooking.Maui.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly SessionService _sessionService;
    private readonly ILoginFacade _loginFacade;
    private readonly GoogleAuthSettings _googleAuthSettings;
    public ICommand LoginCommand { get; }

    private string _apartmentNumber = string.Empty;
    public string ApartmentNumber
    {
        get { return _apartmentNumber; }
        set
        {
            _apartmentNumber = value;
            OnPropertyChanged(nameof(ApartmentNumber));
        }
    }

    private ObservableCollection<HousingCooperative> _housingCooperatives = new();
    public ObservableCollection<HousingCooperative> HousingCooperatives
    {
        get { return _housingCooperatives; }
        set
        {
            _housingCooperatives = value;
            OnPropertyChanged(nameof(HousingCooperatives));
        }
    }

    private HousingCooperative? _selectedHousingCooperative;
    public HousingCooperative? SelectedHousingCooperative
    {
        get { return _selectedHousingCooperative; }
        set
        {
            _selectedHousingCooperative = value;
            OnPropertyChanged(nameof(SelectedHousingCooperative));
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public LoginViewModel(SessionService sessionService, ILoginFacade loginFacade, GoogleAuthSettings googleAuthSettings)
    {
        _sessionService = sessionService;
        _loginFacade = loginFacade;
        _googleAuthSettings = googleAuthSettings;
        LoadCooperativesAsync();
        LoginCommand = new Command(LoginAsync);
    }

    private async void LoadCooperativesAsync()
    {
        try
        {
            var housingCooperatives = await _loginFacade.GetHousingCooperativesAsync();
            HousingCooperatives = new ObservableCollection<HousingCooperative>(housingCooperatives);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", "Kunde inte hämta föreningar", "OK");
        }
    }

    private async void LoginAsync()
    {
        if (SelectedHousingCooperative == null || string.IsNullOrWhiteSpace(ApartmentNumber))
        {
            await Shell.Current.DisplayAlertAsync("Fel", "Välj förening och ange lägenhetsnummer", "OK");
            return;
        }

        var isValid = await _loginFacade.ValidateApartmentAsync(ApartmentNumber, SelectedHousingCooperative.Id);
        if (!isValid)
        {
            await Shell.Current.DisplayAlertAsync("Fel", "Lägenhetsnumret tillhör inte vald förening", "OK");
            return;
        }

        try
        {
            // PKCE (Proof Key for Code Exchange) skyddar mot att en annan app
            // fångar upp authorization code:n och missbrukar den
            var codeVerifier = GenerateCodeVerifier();
            var codeChallenge = GenerateCodeChallenge(codeVerifier);

            // Bygger upp Google OAuth URL med authorization code flow
            // response_type=code innebär att Google returnerar en engångskod, inte en token direkt
            // scope=email%20profile betyder att vi begär tillgång till användarens e-post och profilinfo
            var authUrl = new Uri(
                $"https://accounts.google.com/o/oauth2/v2/auth" +
                $"?client_id={_googleAuthSettings.ClientId}" +
                $"&redirect_uri=com.companyname.laundrybooking.maui://" +
                $"&response_type=code" +
                $"&scope=email%20profile%20https://www.googleapis.com/auth/calendar.events" +
                $"&code_challenge={codeChallenge}" +
                $"&code_challenge_method=S256");

            // Öppnar Googles inloggningssida i en säker webbläsare och väntar på callback
            // När användaren loggar in omdirigerar Google till vår custom URI (com.companyname...)
            WebAuthenticatorResult result = await
                WebAuthenticator.Default.AuthenticateAsync(
                    authUrl, new Uri("com.companyname.laundrybooking.maui://"));

            // Byter ut authorization code mot en access token och hämtar förnamnet via GoogleAuthManager
            var (givenName, accessToken) = await GoogleAuthManager.GetUserInfoAsync(
                result.Properties["code"], codeVerifier, _googleAuthSettings.ClientId);

            _sessionService.SetSession(ApartmentNumber, SelectedHousingCooperative.Id, givenName, accessToken);
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
        catch (TaskCanceledException)
        {
            // Användaren avbröt
        }
    }

    // Genererar en slumpmässig Base64URL-kodad sträng som används som hemlig nyckel i PKCE-flödet
    private static string GenerateCodeVerifier()
    {
        var bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    // Hashar code verifier med SHA256 och Base64URL-kodar resultatet
    // Skickas med i auth-begäran så Google kan verifiera att det är samma app som begärde koden
    private static string GenerateCodeChallenge(string codeVerifier)
    {
        var hash = SHA256.HashData(Encoding.ASCII.GetBytes(codeVerifier));
        return Convert.ToBase64String(hash)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
