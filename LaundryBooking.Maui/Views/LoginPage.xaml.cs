using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;

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
}