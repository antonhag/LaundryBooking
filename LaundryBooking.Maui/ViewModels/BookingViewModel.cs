using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using LaundryBooking.Application.Interfaces;
using LaundryBooking.Application.Services;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Maui.ViewModels;

// Används för att hålla ett tidspass (enum, tex Morning) och en display text "07:00-12:00"
public record TimeSlotOption(TimeSlot TimeSlot, string DisplayText, bool IsAvailable)
{
    // Sätter färg på bordern beroende på ifall tiden är ledig(grön) eller upptagen(grå)
    public Color BackgroundColor => IsAvailable ? Color.FromArgb("#3D6B68") : Color.FromArgb("#B0A898"); 
}

// ViewModel för bokningssidan, implementerar INotifyPropertyChanged för att UI:t ska uppdateras automatiskt
public class BookingViewModel : INotifyPropertyChanged
{
    // Krävs av INotifyPropertyChanged, Event som triggas när property ändras
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public ICommand BookCommand { get; } 
    
    // Valda datumet från kalendern
    private DateTime? _selectedDate;

    public DateTime? SelectedDate
    {
        get { return _selectedDate; }
        set
        {
            _selectedDate = value;
            SelectedTimeSlot = null;
            OnPropertyChanged(nameof(SelectedDate));
            LoadAvailableTimeSlotsAsync();
        }
    }
    
    // Håller det faktiska värdet för AvailableTimeSlots
    private ObservableCollection<TimeSlotOption> _availableTimeSlots = new();
    
    // Lista med tillgängliga tider, UI uppdateras automatiskt när listan byts ut
    public ObservableCollection<TimeSlotOption> AvailableTimeSlots
    {
        get { return _availableTimeSlots; }
        set
        {
            _availableTimeSlots = value;
            OnPropertyChanged(nameof(AvailableTimeSlots)); // Meddelar UI att listan har ändrat så att den ritar om vyn
        }
    }
    
    private TimeSlotOption? _selectedTimeSlot;                                                                                     
    public TimeSlotOption? SelectedTimeSlot                                                                                      
    {
        get { return _selectedTimeSlot; }
        set
        {
            _selectedTimeSlot = value;
            OnPropertyChanged(nameof(SelectedTimeSlot));
        }
    }
    
    // Båda services injiceras via konstruktorn istället för att skapas här
    private readonly IBookingService _bookingService;                                                                              
    private readonly SessionService _sessionService;

    // Tar emot services via dependency injection
    public BookingViewModel(IBookingService bookingService, SessionService sessionService)
    {
        _bookingService = bookingService;
        _sessionService = sessionService;
        BookCommand = new Command(() => CreateBookingAsync());
    }
    
    // Anropas när en property förändras, för att rita om UI, tex när någon bokat en tid
    private void OnPropertyChanged(string propertyName)                                                                            
    {                                                                                                                              
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));                                                 
    }

    private async void LoadAvailableTimeSlotsAsync()
    {
        if (_selectedDate == null)
        {
            return;
        }
        // Konverterar DateTime till DateOnly eftersom databasen lagrar endast DateOnly
        var date = DateOnly.FromDateTime(_selectedDate.Value); 
        
        var existingBookings = await _bookingService.GetBookingsByDateAsync(date);

        var takenSlots = existingBookings.Select(b => b.TimeSlot).ToList();

        var allSlots = new Dictionary<TimeSlot, string>
        {
            { TimeSlot.Morning, "07:00-12:00" },
            { TimeSlot.Afternoon, "12:00-17:00" },
            { TimeSlot.Evening, "17:00-21:00" }
        };
        
        // Kollar efter tidspass och sorterar de på färger beroende på ifall de är lediga eller upptagna
        AvailableTimeSlots = new ObservableCollection<TimeSlotOption>
            (allSlots
                .Select(kvp => new TimeSlotOption(kvp.Key, kvp.Value, !takenSlots.Contains(kvp.Key)))); // Skapar ett TimeSlotOption för varje pass
    }

    public async void CreateBookingAsync()
    {
        if (_selectedDate == null || SelectedTimeSlot == null)
        {
            await Shell.Current.DisplayAlert("Fel", "Välj ett datum och ett tidspass först.", "OK");                                   
            return;  
        }
        
        var date = DateOnly.FromDateTime(_selectedDate.Value);
        
        var newBooking = new Booking()
        {
            HousingCooperativeId = _sessionService.HousingCooperativeId,
            ApartmentNumber = _sessionService.ApartmentNumber,
            Date = date,
            TimeSlot = SelectedTimeSlot.TimeSlot
        };
        
        var success = await _bookingService.CreateBookingAsync(newBooking);

        if (success)
        {
            await Shell.Current.DisplayAlertAsync("Klart", "Bokningen är skapad!", "OK");
        }
        else
        {
            await Shell.Current.DisplayAlertAsync("Fel", "Tiden är redan bokad.", "OK");
        }
    }
}