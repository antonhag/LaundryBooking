using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryBooking.Application.Services; // Ta bort senare
using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        SessionService.GetSession().SetSession("1306", "f3a2c1d4-8b7e-4f6a-9c5d-2e1b0a3f7e8c"); // Temporär
        homeViewModel.ReloadNewsAsync();
        BindingContext = homeViewModel;
        WashMachineImage.HeightRequest = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density * 0.3;
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
}