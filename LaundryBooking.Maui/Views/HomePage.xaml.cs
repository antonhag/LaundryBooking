using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryBooking.Maui.Views;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnClickedGoToBookingPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookingPage());
    }
    
    private async void OnClickedGoToManageBookingPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageBookingPage());
    }

    private async void OnClickedGoToIssueReportPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new IssueReportPage());   
    }
}