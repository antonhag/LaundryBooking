using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LaundryBooking.Maui.Views.Admin;

namespace LaundryBooking.Maui.ViewModels.Admin;


public class AdminLoginViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private string _username = string.Empty;
    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    
    private string _password = string.Empty;
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand { get; }

    public AdminLoginViewModel()
    {
        LoginCommand = new Command(LoginAsync);
    }

    private async void LoginAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("Fyll i användarnamn och lösenord.");

            if (Username != "admin" || Password != "admin123")
                throw new UnauthorizedAccessException("Felaktigt användarnamn eller lösenord.");

            await Shell.Current.GoToAsync(nameof(AdminHomePage));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Fel", ex.Message, "OK");
        }
        finally
        {
            Password = string.Empty;
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}