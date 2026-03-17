using LaundryBooking.Maui.ViewModels.Admin;
using Microsoft.Maui;

namespace LaundryBooking.Maui.Views.Admin;

public partial class AdminHomePage : ContentPage
{
    public AdminHomePage(AdminHomeViewModel adminHomeViewModel)
    {
        InitializeComponent();
        BindingContext = adminHomeViewModel;
    }

    private void OnClickedToggleTheme(object? sender, EventArgs e)
    {
        var app = Microsoft.Maui.Controls.Application.Current;
        if (app == null) return;

        bool isDarkMode = app.UserAppTheme == AppTheme.Dark;
        app.UserAppTheme = isDarkMode ? AppTheme.Light : AppTheme.Dark;
    }

    private async void OnClickedLogout(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnClickedGoAdminIssuePage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdminIssuePage));
    }

    private async void OnClickedGoAdminNewsPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdminNewsPage));
    }

    private async void OnClickedGoAdminHousingPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdminHousingPage));
    }
}