using LaundryBooking.Application.Services;
using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;

public partial class HomePage : ContentPage
{
    private readonly SessionService _sessionService;

    public HomePage(HomeViewModel homeViewModel, SessionService sessionService)
    {
        InitializeComponent();
        homeViewModel.ReloadNewsAsync();
        BindingContext = homeViewModel;
        _sessionService = sessionService;
    }

    private async void OnClickedGoToBookingPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BookingPage));  
    }
    
    private async void OnClickedGoToManageBookingPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ManageBookingPage));                                                                    

    }

    private async void OnClickedGoToIssueReportPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(IssueReportPage));   
    }

    private async void OnClickedGoToNewsPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewsPage));
    }

    private async void OnClickedLogout(object? sender, EventArgs e)
    {
        _sessionService.ClearSession();
        await Navigation.PopAsync();
    }
}