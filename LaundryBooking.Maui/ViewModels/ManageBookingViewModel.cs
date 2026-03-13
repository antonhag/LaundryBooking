using System.Collections.ObjectModel;
using System.ComponentModel;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Interfaces;
using LaundryBooking.Maui.DataManager;

namespace LaundryBooking.Maui.ViewModels;

public class ManageBookingViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private readonly IBookingService _bookingService;
    private readonly SessionService _sessionService;

    private ObservableCollection<Booking> _bookings = new();
    public ObservableCollection<Booking> Bookings
    {
        get { return _bookings; }
        set
        {
            _bookings = value;
            OnPropertyChanged(nameof(Bookings));
        }
    }

    public ManageBookingViewModel(IBookingService bookingService, SessionService sessionService)
    {
        _bookingService = bookingService;
        _sessionService = sessionService;
        LoadBookingsAsync();
    }
    
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void LoadBookingsAsync()
    {
        var bookings = await _bookingService.GetBookingsByApartmentAsync(_sessionService.ApartmentNumber);
        bookings = bookings.OrderBy(b => b.Date).ThenBy(b => b.TimeSlot).ToList();
        Bookings = new ObservableCollection<Booking>(bookings);
    }
    
    public async Task DeleteBookingAsync(Booking? booking)
    {
        if (booking == null)
        {
            return;
        }
        
        await _bookingService.DeleteBookingAsync(booking.Id);
        Bookings.Remove(booking);

        if (!string.IsNullOrEmpty(booking.CalendarEventId))
        {
            await GoogleCalendarManager.DeleteCalendarEvent(_sessionService.AccessToken, booking.CalendarEventId);
        }
    }
}