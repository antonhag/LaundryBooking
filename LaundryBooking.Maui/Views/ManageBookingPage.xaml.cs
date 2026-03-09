using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Maui.ViewModels;

namespace LaundryBooking.Maui.Views;

public partial class ManageBookingPage : ContentPage
{
    public ManageBookingPage(ManageBookingViewModel manageBookingViewModel)
    {
        InitializeComponent();
        BindingContext = manageBookingViewModel;
    }

    private async void OnClickedDeleteBooking(object? sender, EventArgs e)
    {
        // Hämtar knappen som klickades
        var button = sender as Button;
        
        // Hämtar bokningen som skickades med som CommandParameter
        var booking = button?.CommandParameter as Booking;
        
        // Hämtar ViewModel från Bcontext
        var vm = BindingContext as ManageBookingViewModel;
        
        await vm?.DeleteBookingAsync(booking);
    }
}