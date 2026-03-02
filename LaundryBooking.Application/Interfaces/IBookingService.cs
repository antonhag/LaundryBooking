using LaundryBooking.Domain.Entities;

namespace LaundryBooking.Application.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetBookingsByDateAsync(DateOnly date);
    Task<List<Booking>> GetBookingsByApartmentAsync(string apartmentNumber);
    Task<bool> CreateBookingAsync(Booking booking);
    Task DeleteBookingAsync(string id);
}
