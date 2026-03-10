using LaundryBooking.Application.Interfaces;
using LaundryBooking.Domain.Entities;
using LaundryBooking.Domain.Enums;

namespace LaundryBooking.Application.Facade;

public class BookingFacade : IBookingFacade // Facade design pattern
{
    private readonly IBookingService _bookingService;
    private readonly IHousingCooperativeService _housingCooperativeService;
    
    public BookingFacade(IBookingService bookingService, IHousingCooperativeService housingCooperativeService)
    {
        _bookingService = bookingService;
        _housingCooperativeService = housingCooperativeService;
    }

    public async Task<List<TimeSlot>> GetAvailableTimeSlotsAsync(DateOnly date)
    {
        var existingBookings = await _bookingService.GetBookingsByDateAsync(date);
        
        var takenSlots = existingBookings.Select(b => b.TimeSlot).ToList();
        
        // Returnera bara lediga tider, filtrera bort redan bokade slots
        return Enum.GetValues<TimeSlot>().Where(slot => !takenSlots.Contains(slot)).ToList(); 
    }

    public async Task<bool> CreateBookingAsync(Booking booking)
    {
       var housingCooperative = await _housingCooperativeService.GetHousingCooperativeByIdAsync(booking.HousingCooperativeId);
       if (housingCooperative == null || !housingCooperative.ApartmentNumbers.Contains(booking.ApartmentNumber))
       {
           return false;
       }
       
       return await _bookingService.CreateBookingAsync(booking);
    }
}