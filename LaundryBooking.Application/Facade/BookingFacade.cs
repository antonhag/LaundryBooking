using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Application.Facade;

public class BookingFacade : IBookingFacade // Facade design pattern    
{
    private readonly IBookingService _bookingService;

    public BookingFacade(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<List<TimeSlot>> GetAvailableTimeSlotsAsync(DateOnly date, string housingCooperativeId)
    {
        var existingBookings = await _bookingService.GetBookingsByDateAsync(date, housingCooperativeId);
        
        var takenSlots = existingBookings.Select(b => b.TimeSlot).ToList();
        
        // Returnera bara lediga tider, filtrera bort redan bokade slots
        return Enum.GetValues<TimeSlot>().Where(slot => !takenSlots.Contains(slot)).ToList(); 
    }

    public async Task<bool> CreateBookingAsync(Booking booking)
    {
        return await _bookingService.CreateBookingAsync(booking);
    }
}