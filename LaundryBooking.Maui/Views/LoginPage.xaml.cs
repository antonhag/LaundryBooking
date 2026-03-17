using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;
using LaundryBooking.Maui.Views.Admin;                 


public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((LoginViewModel)BindingContext).Reset();
    }
    
    private void OnClickedToggleTheme(object? sender, EventArgs e)
    {
        var app = Microsoft.Maui.Controls.Application.Current; // Globala app instansen
        if (app == null)
        {
            return;
        }

        bool isDarkMode = app.UserAppTheme == AppTheme.Dark;
        app.UserAppTheme = isDarkMode ? AppTheme.Light : AppTheme.Dark;
    }

    private async void OnClickedGoToAdminLoginPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdminLoginPage));
    }
}