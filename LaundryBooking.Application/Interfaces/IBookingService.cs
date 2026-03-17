using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetBookingsByDateAsync(DateOnly date, string housingCooperativeId);
    Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber);
    Task<bool> CreateBookingAsync(Booking booking);
    Task UpdateCalendarEventIdAsync(string id, string calendarEventId);
    Task DeleteBookingAsync(string id);
}
