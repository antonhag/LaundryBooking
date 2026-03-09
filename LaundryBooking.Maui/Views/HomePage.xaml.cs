using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        BindingContext = new HomeViewModel();
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
}